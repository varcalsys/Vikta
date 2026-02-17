using Microsoft.EntityFrameworkCore;
using Vikta.Data.Academico.Configurations;
using Vikta.Domain.Academico.Entities;


namespace Vikta.Data.Academico;

public class AcademicoDbContext : DbContext
{
    public AcademicoDbContext(DbContextOptions<AcademicoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Matricula> Matriculas => Set<Matricula>();
    public DbSet<Professor> Professores => Set<Professor>();
    public DbSet<Responsavel> Reponsaveis => Set<Responsavel>();
    public DbSet<Turma> Turmas => Set<Turma>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AlunoConfig());
        modelBuilder.ApplyConfiguration(new MatriculaConfig());
        modelBuilder.ApplyConfiguration(new ProfessorConfig());
        modelBuilder.ApplyConfiguration(new ResponsavelConfig());
        modelBuilder.ApplyConfiguration(new TurmaConfig());
        base.OnModelCreating(modelBuilder);
    }
}
