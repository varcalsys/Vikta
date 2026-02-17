namespace Vikta.Application.Academico.Requests
{
    public record MatricularAlunoRequest(
    int Numero,
    AlunoRequest Aluno,
    IReadOnlyCollection<ResponsavelRequest> Responsaveis,
    EnderecoRequest Endereco);

    public record AlunoRequest(string Nome, string Cpf);

    public record ResponsavelRequest(string Nome, string Cpf, bool Principal);

    public record EnderecoRequest(
        string Logradouro,
        string Numero,
        string? Complemento,
        string Bairro,
        string Cep);
}
