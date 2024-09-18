using GuiaPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuiaPlus.Infrastructure.Data.Configurations;
public class GuiaConfiguration : IEntityTypeConfiguration<Guia>
{
    public void Configure(EntityTypeBuilder<Guia> builder)
    {
        builder.HasKey(g => g.Id);

        builder.HasOne(g => g.Cliente)
            .WithMany()
            .HasForeignKey(g => g.ClienteId);

        builder.HasOne(g => g.Servico)
            .WithMany()
            .HasForeignKey(g => g.ServicoId);

        builder.HasOne(g => g.ClienteEndereco)
            .WithMany()
            .HasForeignKey(g => g.ClienteEnderecoId);
    }
}

