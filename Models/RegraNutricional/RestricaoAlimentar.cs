namespace Nutra.Models.RegraNutricional;

public class RestricaoAlimentar
{
    public int Id { get; set; }
    public string CompostoOrganico { get; set; } = string.Empty; // e.g., Lactose, Glúten
}
