using FluentValidation;
using Vikta.Application.Academico.Queries;

namespace Vikta.Application.Academico.Validators;

public sealed class ObterTurmaPorIdQueryValidator : AbstractValidator<ObterTurmaPorIdQuery>
{
    public ObterTurmaPorIdQueryValidator()
    {
        RuleFor(query => query.Id).GreaterThan(0);
    }
}
