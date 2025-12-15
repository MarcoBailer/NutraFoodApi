using Nutra.Enum;

namespace Nutra.Models.RegraNutricional;

public class PreferenciaAlimentar
{
    public int Id { get; set; }
    public int AlimentoId { get; set; } // Ex: "Brócolis", "Peixes", "Doces"
    public ETipoTabela Tabela { get; set; } // Ex: Tbcas, Fabricantes, FastFoods, Genericos
    public ETipoPreferencia Tipo { get; set; }
}
