using GuiaPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuiaPlus.Infrastructure.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CPF_CNPJ)
            .IsRequired()
            .HasMaxLength(14);
            

        builder.Property(c => c.NomeCompleto)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Email)
            .HasMaxLength(100);

        builder.Property(c => c.Telefone)
            .HasMaxLength(15);

        builder.Property(c => c.Status)
            .IsRequired();

        builder.HasMany(c => c.ClienteEnderecos)
            .WithOne(ce => ce.Cliente)
            .HasForeignKey(ce => ce.ClienteId);
    }
}
