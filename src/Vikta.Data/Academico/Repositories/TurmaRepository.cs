using Microsoft.EntityFrameworkCore;
using Vikta.Domain.Academico.Entities;
using Vikta.Domain.Academico.Repositories;

namespace Vikta.Data.Academico.Repositories;

public sealed class TurmaRepository(AcademicoDbContext context) : ITurmaRepository
{
    public async Task<Turma> CriarAsync(Turma turma, CancellationToken cancellationToken)
    {
        await context.Turmas.AddAsync(turma, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return turma;
    }

    public async Task<IReadOnlyCollection<Turma>> ListarAsync(CancellationToken cancellationToken)
    {
        return await context.Turmas
            .AsNoTracking()
            .OrderBy(turma => turma.Ano)
            .ThenBy(turma => turma.Nome)
            .ToListAsync(cancellationToken);
    }

    public Task<Turma?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
    {
        return context.Turmas
            .FirstOrDefaultAsync(turma => turma.Id == id, cancellationToken);
    }

    public Task<bool> ExistePorNomeEAnoAsync(string nome, int ano, int? ignorarId, CancellationToken cancellationToken)
    {
        return context.Turmas.AnyAsync(
            turma => turma.Nome == nome && turma.Ano == ano && (!ignorarId.HasValue || turma.Id != ignorarId.Value),
            cancellationToken);
    }

    public async Task<Turma> AtualizarAsync(Turma turma, CancellationToken cancellationToken)
    {
        context.Turmas.Update(turma);
        await context.SaveChangesAsync(cancellationToken);

        return turma;
    }

    public async Task RemoverAsync(Turma turma, CancellationToken cancellationToken)
    {
        context.Turmas.Remove(turma);
        await context.SaveChangesAsync(cancellationToken);
    }
}
