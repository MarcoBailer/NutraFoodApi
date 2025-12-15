using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nutra.Data;
using Nutra.Enum;
using Nutra.Interfaces;
using Nutra.Models;
using Nutra.Models.Dtos;
using Nutra.Models.RegraNutricional;
using Nutra.Models.Usuario;

namespace Nutra.Services;

public class UserProfileService : IUserProfile
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICalculadoraNutricional _calculadora;
    private readonly IBusca _busca;
    private readonly AlimentosContext _context;

    public UserProfileService(UserManager<ApplicationUser> userManager,
        AlimentosContext context,
        ICalculadoraNutricional calculadora,
        IBusca busca
        )
    {
        _userManager = userManager;
        _context = context;
        _calculadora = calculadora;
        _busca = busca;
    }

    public async Task<RetornoPadrao> PostPerfilNutricional(PerfilNutricionalDto perfil)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var user = await _userManager.FindByIdAsync(perfil.UserId);
            if (user == null)
                throw new Exception("Usuário não encontrado.");

            bool perfilJaExiste = _context.PerfilNutricional.Any(p => p.UserId == perfil.UserId);
            if (perfilJaExiste)
                throw new Exception("Perfil nutricional já existe para este usuário.");

            var retorno = new RetornoPadrao();

            var registroInicial = new RegistroBiometrico
            {
                CircunferenciaCinturaCm = perfil.CircunferenciaCinturaCm,
                PercentualGordura = perfil.PercentualGorduraCorporal,
                PesoKg = perfil.PesoAtualKg,
                Data = DateTime.UtcNow
            };

            var novoPerfil = new PerfilNutricional
            {
                UserId = perfil.UserId,
                User = user,
                AlturaCm = perfil.AlturaCm,
                PesoAtualKg = perfil.PesoAtualKg,
                PercentualGorduraCorporal = perfil.PercentualGorduraCorporal,
                FatorAtividade = perfil.FatorAtividade,
                OcupacaoProfissional = perfil.OcupacaoProfissional,
                PossuiDoencasPreExistentes = perfil.PossuiDoencasPreExistentes,
                DescricaoCondicoesMedicas = perfil.DescricaoCondicoesMedicas,
                PesoDesejadoKg = perfil.PesoDesejadoKg,
                RefeicoesPorDiaDesejadas = perfil.RefeicoesPorDiaDesejadas,
                TempoDisponivelPreparoMinutos = perfil.TempoDisponivelPreparoMinutos,
                CircunferenciaCinturaCm = perfil.CircunferenciaCinturaCm,
                DataNascimento = perfil.DataNascimento,
                Genero = perfil.Genero,
                Objetivo = perfil.Objetivo,
                NivelAtividade = perfil.NivelAtividade,
                PreferenciaDieta = perfil.PreferenciaDieta,
                RestricoesAlimentares = perfil.RestricoesAlimentares,
                EquipamentoDisponivel = perfil.EquipamentosIds.Select(enumValue => new PerfilEquipamento { Equipamento = enumValue}).ToList(),
                HistoricoMedidas = new List<RegistroBiometrico>()
                {
                    registroInicial
                },
            };

            var metaCalculada = _calculadora.GerarMetaInicial(novoPerfil);

            novoPerfil.MetaNutricional = metaCalculada;

            metaCalculada.PerfilNutricional = novoPerfil;

            _context.PerfilNutricional.Add(novoPerfil);

            await _context.SaveChangesAsync();

            if (novoPerfil.MetaNutricionalAtualId == null)
            {
                novoPerfil.MetaNutricionalAtualId = novoPerfil.MetaNutricional.Id;
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            retorno.Sucesso = true;
            retorno.Mensagem = "Perfil nutricional criado com sucesso.";

            return retorno;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<RetornoPadrao> PostPreferenciaAlimentar(string userId, int id, ETipoTabela tabela, ETipoPreferencia afinidade)
    {
        var retorno = new RetornoPadrao();

        var perfil = await _context.PerfilNutricional
            .FirstOrDefaultAsync(p => p.UserId == userId);

        if(perfil == null)
        {
            retorno.Sucesso = false;
            retorno.Mensagem = "Perfil nutricional não encontrado para o usuário.";
            return retorno;
        }

        var alimento = await _busca.BuscaAlimentoPorIdAsync(id, tabela);

        if(alimento == null)
        {
            retorno.Sucesso = false;
            retorno.Mensagem = "Alimento não encontrado.";
            return retorno;
        }

        var preferencia = new PreferenciaAlimentar
        {
            PerfilNutricionalId = perfil.Id,
            AlimentoId = alimento.Id,
            Tabela = tabela,
            Tipo = afinidade
        };

        _context.PreferenciaAlimentar.Add(preferencia);
        await _context.SaveChangesAsync();

        retorno.Sucesso = true;
        retorno.Mensagem = "Preferência alimentar registrada com sucesso.";
        return retorno;
    }
}
