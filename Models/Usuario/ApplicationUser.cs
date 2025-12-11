using Microsoft.AspNetCore.Identity;

namespace Nutra.Models.Usuario;

public class ApplicationUser : IdentityUser
{
    public string NomeCompleto { get; set; } = string.Empty;
    //public RoleType Role { get; set; } = RoleType.User;
    public string CPF { get; set; } = string.Empty;
    public PerfilNutricional PerfilAtivo { get; set; }
}
