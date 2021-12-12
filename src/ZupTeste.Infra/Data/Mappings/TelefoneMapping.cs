using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZupTeste.Domain.Funcionarios;

namespace ZupTeste.Infra.Data.Mappings;

public class TelefoneMapping : IEntityTypeConfiguration<Telefone>
{
    public void Configure(EntityTypeBuilder<Telefone> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .Property(x => x.Numero)
            .IsRequired()
            .HasColumnType("varchar(20)")
            .HasMaxLength(20);
            
        builder.HasOne(x => x.Funcionario)
            .WithMany(x => x.Telefones)
            .HasForeignKey(x => x.FuncionarioId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}