using Vikta.Domain.Academico.ValueObjects;

namespace Vikta.Domain.Academico.Entities;

public class Responsavel
{
    public int Id { get; init; }
    public string Nome { get; init; }
    public Cpf Cpf { get; init; }
    public bool Principal { get; init; }

    public  ICollection<Aluno> Alunos { get; init; }
    public ICollection<Matricula> Matriculas { get; set; }


    private Responsavel() { }

    public Responsavel(string nome, Cpf cpf, bool principal)
    {
        Nome = nome;
        Cpf = cpf;
        Principal = principal;
    }
}
