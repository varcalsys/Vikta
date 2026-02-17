using FluentValidation;
using Vikta.Application.Academico.Commands;

namespace Vikta.Application.Academico.Validators;

public sealed class CriarTurmaCommandValidator : AbstractValidator<CriarTurmaCommand>
{
    public CriarTurmaCommandValidator()
    {
        RuleFor(command => command.Turma).NotNull();
        RuleFor(command => command.Turma.Nome).NotEmpty().MaximumLength(50);
        RuleFor(command => command.Turma.Ano).GreaterThan(0);
    }
}
