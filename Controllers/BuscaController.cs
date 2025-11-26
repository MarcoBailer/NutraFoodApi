using Microsoft.AspNetCore.Mvc;
using Nutra.Interfaces;

namespace Nutra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuscaController : ControllerBase
{
    private readonly IBusca _buscaService;

    public BuscaController(IBusca busca)
    {
        _buscaService = busca;
    }

    [HttpGet("{termo}")]
    public async Task<ActionResult> BuscarTudo(string termo)
    {
        if (string.IsNullOrWhiteSpace(termo) || termo.Length < 3)
            return BadRequest("Digite pelo menos 3 caracteres.");

        termo = termo.ToLower();

        var resultadoFinal = await _buscaService.BuscaAlimentoAsync(termo);

        return Ok(resultadoFinal);
    }
}
