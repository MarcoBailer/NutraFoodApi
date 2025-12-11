using Microsoft.AspNetCore.Identity;
using Nutra.Enum;

namespace Nutra.Models;

public class ApplicationUser : IdentityUser
{
    public string NomeCompleto { get; set; } = string.Empty;
    //public RoleType Role { get; set; } = RoleType.User;
    public string CPF { get; set; } = string.Empty;
    public virtual PerfilNutricional PerfilAtivo { get; set; }
}
