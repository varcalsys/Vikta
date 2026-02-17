using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vikta.Domain.Academico.Entities;
using Vikta.Domain.Academico.ValueObjects;

namespace Vikta.Data.Academico.Configurations;

public class AlunoConfig : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("Aluno");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.Cpf)
             .HasConversion(
                 cpf => cpf.Numero,
                 value => new Cpf(value))
             .HasColumnName("Cpf")
             .IsRequired()
             .HasMaxLength(11);

        builder.HasOne(a => a.Turma)
            .WithMany(t => t.Alunos)
            .HasForeignKey(a => a.TurmaId)
            .OnDelete(DeleteBehavior.SetNull);
  
        builder.HasMany(a => a.Responsaveis)
            .WithMany(r => r.Alunos);
    }
}
