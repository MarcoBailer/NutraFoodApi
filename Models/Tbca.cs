using System;
using System.Collections.Generic;

namespace Nutra.Models;

public partial class Tbca
{
    public int Id { get; set; }
    public string Código { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public string NomeCientífico { get; set; } = string.Empty;

    public string Grupo { get; set; } = string.Empty;

    public string Marca { get; set; } = string.Empty;

    public string AlfaTocoferolVitaminaEMg { get; set; } = string.Empty;

    public string AçúcarDeAdiçãoG { get; set; } = string.Empty;

    public string CarboidratoDisponívelG { get; set; } = string.Empty;

    public string CarboidratoTotalG { get; set; } = string.Empty;

    public string CinzasG { get; set; } = string.Empty;

    public string CobreMg { get; set; } = string.Empty;

    public string ColesterolMg { get; set; } = string.Empty;

    public string CálcioMg { get; set; } = string.Empty;

    public double? EnergiaKJ { get; set; }

    public double? EnergiaKcal { get; set; }

    public string EquivalenteDeFolatoMcg { get; set; } = string.Empty;

    public string FerroMg { get; set; } = string.Empty;

    public string FibraAlimentarG { get; set; } = string.Empty;

    public string FósforoMg { get; set; } = string.Empty;
    
    public string LipídiosG { get; set; } = string.Empty;

    public string MagnésioMg { get; set; } = string.Empty;

    public string ManganêsMg { get; set; } = string.Empty;

    public string NiacinaMg { get; set; } = string.Empty;

    public string PotássioMg { get; set; } = string.Empty;

    public string ProteínaG { get; set; } = string.Empty;

    public string RiboflavinaMg { get; set; } = string.Empty;

    public string SalDeAdiçãoG { get; set; } = string.Empty;

    public string SelênioMcg { get; set; } = string.Empty;

    public string SódioMg { get; set; } = string.Empty;

    public string TiaminaMg { get; set; } = string.Empty;

    public string UmidadeG { get; set; } = string.Empty;

    public string VitaminaARaeMcg { get; set; } = string.Empty;

    public string VitaminaAReMcg { get; set; } = string.Empty;

    public string VitaminaB12Mcg { get; set; } = string.Empty;

    public string VitaminaB6Mg { get; set; } = string.Empty;

    public string VitaminaCMg { get; set; } = string.Empty;

    public string VitaminaDMcg { get; set; } = string.Empty;

    public string ZincoMg { get; set; } = string.Empty;

    public string ÁcidosGraxosMonoinsaturadosG { get; set; } = string.Empty;

    public string ÁcidosGraxosPoliinsaturadosG { get; set; } = string.Empty;

    public string ÁcidosGraxosSaturadosG { get; set; } = string.Empty;

    public string ÁcidosGraxosTransG { get; set; } = string.Empty;

    public string ÁlcoolG { get; set; } = string.Empty;
}
