using MediatR;
using Vikta.Application.Academico.Responses;

namespace Vikta.Application.Academico.Queries;

public record ObterTurmaPorIdQuery(int Id) : IRequest<TurmaResponse?>;
