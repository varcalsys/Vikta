
namespace Vikta.Domain.Academico.ValueObjects
{
    public record Cpf
    {
        public string Numero { get; init; }

        private Cpf()
        {
            Numero = string.Empty;
        }

        public Cpf(string numero)
        {
            Numero = numero;
        }
    }
}
