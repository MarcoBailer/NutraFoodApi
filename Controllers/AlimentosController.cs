﻿using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<PaginatedResult<Fabricante>>> BuscarAlimentosFabricantesPorNome(
    string alimento_fabricante,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(alimento_fabricante))
        {
            return BadRequest("O termo de busca não pode ser vazio.");
        }

        var alimentosEncontrados = _context.Fabricantes.AsQueryable();

        var query = alimentosEncontrados.Where(a => a.Produto != null && a.Produto.Contains(alimento_fabricante));

        var totalCount = await query.CountAsync();
        
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (totalCount == 0)
        {
            return NotFound("Nenhum alimento encontrado com os termos informados.");
        }

        var result = new PaginatedResult<Fabricante>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            Items = items
        };

        return Ok(result);
    }

    [HttpGet("fastfood/alimento/{nome_fastfood}")]
    public async Task<ActionResult<PaginatedResult<FastFood>>> BuscarAlimentosFastFoodPorNome(
    string nome_fastfood,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(nome_fastfood))
        {
            return BadRequest("O termo de busca não pode ser vazio.");
        }

        var alimentosEncontrados = _context.FastFoods.AsQueryable();

        var query = alimentosEncontrados.Where(a => a.Produto != null && a.Produto.ToLower().Contains(nome_fastfood));

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        if (totalCount == 0)
        {
            return NotFound("Nenhum alimento encontrado com os termos informados.");
        }
        var result = new PaginatedResult<FastFood>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            Items = items
        };

        return Ok(result);
    }

    [HttpGet("tbca/alimento/{nome_tbca}")]
    public async Task<ActionResult<PaginatedResult<Tbca>>> BuscarAlimentosTbcaPorNome(
    string nome_tbca,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(nome_tbca))
        {
            return BadRequest("O termo de busca não pode ser vazio.");
        }

        var palavrasDeBusca = nome_tbca.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var query = _context.Tbcas.AsQueryable();

        foreach (var palavra in palavrasDeBusca)
        {
            query = query.Where(a => a.Nome != null && a.Nome.ToLower().Contains(palavra));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (totalCount == 0)
        {
            return NotFound("Nenhum alimento encontrado com os termos informados.");
        }

        var result = new PaginatedResult<Tbca>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            Items = items
        };

        return Ok(result);
    }
}