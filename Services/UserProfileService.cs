using Microsoft.AspNetCore.Identity;
using Nutra.Interfaces;
using Nutra.Models.Usuario;

namespace Nutra.Services;

public class UserProfileService : IUserProfile
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public UserProfileService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<PerfilNutricional> PostPerfilNutricional(PerfilNutricional perfilNutricional)
    {
        return perfilNutricional;
    }
}
