using MediatR;
using Vikta.Application.Academico.Commands;
using Vikta.Application.Academico.Responses;
using Vikta.Domain.Academico.Entities;
using Vikta.Domain.Academico.Repositories;
using Vikta.Domain.Common;

namespace Vikta.Application.Academico.Handlers;

public sealed class CriarTurmaHandler(ITurmaRepository turmaRepository) : IRequestHandler<CriarTurmaCommand, TurmaResponse>
{
    public async Task<TurmaResponse> Handle(CriarTurmaCommand command, CancellationToken cancellationToken)
    {
        if (await turmaRepository.ExistePorNomeEAnoAsync(command.Turma.Nome, command.Turma.Ano, null, cancellationToken))
        {
            throw new BusinessRuleException("Ja existe turma cadastrada com o mesmo nome e ano.");
        }

        var turma = new Turma(command.Turma.Nome, command.Turma.Ano);
        var turmaCriada = await turmaRepository.CriarAsync(turma, cancellationToken);

        return new TurmaResponse(turmaCriada.Id, turmaCriada.Nome, turmaCriada.Ano);
    }
}
