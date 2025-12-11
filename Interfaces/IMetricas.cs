using Nutra.Models.Dtos;
using Nutra.Models.Usuario;

namespace Nutra.Interfaces;

public interface IMetricas
{
    BiometriaDto CalcularMetas(ApplicationUser user);
}
