using Vikta.Domain.Academico.Enums;
using Vikta.Domain.Academico.ValueObjects;

namespace Vikta.Domain.Academico.Entities;

public class Matricula
{
    public int Id { get; set; }
    public int Numero { get; init; }
    public Aluno Aluno { get; init; }
    public ICollection<Responsavel> Responsaveis { get; init; }
    public Endereco Endereco { get; init; }
    public StatusMatricula Status { get; init; }

    private Matricula() { }

    public Matricula(int numero, Aluno aluno, ICollection<Responsavel> responsaveis, Endereco endereco)
    {
        Numero = numero;
        Aluno = aluno;
        Responsaveis = responsaveis;
        Endereco = endereco;
        Status = StatusMatricula.Ativa;
    }
}
