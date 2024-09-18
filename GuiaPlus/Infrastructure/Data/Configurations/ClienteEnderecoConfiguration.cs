using GuiaPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuiaPlus.Infrastructure.Data.Configurations;
public class ClienteEnderecoConfiguration : IEntityTypeConfiguration<ClienteEndereco>
{
    public void Configure(EntityTypeBuilder<ClienteEndereco> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Cliente)
            .WithMany()
            .HasForeignKey(e => e.ClienteId);
    }
}
