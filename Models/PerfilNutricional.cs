using Nutra.Enum;

namespace Nutra.Models;

public class PerfilNutricional
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime DataNascimento { get; set; }
    public EGeneroBiologico Genero { get; set; }
    public double AlturaCm { get; set; }
    public double PesoAtualKg { get; set; }
    public double? PercentualGorduraCorporal { get; set; }
    public ENivelAtividadeFisica NivelAtividade { get; set; }
    public double FatorAtividade { get; set; }
    public string OcupacaoProfissional { get; set; } = string.Empty;
    public virtual ICollection<RestricaoAlimentar> RestricoesAlimentares { get; set; } = new List<RestricaoAlimentar>();
    public virtual ICollection<PreferenciaAlimentar> PreferenciasAlimentares { get; set; } = new List<PreferenciaAlimentar>();
    public bool PossuiDoencasPreExistentes { get; set; }
    public string DescricaoCondicoesMedicas { get; set; } = string.Empty;
    public ETipoObjetivo Objetivo { get; set; }
    public EPreferenciaAlimentar PreferenciaDieta { get; set; }
    public double? PesoDesejadoKg { get; set; }
    public int RefeicoesPorDiaDesejadas { get; set; }
    public int TempoDisponivelPreparoMinutos { get; set; }
    public EEquipamentoDisponivel EquipamentoDisponivel { get; set; }
}
