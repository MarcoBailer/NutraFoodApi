
using Nutra.Dtos;

namespace Nutra.Interfaces
{
    public interface IBusca
    {
        Task<List<AlimentoResumoDto>> BuscaAlimentoAsync(string termo);
    }
}
