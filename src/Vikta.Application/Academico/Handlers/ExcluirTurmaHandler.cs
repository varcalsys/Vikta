using MediatR;
using Vikta.Application.Academico.Commands;
using Vikta.Domain.Academico.Repositories;
using Vikta.Domain.Common;

namespace Vikta.Application.Academico.Handlers;

public sealed class ExcluirTurmaHandler(ITurmaRepository turmaRepository) : IRequestHandler<ExcluirTurmaCommand>
{
    public async Task Handle(ExcluirTurmaCommand command, CancellationToken cancellationToken)
    {
        var turma = await turmaRepository.ObterPorIdAsync(command.Id, cancellationToken);

        if (turma is null)
        {
            throw new BusinessRuleException("Turma nao encontrada.");
        }

        await turmaRepository.RemoverAsync(turma, cancellationToken);
    }
}
