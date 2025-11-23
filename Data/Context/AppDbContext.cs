using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Nutra.Models;

namespace Nutra.Data.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fabricantes> Fabricantes { get; set; }
    public virtual DbSet<FastFood> FastFoods { get; set; }
    public virtual DbSet<Tbca> Tbcas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=Data/alimentos.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fabricantes>(entity =>
        {
            entity
                .ToTable("fabricantes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("rowid");

            entity.Property(e => e.EnergiaKcal).HasColumnName("Energia_kcal");
            entity.Property(e => e.EnergiaKj).HasColumnName("Energia_kj");
            entity.Property(e => e.Fabricante).HasColumnName("Fabricante");
            entity.Property(e => e.GorduraMonoinsaturada).HasColumnName("Gordura_Monoinsaturada");
            entity.Property(e => e.GorduraPoliinsaturada).HasColumnName("Gordura_Poliinsaturada");
            entity.Property(e => e.GorduraSaturada).HasColumnName("Gordura_Saturada");
            entity.Property(e => e.GorduraTrans).HasColumnName("Gordura_Trans");
        });

        modelBuilder.Entity<FastFood>(entity =>
        {
            entity
                .ToTable("fast_food");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("rowid");

            entity.Property(e => e.EnergiaKcal).HasColumnName("Energia_kcal");
            entity.Property(e => e.EnergiaKj).HasColumnName("Energia_kj");
            entity.Property(e => e.GorduraMonoinsaturada).HasColumnName("Gordura_Monoinsaturada");
            entity.Property(e => e.GorduraPoliinsaturada).HasColumnName("Gordura_Poliinsaturada");
            entity.Property(e => e.GorduraSaturada).HasColumnName("Gordura_Saturada");
            entity.Property(e => e.GorduraTrans).HasColumnName("Gordura_Trans");
        });

        modelBuilder.Entity<Tbca>(entity =>
        {
            entity
                .ToTable("tbca");
            entity.HasKey(e => e.Código);
            entity.Property(e => e.Código).HasColumnName("Código");

            entity.Property(e => e.AlfaTocoferolVitaminaEMg).HasColumnName("Alfa-tocoferol_Vitamina_E_mg");
            entity.Property(e => e.AçúcarDeAdiçãoG).HasColumnName("Açúcar_de_adição_g");
            entity.Property(e => e.CarboidratoDisponívelG).HasColumnName("Carboidrato_disponível_g");
            entity.Property(e => e.CarboidratoTotalG).HasColumnName("Carboidrato_total_g");
            entity.Property(e => e.CinzasG).HasColumnName("Cinzas_g");
            entity.Property(e => e.CobreMg).HasColumnName("Cobre_mg");
            entity.Property(e => e.ColesterolMg).HasColumnName("Colesterol_mg");
            entity.Property(e => e.CálcioMg).HasColumnName("Cálcio_mg");
            entity.Property(e => e.EnergiaKJ).HasColumnName("Energia_kJ");
            entity.Property(e => e.EnergiaKcal).HasColumnName("Energia_kcal");
            entity.Property(e => e.EquivalenteDeFolatoMcg).HasColumnName("Equivalente_de_folato_mcg");
            entity.Property(e => e.FerroMg).HasColumnName("Ferro_mg");
            entity.Property(e => e.FibraAlimentarG).HasColumnName("Fibra_alimentar_g");
            entity.Property(e => e.FósforoMg).HasColumnName("Fósforo_mg");
            entity.Property(e => e.LipídiosG).HasColumnName("Lipídios_g");
            entity.Property(e => e.MagnésioMg).HasColumnName("Magnésio_mg");
            entity.Property(e => e.ManganêsMg).HasColumnName("Manganês_mg");
            entity.Property(e => e.NiacinaMg).HasColumnName("Niacina_mg");
            entity.Property(e => e.NomeCientífico).HasColumnName("Nome_Científico");
            entity.Property(e => e.PotássioMg).HasColumnName("Potássio_mg");
            entity.Property(e => e.ProteínaG).HasColumnName("Proteína_g");
            entity.Property(e => e.RiboflavinaMg).HasColumnName("Riboflavina_mg");
            entity.Property(e => e.SalDeAdiçãoG).HasColumnName("Sal_de_adição_g");
            entity.Property(e => e.SelênioMcg).HasColumnName("Selênio_mcg");
            entity.Property(e => e.SódioMg).HasColumnName("Sódio_mg");
            entity.Property(e => e.TiaminaMg).HasColumnName("Tiamina_mg");
            entity.Property(e => e.UmidadeG).HasColumnName("Umidade_g");
            entity.Property(e => e.VitaminaARaeMcg).HasColumnName("Vitamina_A_RAE_mcg");
            entity.Property(e => e.VitaminaAReMcg).HasColumnName("Vitamina_A_RE_mcg");
            entity.Property(e => e.VitaminaB12Mcg).HasColumnName("Vitamina_B12_mcg");
            entity.Property(e => e.VitaminaB6Mg).HasColumnName("Vitamina_B6_mg");
            entity.Property(e => e.VitaminaCMg).HasColumnName("Vitamina_C_mg");
            entity.Property(e => e.VitaminaDMcg).HasColumnName("Vitamina_D_mcg");
            entity.Property(e => e.ZincoMg).HasColumnName("Zinco_mg");
            entity.Property(e => e.ÁcidosGraxosMonoinsaturadosG).HasColumnName("Ácidos_graxos_monoinsaturados_g");
            entity.Property(e => e.ÁcidosGraxosPoliinsaturadosG).HasColumnName("Ácidos_graxos_poliinsaturados_g");
            entity.Property(e => e.ÁcidosGraxosSaturadosG).HasColumnName("Ácidos_graxos_saturados_g");
            entity.Property(e => e.ÁcidosGraxosTransG).HasColumnName("Ácidos_graxos_trans_g");
            entity.Property(e => e.ÁlcoolG).HasColumnName("Álcool_g");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
