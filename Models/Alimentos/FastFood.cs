using System;
using System.Collections.Generic;

namespace Nutra.Models.Alimentos;

public class FastFood
{
    public int Id { get; set; }
    public string? Fabricante { get; set; }
    public string? Produto { get; set; }
    public double? Porcao { get; set; }
    public double? EnergiaKcal { get; set; }
    public double? EnergiaKj { get; set; }
    public double? Proteinas { get; set; }
    public double? Carboidratos { get; set; }
    public double? Acucar { get; set; }
    public double? Gorduras { get; set; }
    public double? GorduraSaturada { get; set; }
    public double? GorduraPoliinsaturada { get; set; }
    public double? GorduraMonoinsaturada { get; set; }
    public double? GorduraTrans { get; set; }
    public double? Colesterol { get; set; }
    public double? Fibras { get; set; }
    public double? Sodio { get; set; }
    public double? Potassio { get; set; }
}
