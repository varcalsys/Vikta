using Vikta.Domain.Common;

namespace Vikta.Domain.Academico.Rules;

public static class MatriculaRules
{
    public static void ValidarResponsaveis(int quantidadeResponsaveis, int quantidadePrincipais)
    {
        if (quantidadeResponsaveis > 3)
        {
            throw new BusinessRuleException("Uma matricula permite no maximo 3 responsaveis.");
        }

        if (quantidadePrincipais != 1)
        {
            throw new BusinessRuleException("Uma matricula deve ter exatamente 1 responsavel principal.");
        }
    }
}
