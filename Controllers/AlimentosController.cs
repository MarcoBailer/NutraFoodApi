using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutra.Models;

[ApiController]
[Route("api/[controller]")]
public class AlimentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public AlimentosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("fabricante/alimento/{alimento_fabricante}")]
    public async Task<ActionResult<IEnumerable<Fabricante>>> BuscarAlimentosFabricantesPorNome(string alimento_fabricante)
    {
        if (string.IsNullOrWhiteSpace(alimento_fabricante))
        {
            return BadRequest("O termo de busca não pode ser vazio.");
        }

        var alimentosEncontrados = await _context.Fabricantes
            .Where(a => a.Produto != null && a.Produto.StartsWith(alimento_fabricante))
            .ToListAsync();

        if (!alimentosEncontrados.Any())
        {
            return NotFound("Nenhum alimento encontrado com o termo informado.");
        }

        return Ok(alimentosEncontrados);
    }

    [HttpGet("fastfood/alimento/{nome_fastfood}")]
    public async Task<ActionResult<IEnumerable<FastFood>>> BuscarAlimentosFastFoodPorNome(string nome_fastfood)
    {
        if (string.IsNullOrWhiteSpace(nome_fastfood))
        {
            return BadRequest("O termo de busca não pode ser vazio.");
        }

        var alimentosEncontrados = await _context.FastFoods
            .Where(a => a.Produto != null && a.Produto.StartsWith(nome_fastfood))
            .ToListAsync();

        if (!alimentosEncontrados.Any())
        {
            return NotFound("Nenhum alimento encontrado com o termo informado.");
        }

        return Ok(alimentosEncontrados);
    }
}