using Microsoft.EntityFrameworkCore;
using Nutra.Data;
using Nutra.Dtos;
using Nutra.Helper;
using Nutra.Interfaces;
using Nutra.Models;

namespace Nutra.Services
{
    public class BuscaService : IBusca
    {
        private readonly AlimentosContext _context;
        public BuscaService(AlimentosContext context)
        {
            _context = context;
        }
        public async Task<List<AlimentoResumoDto>> BuscaAlimentoAsync(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo)) return new List<AlimentoResumoDto>();

            termo = termo.ToLower();

            var taskTbca = _context.Tbcas
                .AsNoTracking()
                .Where(t => t.Nome != null && t.Nome.ToLower().Contains(termo))
                .Take(20)
                .ToListAsync();

            var taskFab = _context.Fabricantes
                .AsNoTracking()
                .Where(f => f.Produto != null && f.Produto.ToLower().Contains(termo))
                .Take(20)
                .ToListAsync();

            var taskFast = _context.FastFoods
                .AsNoTracking()
                .Where(ff => ff.Produto != null && ff.Produto.ToLower().Contains(termo))
                .Take(10)
                .ToListAsync();

            await Task.WhenAll(taskTbca, taskFab, taskFast);

            var resultadoFinal = new List<AlimentoResumoDto>();

            resultadoFinal.AddRange(taskTbca.Result.Select(MapTbcaToDto));
            resultadoFinal.AddRange(taskFab.Result.Select(MapFabricanteToDto));
            resultadoFinal.AddRange(taskFast.Result.Select(MapFastFoodToDto));

