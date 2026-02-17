using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vikta.Domain.Academico.Entities;

namespace Vikta.Data.Academico.Configurations;

public class MatriculaConfig : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.ToTable("Matricula");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Numero)
            .IsRequired();
        builder.HasOne(m => m.Aluno);
        builder.HasMany(m => m.Responsaveis)
            .WithMany(r => r.Matriculas)
            .UsingEntity(j => j.ToTable("MatriculaResponsavel"));
        builder.ComplexProperty(builder => builder.Endereco);
        builder.Property(m => m.Status)
            .HasConversion<int>()
            .IsRequired();
    }
}
