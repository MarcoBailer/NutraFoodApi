using Nutra.Enum;
using Nutra.Models.Usuario;

namespace Nutra.Models.RegraNutricional;

public class RegistroAlimentar
{
    public long Id { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int AlimentoId { get; set; }
    public string NomeAlimento { get; set; }
    public ETipoTabela TipoTabela { get; set; } // "Tabela Tbca", "Tabela Fabricantes", etc.

    public double QuantidadeConsumidaG { get; set; }

    public DateTime DataConsumo { get; set; } = DateTime.Now;
    public string Refeicao { get; set; } = string.Empty; // "Cafe", "Almoco", "Jantar"
}
