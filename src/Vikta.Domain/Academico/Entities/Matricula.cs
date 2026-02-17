using Vikta.Domain.Academico.Enums;
using Vikta.Domain.Academico.ValueObjects;

namespace Vikta.Domain.Academico.Entities;

public class Matricula
{
    public int Id { get; init; }
    public int Numero { get; init; }
    public int AlunoId { get; init; }
    public int TurmaId { get; init; }
    public Endereco Endereco { get; init; }
    public StatusMatricula Status { get; init; }


    public Aluno Aluno { get; init; }
    public Turma Turma { get; init; }
    public ICollection<Responsavel> Responsaveis { get; init; }
    

    private Matricula() { }

    public Matricula(int numero, int turmaId, Aluno aluno, ICollection<Responsavel> responsaveis, Endereco endereco)
    {
        Numero = numero;
        TurmaId = turmaId;
        Aluno = aluno;
        Responsaveis = responsaveis;
        Endereco = endereco;
        Status = StatusMatricula.Ativa;
    }
}
