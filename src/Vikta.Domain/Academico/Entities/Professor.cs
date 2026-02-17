
namespace Vikta.Domain.Academico.Entities;

public sealed class Professor
{
    public int Id { get; init; }
    public required string Nome { get; init; }
    public required ICollection<Turma> Turmas { get; init; }

    private Professor() { }
}
