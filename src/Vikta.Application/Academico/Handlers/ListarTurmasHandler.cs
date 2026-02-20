using MediatR;
using Vikta.Application.Academico.Queries;
using Vikta.Application.Academico.Responses;
using Vikta.Domain.Academico.Repositories;

namespace Vikta.Application.Academico.Handlers;

public sealed class ListarTurmasHandler(ITurmaRepository turmaRepository) : IRequestHandler<ListarTurmasQuery, IReadOnlyCollection<TurmaResponse>>
{
    public async Task<IReadOnlyCollection<TurmaResponse>> Handle(ListarTurmasQuery query, CancellationToken cancellationToken)
    {
        var turmas = await turmaRepository.ListarAsync(cancellationToken);

        return turmas
            .Select(turma => new TurmaResponse(turma.Id, turma.Nome, turma.Ano))
            .ToList();
    }
}
