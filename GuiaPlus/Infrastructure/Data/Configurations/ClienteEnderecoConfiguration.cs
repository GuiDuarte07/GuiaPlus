using GuiaPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuiaPlus.Infrastructure.Data.Configurations;
public class ClienteEnderecoConfiguration : IEntityTypeConfiguration<ClienteEndereco>
{
    public void Configure(EntityTypeBuilder<ClienteEndereco> builder)
    {

        builder.HasKey(ce => ce.Id);

        builder.Property(ce => ce.CEP)
            .IsRequired()
            .HasMaxLength(8);

        builder.Property(ce => ce.Logradouro)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(ce => ce.Bairro)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ce => ce.Cidade)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ce => ce.Complemento)
            .HasMaxLength(255);

        builder.Property(ce => ce.Numero)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(ce => ce.Latitude)
            .IsRequired();

        builder.Property(ce => ce.Longitude)
           .IsRequired();

        builder.HasOne(ce => ce.Cliente)
            .WithMany(c => c.ClienteEnderecos)
            .HasForeignKey(ce => ce.ClienteId);
    }
}