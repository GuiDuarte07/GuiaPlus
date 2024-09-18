using Microsoft.EntityFrameworkCore;
using GuiaPlus.Domain.Entities;

namespace GuiaPlus.Infrastructure.Data.AppDbContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Servico> Servicos { get; set; }
    public DbSet<ClienteEndereco> ClienteEnderecos { get; set; }
    public DbSet<Guia> Guias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
