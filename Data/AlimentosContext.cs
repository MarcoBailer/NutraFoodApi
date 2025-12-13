using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nutra.Models.Alimentos;
using Nutra.Models.Usuario;

namespace Nutra.Data;

public class AlimentosContext : IdentityDbContext<ApplicationUser>
{
    public AlimentosContext(DbContextOptions<AlimentosContext> options)
        : base(options)
    {
    }

    public DbSet<Fabricantes> Fabricantes { get; set; }

    public DbSet<FastFood> FastFoods { get; set; }

    public DbSet<Tbca> Tbcas { get; set; }

    public DbSet<Genericos> Genericos { get; set; }

}
