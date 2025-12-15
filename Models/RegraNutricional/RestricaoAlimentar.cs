using Nutra.Enum;

namespace Nutra.Models.RegraNutricional;

public class RestricaoAlimentar
{
    public int Id { get; set; }
    public EAlergico CompostoOrganico { get; set; } // e.g., Lactose, Glúten
}
