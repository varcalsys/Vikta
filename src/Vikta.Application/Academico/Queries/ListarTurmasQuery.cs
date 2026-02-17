using MediatR;
using Vikta.Application.Academico.Responses;

namespace Vikta.Application.Academico.Queries;

public record ListarTurmasQuery() : IRequest<IReadOnlyCollection<TurmaResponse>>;
