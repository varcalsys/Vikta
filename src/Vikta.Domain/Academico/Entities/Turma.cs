
namespace Vikta.Domain.Academico.Entities;

public sealed class Turma
{
    public int Id { get; init; }
    public required string Nome { get; init; }
    public int Ano { get; init; }
    public required ICollection<Professor> Professores { get; init; }
    public required ICollection<Aluno> Alunos { get; init; }

    private Turma() { }
}
