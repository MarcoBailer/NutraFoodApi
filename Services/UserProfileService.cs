using Microsoft.AspNetCore.Identity;
using Nutra.Data;
using Nutra.Interfaces;
using Nutra.Models;
using Nutra.Models.Dtos;
using Nutra.Models.Usuario;

namespace Nutra.Services;

public class UserProfileService : IUserProfile
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICalculadoraNutricional _calculadora;
    private readonly AlimentosContext _context;

    public UserProfileService(UserManager<ApplicationUser> userManager,
        AlimentosContext context,
        ICalculadoraNutricional calculadora
        )
    {
        _userManager = userManager;
        _context = context;
        _calculadora = calculadora;
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
                PreferenciasAlimentares = perfil.PreferenciasAlimentares,
                EquipamentoDisponivel = perfil.EquipamentosIds.Select(enumValue => new PerfilEquipamento { Equipamento = enumValue}).ToList(),
                HistoricoMedidas = new List<RegistroBiometrico>()
                {
                    registroInicial
                },
            };

            novoPerfil.HistoricoMedidas.Add(registroInicial);

            var metaCalculada = _calculadora.GerarMetaInicial(novoPerfil);

            novoPerfil.MetaNutricional = metaCalculada;

            _context.PerfilNutricional.Add(novoPerfil);

            novoPerfil.MetaNutricionalAtualId = metaCalculada.Id;

            _context.Entry(novoPerfil).Property(p => p.MetaNutricionalAtualId).IsModified = true;

            await _context.SaveChangesAsync();

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
}
