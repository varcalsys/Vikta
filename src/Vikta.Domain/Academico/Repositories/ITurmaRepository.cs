using Vikta.Domain.Academico.Entities;

namespace Vikta.Domain.Academico.Repositories
{
    public interface ITurmaRepository
    {
        public Task<Turma> CriarTurmaAsync(Turma turma);
    }
}
