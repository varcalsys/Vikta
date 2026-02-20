using FluentValidation;
using Vikta.Application.Academico.Commands;

namespace Vikta.Application.Academico.Validators;

public class MatricularAlunoCommandValidator : AbstractValidator<MatricularAlunoCommand>
{
    public MatricularAlunoCommandValidator()
    {
        RuleFor(command => command.Matricula).NotNull();
        RuleFor(command => command.Matricula.Numero).GreaterThan(0);

        RuleFor(command => command.Matricula.Aluno).NotNull();
        RuleFor(command => command.Matricula.Aluno.Nome).NotEmpty().MaximumLength(100);
        RuleFor(command => command.Matricula.Aluno.Cpf).NotEmpty().Matches("^[0-9]{11}$");

        RuleFor(command => command.Matricula.Responsaveis).NotNull();
        RuleFor(command => command.Matricula.Responsaveis).NotEmpty();
        RuleFor(command => command.Matricula.Responsaveis.Count).LessThanOrEqualTo(3);
        RuleFor(command => command.Matricula.Responsaveis.Count(responsavel => responsavel.Principal))
            .Equal(1);
        RuleForEach(command => command.Matricula.Responsaveis).ChildRules(responsavel =>
        {
            responsavel.RuleFor(x => x.Nome).NotEmpty().MaximumLength(100);
            responsavel.RuleFor(x => x.Cpf).NotEmpty().Matches("^[0-9]{11}$");
        });

        RuleFor(command => command.Matricula.Endereco).NotNull();
        RuleFor(command => command.Matricula.Endereco.Logradouro).NotEmpty().MaximumLength(150);
        RuleFor(command => command.Matricula.Endereco.Numero).NotEmpty().MaximumLength(20);
        RuleFor(command => command.Matricula.Endereco.Bairro).NotEmpty().MaximumLength(100);
        RuleFor(command => command.Matricula.Endereco.Cep).NotEmpty().Matches("^[0-9]{8}$");
    }
}
