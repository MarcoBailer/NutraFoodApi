namespace Nutra.Models;

public class RegistroAlimentar
{
    public long Id { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    // Qual alimento? (Copiamos o nome e os macros para histórico, 
    // ou referenciamos o ID? 
    public int AlimentoId { get; set; }
    public string NomeAlimento { get; set; }

    public double QuantidadeConsumidaG { get; set; } // Ex: 150g de arroz

    // Macros CALCULADOS para essa quantidade (Regra de 3)
    public double CaloriasConsumidas { get; set; }
    public double ProteinaConsumida { get; set; }
    // ... outros macros ...

    public DateTime DataConsumo { get; set; }
    public string Refeicao { get; set; } // "Cafe", "Almoco", "Jantar"
}
