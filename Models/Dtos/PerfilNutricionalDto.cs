using Nutra.Enum;
using Nutra.Models.RegraNutricional;
using Nutra.Models.Usuario;

namespace Nutra.Models.Dtos;

public class PerfilNutricionalDto
{
    public string UserId { get; set; }
    public double AlturaCm { get; set; }
    public double PesoAtualKg { get; set; }
    public double? PercentualGorduraCorporal { get; set; }
    public double FatorAtividade { get; set; }
    public string OcupacaoProfissional { get; set; } = string.Empty;
    public bool PossuiDoencasPreExistentes { get; set; }
    public string DescricaoCondicoesMedicas { get; set; } = string.Empty;
    public double? PesoDesejadoKg { get; set; }
    public int RefeicoesPorDiaDesejadas { get; set; }
    public int TempoDisponivelPreparoMinutos { get; set; }
    public double? CircunferenciaCinturaCm { get; set; }
    public DateTime DataNascimento { get; set; }
    public EGeneroBiologico Genero { get; set; }
    public ETipoObjetivo Objetivo { get; set; }
    public ENivelAtividadeFisica NivelAtividade { get; set; }
    public EPreferenciaAlimentar PreferenciaDieta { get; set; }
    public ICollection<RestricaoAlimentar> RestricoesAlimentares { get; set; }
    public ICollection<PreferenciaAlimentar>? PreferenciasAlimentares { get; set; }
    public List<EEquipamentoDisponivel> EquipamentosIds { get; set; }
}
