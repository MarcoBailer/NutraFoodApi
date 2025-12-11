using Nutra.Enum;
using Nutra.Interfaces;
using Nutra.Models.Usuario;

namespace Nutra.Services;

public class MetricasService : IMetricas
{
    public MetaNutricional CalcularMetas(ApplicationUser user)
    {
        // 1. Calcula Idade
        var idade = DateTime.Now.Year - user.PerfilAtivo.DataNascimento.Year;
        if (user.PerfilAtivo.DataNascimento.Date > DateTime.Now.AddYears(-idade)) idade--;

        // 2. TMB (Taxa Metabólica Basal) - O quanto gasta dormindo
        double tmb;
        if (user.PerfilAtivo.Genero == Enum.EGeneroBiologico.Masculino)
        {
            tmb = (10 * user.PerfilAtivo.PesoAtualKg) + (6.25 * user.PerfilAtivo.AlturaCm) - (5 * idade) + 5;
        }
        else
        {
            tmb = (10 * user.PerfilAtivo.PesoAtualKg) + (6.25 * user.PerfilAtivo.PesoAtualKg) - (5 * idade) - 161;
        }

        // 3. GET (Gasto Energético Total)
        double get = tmb * user.PerfilAtivo.FatorAtividade;

        // 4. Aplica o Objetivo (Déficit ou Superávit)
        double caloriasMeta = user.PerfilAtivo.Objetivo switch
        {
            ETipoObjetivo.PerdaDeGordura => get - 500, // Déficit agressivo mas seguro
            ETipoObjetivo.Hipertrofia => get + 300,    // Superávit leve
            _ => get                      // Manutenção
        };

        // 5. Divide os Macros (Exemplo balanceado: 30% Prot, 40% Carb, 30% Gord)
        // Obs: 1g Prot = 4kcal, 1g Carb = 4kcal, 1g Gord = 9kcal
        return new MetaNutricional
        {
            CaloriasDiarias = Math.Round(caloriasMeta),
            ProteinaG = Math.Round((caloriasMeta * 0.30) / 4),
            CarboidratoG = Math.Round((caloriasMeta * 0.40) / 4),
            GorduraG = Math.Round((caloriasMeta * 0.30) / 9)
        };
    }
}
