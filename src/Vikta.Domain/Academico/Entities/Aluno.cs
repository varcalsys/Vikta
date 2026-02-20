
using Vikta.Domain.Academico.ValueObjects;

namespace Vikta.Domain.Academico.Entities;

public class Aluno
{
    public int Id { get; init; }
    public string Nome { get; init; }
    public Cpf Cpf { get; init; }
    public int TurmaId { get; init; }

    public Turma Turma { get; init; }
    public ICollection<Responsavel> Responsaveis { get; init; }

    private Aluno() {}

    public Aluno(string nome, Cpf cpf, ICollection<Responsavel> responsaveis, int turmaId)
    {
        Nome = nome;
        Cpf = cpf;
        Responsaveis = responsaveis;
        TurmaId = turmaId;
    }
}
