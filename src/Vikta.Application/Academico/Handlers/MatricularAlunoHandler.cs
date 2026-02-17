using MediatR;
using Vikta.Application.Academico.Commands;
using Vikta.Application.Academico.Responses;
using Vikta.Domain.Academico.Entities;
using Vikta.Domain.Academico.Repositories;
using Vikta.Domain.Academico.Rules;
using Vikta.Domain.Common;
using Vikta.Domain.Academico.ValueObjects;

namespace Vikta.Application.Academico.Handlers;

public class MatricularAlunoHandler(IMatriculaRepository matriculaRepository) : IRequestHandler<MatricularAlunoCommand, MatricularAlunoResponse>
{
    public async Task<MatricularAlunoResponse> Handle(MatricularAlunoCommand command, CancellationToken cancellationToken)
    {
        MatriculaRules.ValidarResponsaveis(
            command.Matricula.Responsaveis.Count,
            command.Matricula.Responsaveis.Count(responsavel => responsavel.Principal));

        if (await matriculaRepository.ExistePorNumeroAsync(command.Matricula.Numero, cancellationToken))
        {
            throw new BusinessRuleException("Ja existe matricula cadastrada com o numero informado.");
        }

        if (await matriculaRepository.AlunoPossuiMatriculaAtivaAsync(command.Matricula.Aluno.Cpf, cancellationToken))
        {
            throw new BusinessRuleException("O aluno informado ja possui matricula ativa.");
        }

        var responsaveis = command.Matricula.Responsaveis
                .Select(responsavel => new Responsavel
                (
                    responsavel.Nome,
                    new Cpf(responsavel.Cpf),
                    responsavel.Principal                
                )).ToList();


        var matricula = new Matricula(
            command.Matricula.Numero,
            new Aluno
            (
                command.Matricula.Aluno.Nome,
                new Cpf (command.Matricula.Aluno.Cpf ),
                responsaveis
            ),
            responsaveis,
            new Endereco
            {
                Logradouro = command.Matricula.Endereco.Logradouro,
                Numero = command.Matricula.Endereco.Numero,
                Complemento = command.Matricula.Endereco.Complemento,
                Bairro = command.Matricula.Endereco.Bairro,
                Cep = new Cep { Numero = command.Matricula.Endereco.Cep }
            });

        await matriculaRepository.MatricularAsync(matricula, cancellationToken);

        var response = new MatricularAlunoResponse(matricula.Numero);

        return response;
    }
}
