using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vikta.Domain.Academico.Entities;

namespace Vikta.Data.Academico.Configurations;

public class ProfessorConfig : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder.ToTable("Professor");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasMany(p => p.Turmas)
            .WithMany(t => t.Professores)
            .UsingEntity(j => j.ToTable("TurmaProfessor"));
    }
}

