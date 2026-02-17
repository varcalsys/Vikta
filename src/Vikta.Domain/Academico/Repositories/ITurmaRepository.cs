using Vikta.Domain.Academico.Entities;

namespace Vikta.Domain.Academico.Repositories;

public interface ITurmaRepository
{
    Task<Turma> CriarAsync(Turma turma, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Turma>> ListarAsync(CancellationToken cancellationToken);
    Task<Turma?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistePorNomeEAnoAsync(string nome, int ano, int? ignorarId, CancellationToken cancellationToken);
    Task<Turma> AtualizarAsync(Turma turma, CancellationToken cancellationToken);
    Task RemoverAsync(Turma turma, CancellationToken cancellationToken);
}
