namespace Vikta.Domain.Academico.Entities;

public sealed class Turma
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public int Ano { get; private set; }
    public ICollection<Professor> Professores { get; private set; }
    public ICollection<Aluno> Alunos { get; private set; }

    private Turma()
    {
        Nome = string.Empty;
        Professores = [];
        Alunos = [];
    }

    public Turma(string nome, int ano)
    {
        Nome = nome;
        Ano = ano;
        Professores = [];
        Alunos = [];
    }

    public void Atualizar(string nome, int ano)
    {
        Nome = nome;
        Ano = ano;
    }
}
