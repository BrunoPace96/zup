using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZupTeste.Core.Utils;
using ZupTeste.Domain.Administradores;

namespace ZupTeste.Infra.Data.Mappings;

public class AdministradorMapping : IEntityTypeConfiguration<Administrador>
{
    public void Configure(EntityTypeBuilder<Administrador> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnType("varchar(128)")
            .HasMaxLength(128);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("varchar(256)")
            .HasMaxLength(256);

        builder
            .Property(x => x.Senha)
            .HasColumnType("varchar(512)")
            .HasMaxLength(512)
            .IsRequired();
    }
}