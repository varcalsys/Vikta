
namespace Vikta.Domain.Academico.ValueObjects;

public record Endereco
{
    public required string Logradouro { get; init; }
    public required string Numero { get; init; }
    public string? Complemento { get; init; }
    public required string Bairro { get; init; }
    public required Cep Cep { get; init; }

    public Endereco()
    {

    }

}

public record Cep
{
    public required string Numero { get; init; }

    public Cep()
    {

    }
}

