using MediatR;
using Vikta.Application.Academico.Requests;
using Vikta.Application.Academico.Responses;

namespace Vikta.Application.Academico.Commands;

public record MatricularAlunoCommand(MatricularAlunoRequest Matricula) : IRequest<MatricularAlunoResponse>;
