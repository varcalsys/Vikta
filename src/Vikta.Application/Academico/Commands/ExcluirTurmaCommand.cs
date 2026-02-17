using MediatR;

namespace Vikta.Application.Academico.Commands;

public record ExcluirTurmaCommand(int Id) : IRequest;
