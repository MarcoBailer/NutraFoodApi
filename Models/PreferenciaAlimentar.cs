using Nutra.Enum;

namespace Nutra.Models;

public class PreferenciaAlimentar
{
    public int Id { get; set; }
    public string AlimentoOuGrupo { get; set; } // Ex: "Brócolis", "Peixes", "Doces"
    public ETipoPreferencia Tipo { get; set; }
}
