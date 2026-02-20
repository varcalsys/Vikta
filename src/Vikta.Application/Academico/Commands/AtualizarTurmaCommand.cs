using MediatR;
using Vikta.Application.Academico.Requests;
using Vikta.Application.Academico.Responses;

namespace Vikta.Application.Academico.Commands;

public record AtualizarTurmaCommand(int Id, TurmaRequest Turma) : IRequest<TurmaResponse>;
