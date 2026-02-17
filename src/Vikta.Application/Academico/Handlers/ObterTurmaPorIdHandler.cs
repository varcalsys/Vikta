using MediatR;
using Vikta.Application.Academico.Queries;
using Vikta.Application.Academico.Responses;
using Vikta.Domain.Academico.Repositories;

namespace Vikta.Application.Academico.Handlers;

public sealed class ObterTurmaPorIdHandler(ITurmaRepository turmaRepository) : IRequestHandler<ObterTurmaPorIdQuery, TurmaResponse?>
{
    public async Task<TurmaResponse?> Handle(ObterTurmaPorIdQuery query, CancellationToken cancellationToken)
    {
        var turma = await turmaRepository.ObterPorIdAsync(query.Id, cancellationToken);

        if (turma is null)
        {
            return null;
        }

        return new TurmaResponse(turma.Id, turma.Nome, turma.Ano);
    }
}
