using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZupTeste.Domain.Funcionarios;

namespace ZupTeste.Infra.Data.Mappings;

public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnType("varchar(128)")
            .HasMaxLength(128);
            
        builder.Property(x => x.Sobrenome)
            .IsRequired()
            .HasColumnType("varchar(256)")
            .HasMaxLength(256);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("varchar(256)")
            .HasMaxLength(256);

        builder
            .Property(x => x.NumeroChapa)
            .HasColumnType("varchar(30)")
            .HasMaxLength(30)
            .IsRequired();
        
        builder
            .Property(x => x.Senha)
            .HasColumnType("varchar(512)")
            .HasMaxLength(512)
            .IsRequired();


        // Relationships

        builder.HasOne(x => x.Lider)
            .WithMany(x => x.Funcionarios)
            .HasForeignKey(x => x.LiderId);
        
        
        // Indexes

        builder.HasIndex(x => x.NumeroChapa)
            .IsUnique();
    }
}