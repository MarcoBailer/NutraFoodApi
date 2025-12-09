using Nutra.Models;
using Nutra.Models.Dtos;

namespace Nutra.Interfaces;

public interface IMetricas
{
    BiometriaDto CalcularMetas(ApplicationUser user);
}
