using GuiaPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuiaPlus.Infrastructure.Data.Configurations;
public class GuiaConfiguration : IEntityTypeConfiguration<Guia>
{
    public void Configure(EntityTypeBuilder<Guia> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.NumeroGuia)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(g => g.Status)
            .IsRequired();

        builder.Property(g => g.DataHoraRegistro)
            .IsRequired();

        builder.Property(g => g.DataHoraIniciouColeta)
            .IsRequired(false);

        builder.Property(g => g.DataHoraConfirmouRetirada)
            .IsRequired(false);

        builder.HasOne(g => g.Cliente)
            .WithMany()                  
            .HasForeignKey(g => g.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Servico)
            .WithMany()
            .HasForeignKey(g => g.ServicoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.ClienteEndereco)
            .WithMany()
            .HasForeignKey(g => g.ClienteEnderecoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Para ajudar com a perfomace de busca por número de guia
        builder.HasIndex(g => g.NumeroGuia)
            .IsUnique(); // O número da guia deve ser único no sistema
    }
}

