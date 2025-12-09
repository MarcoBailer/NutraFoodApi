using Microsoft.AspNetCore.Identity;

namespace Nutra.Models;

public class ApplicationUser : IdentityUser
{
    public DateTime DataNascimento { get; set; }
    public string Genero { get; set; } = string.Empty;
    public double PesoAtual { get; set; }
    public double AlturaCm { get; set; }

    public double FatorAtividade { get; set; }

    public string Objetivo { get; set; } = "Manutenção";
}
