using Microsoft.EntityFrameworkCore;
using Vikta.Domain.Academico.Entities;
using Vikta.Domain.Academico.Enums;
using Vikta.Domain.Academico.Repositories;

namespace Vikta.Data.Academico.Repositories
{
    public sealed class MatriculaRepository(AcademicoDbContext context) : IMatriculaRepository
    {
        public async Task<Matricula> MatricularAsync(Matricula matricula, CancellationToken cancellationToken)
        {
            await context.Matriculas.AddAsync(matricula, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return matricula;
        }

        public Task<bool> ExistePorNumeroAsync(int numero, CancellationToken cancellationToken)
        {
            return context.Matriculas.AnyAsync(m => m.Numero == numero, cancellationToken);
        }

        public async Task<bool> AlunoPossuiMatriculaAtivaAsync(string cpfAluno, CancellationToken cancellationToken)
        {
            var matriculasAtivas = await context.Matriculas
                .AsNoTracking()
                .Include(m => m.Aluno)
                .Where(m => m.Status == StatusMatricula.Ativa)
                .ToListAsync(cancellationToken);

            return matriculasAtivas.Any(m => m.Aluno.Cpf.Numero == cpfAluno);
        }
    }
}
