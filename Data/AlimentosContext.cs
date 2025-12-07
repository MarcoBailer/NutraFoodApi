using Microsoft.EntityFrameworkCore;
using Nutra.Models;

namespace Nutra.Data;

public class AlimentosContext : DbContext
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
