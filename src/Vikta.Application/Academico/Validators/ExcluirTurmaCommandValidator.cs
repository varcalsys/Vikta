using FluentValidation;
using Vikta.Application.Academico.Commands;

namespace Vikta.Application.Academico.Validators;

public sealed class ExcluirTurmaCommandValidator : AbstractValidator<ExcluirTurmaCommand>
{
    public ExcluirTurmaCommandValidator()
    {
        RuleFor(command => command.Id).GreaterThan(0);
    }
}