            return resultadoFinal.OrderBy(a => a.Nome.Length).ToList();

        }

        private AlimentoResumoDto MapTbcaToDto(Tbca t)
        {
            return new AlimentoResumoDto
            {
                Id = t.Id,
                Nome = t.Nome,
                NomeCientifico = t.NomeCientífico ?? "Desconhecido",
                MarcaFabricante = t.Marca,
                Grupo = t.Grupo ?? "Desconhecido",
                Fonte = "TBCA",
                Macros = new()
                {
                    EnergiaKcal = t.EnergiaKcal ?? 0,
                    EnergiaKJ = t.EnergiaKJ ?? 0,
                    Proteina = Conversor.LimparEConverter(t.ProteínaG),
                    CarboDisponivel = Conversor.LimparEConverter(t.CarboidratoDisponívelG),
                    Fibras = Conversor.LimparEConverter(t.FibraAlimentarG),
                    Acucar = Conversor.LimparEConverter(t.AçúcarDeAdiçãoG),
                    LipidiosG = Conversor.LimparEConverter(t.LipídiosG),
                    Umidade = Conversor.LimparEConverter(t.UmidadeG),
                    CarboTotal = Conversor.LimparEConverter(t.CarboidratoTotalG),
                    AlcoolG = Conversor.LimparEConverter(t.ÁlcoolG),
                },
                Minerais = new()
                {
                    ManganesMg = Conversor.LimparEConverter(t.ManganêsMg),
                    MagnesioMg = Conversor.LimparEConverter(t.MagnésioMg),
                    FosforoMg = Conversor.LimparEConverter(t.FósforoMg),
                    FerroMg = Conversor.LimparEConverter(t.FerroMg),
                    NiacinaMg = Conversor.LimparEConverter(t.NiacinaMg),
                    CalcioMg = Conversor.LimparEConverter(t.CálcioMg),
                    PotassioMg = Conversor.LimparEConverter(t.PotássioMg),
                    SelenioMcg = Conversor.LimparEConverter(t.SelênioMcg),
                    SodioMg = Conversor.LimparEConverter(t.SódioMg),
                    ZincoMg = Conversor.LimparEConverter(t.ZincoMg),
                    CobreMg = Conversor.LimparEConverter(t.CobreMg),
                    CinzasG = Conversor.LimparEConverter(t.CinzasG),
                },
                Vitaminas = new()
                {
                    VitaminaDMcg = Conversor.LimparEConverter(t.VitaminaDMcg),
                    VitaminaARaeMcg = Conversor.LimparEConverter(t.VitaminaARaeMcg),
                    VitaminaAReMcg = Conversor.LimparEConverter(t.VitaminaAReMcg),
                    VitaminaCMg = Conversor.LimparEConverter(t.VitaminaCMg),
                    VitaminaB12Mcg = Conversor.LimparEConverter(t.VitaminaB12Mcg),
                    VitaminaB6Mg = Conversor.LimparEConverter(t.VitaminaB6Mg),
                    RiboflavinaMg = Conversor.LimparEConverter(t.RiboflavinaMg),
                    TiaminaMg = Conversor.LimparEConverter(t.TiaminaMg),
                    AlfaTocoferolVitaminaEMg = Conversor.LimparEConverter(t.AlfaTocoferolVitaminaEMg),
                },
                Gorduras = new()
                {
                    Totais = Conversor.LimparEConverter(t.LipídiosG),
                    Saturadas = Conversor.LimparEConverter(t.ÁcidosGraxosSaturadosG),
                    Trans = Conversor.LimparEConverter(t.ÁcidosGraxosTransG),
                    ColesterolMg = Conversor.LimparEConverter(t.ColesterolMg),
                    Poliinsaturadas = Conversor.LimparEConverter(t.ÁcidosGraxosPoliinsaturadosG),
                    Monoinsaturadas = Conversor.LimparEConverter(t.ÁcidosGraxosMonoinsaturadosG),
                }
            };
        }

        private AlimentoResumoDto MapFabricanteToDto(Fabricante f)
        {
            return new AlimentoResumoDto
            {
                Id = f.Id,
                Nome = f.Produto ?? "Desconhecido",
                MarcaFabricante = f.Fabricante1 ?? "Genérico",
                PorcaoReferencia = f.Porcao ?? "Não informado",
                Fonte = "Fabricantes",
                Macros = new()
                {
                    EnergiaKcal = Conversor.LimparEConverter(f.EnergiaKcal),
                    EnergiaKJ = Conversor.LimparEConverter(f.EnergiaKj),
                    Proteina = Conversor.LimparEConverter(f.Proteinas),
                    CarboDisponivel = Conversor.LimparEConverter(f.Carboidratos),
                    LipidiosG = Conversor.LimparEConverter(f.Gorduras),
                    Acucar = Conversor.LimparEConverter(f.Acucar),
                    Fibras = Conversor.LimparEConverter(f.Fibras),
                },
                Minerais = new()
                {
                    SodioMg = Conversor.LimparEConverter(f.Sodio),
                    PotassioMg = Conversor.LimparEConverter(f.Potassio),
                },
                Gorduras = new()
                {
                    Totais = Conversor.LimparEConverter(f.Gorduras),
                    Saturadas = Conversor.LimparEConverter(f.GorduraSaturada),
                    ColesterolMg = Conversor.LimparEConverter(f.Colesterol),
                    Monoinsaturadas = Conversor.LimparEConverter(f.GorduraMonoinsaturada),
                    Poliinsaturadas = Conversor.LimparEConverter(f.GorduraPoliinsaturada),
                }
            };
        }

        private AlimentoResumoDto MapFastFoodToDto(FastFood ff)
        {
            return new AlimentoResumoDto
            {
                Id = ff.Id,
                Nome = ff.Produto ?? "Desconhecido",
                PorcaoReferencia = ff.Porcao ?? "Não informado",
                MarcaFabricante = ff.Fabricante ?? "Restaurante",
                Fonte = "FastFood",
                Macros = new()
                {
                    EnergiaKcal = Conversor.LimparEConverter(ff.EnergiaKcal),
                    EnergiaKJ = Conversor.LimparEConverter(ff.EnergiaKj),
                    Proteina = Conversor.LimparEConverter(ff.Proteinas),
                    CarboDisponivel = Conversor.LimparEConverter(ff.Carboidratos),
                    LipidiosG = Conversor.LimparEConverter(ff.Gorduras),
                    Acucar = Conversor.LimparEConverter(ff.Acucar),
                    Fibras = Conversor.LimparEConverter(ff.Fibras),
                },
                Minerais = new()
                {
                    SodioMg = Conversor.LimparEConverter(ff.Sodio),
                    PotassioMg = Conversor.LimparEConverter(ff.Potassio),
                },
                Gorduras = new()
                {
                    Totais = Conversor.LimparEConverter(ff.Gorduras),
                    Saturadas = Conversor.LimparEConverter(ff.GorduraSaturada),
                    ColesterolMg = Conversor.LimparEConverter(ff.Colesterol),
                    Monoinsaturadas = Conversor.LimparEConverter(ff.GorduraMonoinsaturada),
                    Poliinsaturadas = Conversor.LimparEConverter(ff.GorduraPoliinsaturada),
                }
            };
        }
    }
}
