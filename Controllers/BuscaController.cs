using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutra.Data.Context;
using Nutra.Dtos;
using System.Globalization;

namespace Nutra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuscaController : ControllerBase
{
    private readonly AppDbContext _context;

    public BuscaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{termo}")]
    public async Task<ActionResult<IEnumerable<AlimentoResumoDto>>> BuscarTudo(string termo)
    {
        if (string.IsNullOrWhiteSpace(termo) || termo.Length < 3)
            return BadRequest("Digite pelo menos 3 caracteres.");

        termo = termo.ToLower();

        var rawTbca = await _context.Tbcas
            .AsNoTracking()
            .Where(t => t.Nome.ToLower().Contains(termo))
            .Take(20)
            .ToListAsync();

        var dtosTbca = rawTbca.Select(t => new AlimentoResumoDto
        {
            Id = int.TryParse(t.Código, out int id) ? id : t.Código.GetHashCode(),
            Nome = t.Nome,
            NomeCientifico = t.NomeCientífico ?? "Desconhecido",
            MarcaFabricante = t.Marca,
            Grupo = t.Grupo ?? "Desconhecido",
            Fonte = "TBCA",
            Macros = new()
            {
                EnergiaKcal = t.EnergiaKcal ?? 0,
                EnergiaKJ = t.EnergiaKJ ?? 0,
                Proteina = ParseToDouble(t.ProteínaG),
                CarboDisponivel = ParseToDouble(t.CarboidratoDisponívelG),
                Fibras = ParseToDouble(t.FibraAlimentarG),
                Acucar = ParseToDouble(t.AçúcarDeAdiçãoG),
                LipidiosG = ParseToDouble(t.LipídiosG),
                Umidade = ParseToDouble(t.UmidadeG),
                CarboTotal = ParseToDouble(t.CarboidratoTotalG),
                AlcoolG = ParseToDouble(t.ÁlcoolG),
            },
            Minerais = new()
            {
                ManganesMg = ParseToDouble(t.ManganêsMg),
                MagnesioMg = ParseToDouble(t.MagnésioMg),
                FosforoMg = ParseToDouble(t.FósforoMg),
                FerroMg = ParseToDouble(t.FerroMg),
                NiacinaMg = ParseToDouble(t.NiacinaMg),
                CalcioMg = ParseToDouble(t.CálcioMg),
                PotassioMg = ParseToDouble(t.PotássioMg),
                SelenioMcg = ParseToDouble(t.SelênioMcg),
                SodioMg = ParseToDouble(t.SódioMg),
                ZincoMg = ParseToDouble(t.ZincoMg),
                CobreMg = ParseToDouble(t.CobreMg),
                CinzasG = ParseToDouble(t.CinzasG),
            },
            Vitaminas = new()
            {
                VitaminaDMcg = ParseToDouble(t.VitaminaDMcg),
                VitaminaARaeMcg = ParseToDouble(t.VitaminaARaeMcg),
                VitaminaAReMcg = ParseToDouble(t.VitaminaAReMcg),
                VitaminaCMg = ParseToDouble(t.VitaminaCMg),
                VitaminaB12Mcg = ParseToDouble(t.VitaminaB12Mcg),
                VitaminaB6Mg = ParseToDouble(t.VitaminaB6Mg),
                RiboflavinaMg = ParseToDouble(t.RiboflavinaMg),
                TiaminaMg = ParseToDouble(t.TiaminaMg),
                AlfaTocoferolVitaminaEMg = ParseToDouble(t.AlfaTocoferolVitaminaEMg),
            },
            Gorduras = new()
            {
                Totais = ParseToDouble(t.LipídiosG),
                Saturadas = ParseToDouble(t.ÁcidosGraxosSaturadosG),
                Trans = ParseToDouble(t.ÁcidosGraxosTransG),
                ColesterolMg = ParseToDouble(t.ColesterolMg),
                Poliinsaturadas = ParseToDouble(t.ÁcidosGraxosPoliinsaturadosG),
                Monoinsaturadas = ParseToDouble(t.ÁcidosGraxosMonoinsaturadosG),
            }
        });

        var rawFab = await _context.Fabricantes
            .AsNoTracking()
            .Where(f => f.Produto != null && f.Produto.ToLower().Contains(termo))
            .Take(20)
            .ToListAsync();

        var dtosFab = rawFab.Select(f => new AlimentoResumoDto
        {
            Id = 0,
            Nome = f.Produto ?? "Desconhecido",
            MarcaFabricante = f.Fabricante ?? "Genérico",
            PorcaoReferencia = f.Porcao ?? "Não informado",
            Fonte = "Industrializado",
            Macros = new()
            {
                EnergiaKcal = ParseToDouble(f.EnergiaKcal),
                EnergiaKJ = ParseToDouble(f.EnergiaKj),
                Proteina = ParseToDouble(f.Proteinas),
                CarboDisponivel = ParseToDouble(f.Carboidratos),
                LipidiosG = ParseToDouble(f.Gorduras),
                Acucar = ParseToDouble(f.Acucar),
                Fibras = ParseToDouble(f.Fibras),
            },
            Minerais = new()
            {
                SodioMg = ParseToDouble(f.Sodio),
                PotassioMg = ParseToDouble(f.Potassio),
            },
            Gorduras = new()
            {
                Totais = ParseToDouble(f.Gorduras),
                Saturadas = ParseToDouble(f.GorduraSaturada),
                ColesterolMg = ParseToDouble(f.Colesterol),
                Monoinsaturadas = ParseToDouble(f.GorduraMonoinsaturada),
                Poliinsaturadas = ParseToDouble(f.GorduraPoliinsaturada),
                Trans = ParseToDouble(f.GorduraTrans),
            }
        });

        var rawFast = await _context.FastFoods
            .AsNoTracking()
            .Where(ff => ff.Produto != null && ff.Produto.ToLower().Contains(termo))
            .Take(10)
            .ToListAsync();

        var dtosFast = rawFast.Select(ff => new AlimentoResumoDto
        {
            Id = 0,
            Nome = ff.Produto ?? "Desconhecido",
            PorcaoReferencia = ff.Porcao ?? "Não informado",
            MarcaFabricante = ff.Fabricante ?? "Restaurante",
            Fonte = "FastFood",
            Macros = new()
            {
                EnergiaKcal = ParseToDouble(ff.EnergiaKcal),
                EnergiaKJ = ParseToDouble(ff.EnergiaKj),
                Proteina = ParseToDouble(ff.Proteinas),
                CarboDisponivel = ParseToDouble(ff.Carboidratos),
                LipidiosG = ParseToDouble(ff.Gorduras),
                Acucar = ParseToDouble(ff.Acucar),
                Fibras = ParseToDouble(ff.Fibras),
            },
            Minerais = new()
            {
                SodioMg = ParseToDouble(ff.Sodio),
                PotassioMg = ParseToDouble(ff.Potassio),
            },
            Gorduras = new()
            {
                Totais = ParseToDouble(ff.Gorduras),
                Saturadas = ParseToDouble(ff.GorduraSaturada),
                ColesterolMg = ParseToDouble(ff.Colesterol),
                Monoinsaturadas = ParseToDouble(ff.GorduraMonoinsaturada),
                Poliinsaturadas = ParseToDouble(ff.GorduraPoliinsaturada),
                Trans = ParseToDouble(ff.GorduraTrans),
            }
        });

        var resultadoFinal = new List<AlimentoResumoDto>();
        resultadoFinal.AddRange(dtosTbca);
        resultadoFinal.AddRange(dtosFab);
        resultadoFinal.AddRange(dtosFast);

        return Ok(resultadoFinal.OrderBy(x => x.Nome.Length));
    }
    private double ParseToDouble(string? valor)
    {
        if (string.IsNullOrWhiteSpace(valor)) return 0;

        var v = valor.Trim().ToLower();

        if (v == "na" || v == "n/a" || v == "tr" || v == "*") return 0;

        if (double.TryParse(v, NumberStyles.Any, new CultureInfo("pt-BR"), out double resultPt))
        {
            return resultPt;
        }

        if (double.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out double resultEn))
        {
            return resultEn;
        }

        return 0;
    }
}
