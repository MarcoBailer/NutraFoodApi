using Nutra.Models.Usuario;

namespace Nutra.Interfaces;

public interface IMetricas
{
    MetaNutricional CalcularMetas(ApplicationUser user);
}
