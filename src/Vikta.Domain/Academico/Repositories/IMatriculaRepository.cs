using Vikta.Domain.Academico.Entities;

namespace Vikta.Domain.Academico.Repositories
{
    public interface IMatriculaRepository
    {
        Task<Matricula> MatricularAsync(Matricula matricula, CancellationToken cancellationToken);
        Task<bool> ExistePorNumeroAsync(int numero, CancellationToken cancellationToken);
        Task<bool> AlunoPossuiMatriculaAtivaAsync(string cpfAluno, CancellationToken cancellationToken);
    }
}
