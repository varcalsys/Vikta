using MediatR;
using Vikta.Application.Academico.Commands;
using Vikta.Application.Academico.Responses;
using Vikta.Domain.Academico.Repositories;
using Vikta.Domain.Common;

namespace Vikta.Application.Academico.Handlers;

public sealed class AtualizarTurmaHandler(ITurmaRepository turmaRepository) : IRequestHandler<AtualizarTurmaCommand, TurmaResponse>
{
    public async Task<TurmaResponse> Handle(AtualizarTurmaCommand command, CancellationToken cancellationToken)
    {
        var turma = await turmaRepository.ObterPorIdAsync(command.Id, cancellationToken);

        if (turma is null)
        {
            throw new BusinessRuleException("Turma nao encontrada.");
        }

        if (await turmaRepository.ExistePorNomeEAnoAsync(command.Turma.Nome, command.Turma.Ano, turma.Id, cancellationToken))
        {
            throw new BusinessRuleException("Ja existe turma cadastrada com o mesmo nome e ano.");
        }

        turma.Atualizar(command.Turma.Nome, command.Turma.Ano);

        var turmaAtualizada = await turmaRepository.AtualizarAsync(turma, cancellationToken);

        return new TurmaResponse(turmaAtualizada.Id, turmaAtualizada.Nome, turmaAtualizada.Ano);
    }
}
