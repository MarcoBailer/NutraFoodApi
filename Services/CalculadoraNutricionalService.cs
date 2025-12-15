using Nutra.Enum;
using Nutra.Interfaces;
using Nutra.Models.Usuario;

namespace Nutra.Services;

public class CalculadoraNutricionalService : ICalculadoraNutricional
{
    // Constantes Nutricionais (Calorias por grama)
    private const double CALORIAS_POR_GRAMA_PROTEINA = 4.0;
    private const double CALORIAS_POR_GRAMA_CARBO = 4.0;
    private const double CALORIAS_POR_GRAMA_GORDURA = 9.0;

    /// <summary>
    /// Método principal que orquestra todo o cálculo da dieta
    /// </summary>
    public MetaNutricional GerarMetaInicial(PerfilNutricional perfil)
    {
        // 1. Calcular Idade
        var idade = CalcularIdade(perfil.DataNascimento);

        // 2. Calcular TMB (Taxa Metabólica Basal) - O gasto em coma
        double tmb = CalcularTMB_MifflinStJeor(perfil.PesoAtualKg, perfil.AlturaCm, idade, perfil.Genero);

        // 3. Calcular GET (Gasto Energético Total) - O gasto real do dia a dia
        double get = CalcularGET(tmb, perfil.NivelAtividade);

        // 4. Definir Calorias da Dieta (VET - Valor Energético Total) baseado no objetivo
        double caloriasMeta = AjustarCaloriasPeloObjetivo(get, perfil.Objetivo);

        // 5. Calcular Macros (Divisão dos nutrientes)
        return CalcularDivisaoDeMacros(perfil, caloriasMeta);
    }

    // --- ETAPA 1: O MOTOR (TMB) ---
    // Fórmula de Mifflin-St Jeor (Considerada a mais segura atualmente)
    private double CalcularTMB_MifflinStJeor(double peso, double altura, int idade, EGeneroBiologico genero)
    {
        // Fórmula Base: (10 x peso) + (6.25 x altura) - (5 x idade)
        double tmbBase = (10 * peso) + (6.25 * altura) - (5 * idade);

        // Ajuste por Gênero
        if (genero == EGeneroBiologico.Masculino)
            return tmbBase + 5;
        else
            return tmbBase - 161;
    }

    // --- ETAPA 2: O MOVIMENTO (GET) ---
    private double CalcularGET(double tmb, ENivelAtividadeFisica nivel)
    {
        double fator = nivel switch
        {
            ENivelAtividadeFisica.Sedentario => 1.2,      // Escritório + zero exercício
            ENivelAtividadeFisica.LevementeAtivo => 1.375, // Exercício 1-3x semana
            ENivelAtividadeFisica.ModeradamenteAtivo => 1.55, // Exercício 3-5x semana
            ENivelAtividadeFisica.MuitoAtivo => 1.725,    // Exercício pesado 6-7x
            ENivelAtividadeFisica.ExtremamenteAtivo => 1.9, // Atleta de elite / Trabalho braçal pesado
            _ => 1.2
        };

        return tmb * fator;
    }

    // --- ETAPA 3: O OBJETIVO (Déficit ou Superávit) ---
    private double AjustarCaloriasPeloObjetivo(double get, ETipoObjetivo objetivo)
    {
        return objetivo switch
        {
            // Perda de Gordura: Déficit moderado de 20% (seguro e sustentável)
            // Poderia ser um valor fixo (-500kcal), mas porcentagem adapta melhor a pessoas pequenas/grandes
            ETipoObjetivo.PerdaDeGordura => get * 0.80,

            // Hipertrofia: Superávit leve de 10-15% para minimizar ganho de gordura
            ETipoObjetivo.Hipertrofia => get * 1.10,

            // Recomposição: Come na manutenção ou leve déficit (-5%), o foco será na Proteína alta
            ETipoObjetivo.RecomposicaoCorporal => get * 0.95,

            // Manutenção: Come o que gasta
            _ => get
        };
    }

    // --- ETAPA 4: A QUÍMICA (Macros e Hidratação) ---
    private MetaNutricional CalcularDivisaoDeMacros(PerfilNutricional perfil, double caloriasTotais)
    {
        var meta = new MetaNutricional
        {
            DataCalculo = DateTime.UtcNow,
            CaloriasDiarias = Math.Round(caloriasTotais),
            PerfilNutricionalId = perfil.Id
        };

        // --- A. PROTEÍNA (A Prioridade) ---
        // Estratégia: Definir gramas por KG de peso, não por % das calorias.
        // Motivo: O músculo precisa de material fixo, independente de quanto você come de energia.
        double gramasProteinaPorKg = perfil.Objetivo switch
        {
            ETipoObjetivo.Hipertrofia => 2.0, // Alta para construir
            ETipoObjetivo.PerdaDeGordura => 2.2, // Mais alta ainda para PROTEGER músculo no déficit
            ETipoObjetivo.RecomposicaoCorporal => 2.4, // Altíssima, pois é o cenário mais difícil
            _ => 1.8 // Manutenção saudável
        };

        meta.ProteinasDiarias = Math.Round(perfil.PesoAtualKg * gramasProteinaPorKg);

        // --- B. GORDURA (A Regulação Hormonal) ---
        // Estratégia: 0.8g a 1.0g por Kg é o saudável para hormônios.
        double gramasGorduraPorKg = 0.9; // Média segura
        meta.GordurasDiarias = Math.Round(perfil.PesoAtualKg * gramasGorduraPorKg);

        // --- C. CARBOIDRATO (O Combustível) ---
        // Estratégia: O que sobrar das calorias vai para o carboidrato.

        // 1. Calcula quantas calorias já gastamos com Proteína e Gordura
        double calsProteina = meta.ProteinasDiarias * CALORIAS_POR_GRAMA_PROTEINA;
        double calsGordura = meta.GordurasDiarias * CALORIAS_POR_GRAMA_GORDURA;

        // 2. Vê quanto sobrou
        double calsRestantes = caloriasTotais - calsProteina - calsGordura;

        // 3. Converte as calorias restantes em gramas de carboidrato
        // Se der negativo (muito raro, só em dietas extremas), zeramos.
        meta.CarboidratosDiarios = Math.Max(0, Math.Round(calsRestantes / CALORIAS_POR_GRAMA_CARBO));

        // --- D. ÁGUA E FIBRAS ---
        // Regra de bolso: 35ml a 45ml por Kg
        meta.AguaDiaria = Math.Round(perfil.PesoAtualKg * 0.035, 1); // Em Litros (se quiser ML, multiplique por 35)

        // Regra de bolso: 14g de fibra a cada 1000kcal
        meta.FibraDiaria = Math.Round((caloriasTotais / 1000) * 14);

        return meta;
    }

    private int CalcularIdade(DateTime dataNascimento)
    {
        var hoje = DateTime.Today;
        var idade = hoje.Year - dataNascimento.Year;
        if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
        return idade;
    }
}
