using GuiaPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuiaPlus.Infrastructure.Data.Configurations;
public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
{
    public void Configure(EntityTypeBuilder<Servico> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Nome)
            .IsRequired()
            .HasMaxLength(100);
    }
}
