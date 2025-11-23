using System;
using System.Collections.Generic;

namespace Nutra.Models;

public partial class FastFood
{
    public int Id { get; set; }
    public string Fabricante { get; set; } = string.Empty;

    public string Produto { get; set; } = string.Empty;

    public string Porcao { get; set; } = string.Empty;

    public string EnergiaKcal { get; set; } = string.Empty;

    public string EnergiaKj { get; set; } = string.Empty;

    public string Proteinas { get; set; } = string.Empty;

    public string Carboidratos { get; set; } = string.Empty;

    public string Acucar { get; set; } = string.Empty;

    public string Gorduras { get; set; } = string.Empty;

    public string GorduraSaturada { get; set; } = string.Empty;

    public string GorduraPoliinsaturada { get; set; } = string.Empty;

    public string GorduraMonoinsaturada { get; set; } = string.Empty;

    public string GorduraTrans { get; set; } = string.Empty;

    public string Colesterol { get; set; } = string.Empty;

    public string Fibras { get; set; } = string.Empty;

    public string Sodio { get; set; } = string.Empty;

    public string Potassio { get; set; } = string.Empty;
}
