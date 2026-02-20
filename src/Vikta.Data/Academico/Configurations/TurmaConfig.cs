using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vikta.Domain.Academico.Entities;

namespace Vikta.Data.Academico.Configurations;

internal class TurmaConfig : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("Turma");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Nome)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(t => t.Professores)
            .WithMany(p => p.Turmas)
            .UsingEntity(j => j.ToTable("TurmaProfessor")); 
    }
}
