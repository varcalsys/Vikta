using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vikta.Domain.Academico.Entities;
using Vikta.Domain.Academico.ValueObjects;

namespace Vikta.Data.Academico.Configurations;

internal class ResponsavelConfig : IEntityTypeConfiguration<Responsavel>
{
    public void Configure(EntityTypeBuilder<Responsavel> builder)
    {
        builder.ToTable("Responsavel");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Nome)
            .IsRequired()
            .HasMaxLength(100);


        builder.Property(r => r.Cpf)
            .HasConversion(
                cpf => cpf.Numero,
                value => new Cpf(value))
            .HasColumnName("Cpf")
            .IsRequired()
            .HasMaxLength(11);


        builder.HasMany(r => r.Alunos)
                .WithMany(a => a.Responsaveis)
                .UsingEntity(j => j.ToTable("ResponsavelAluno"));

    }
}
