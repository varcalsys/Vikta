using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vikta.Application.Academico.Commands;
using Vikta.Application.Academico.Queries;
using Vikta.Application.Academico.Requests;
using Vikta.Domain.Common;

namespace Vikta.Api.Academico.Endpoints;

public static class TurmaEndpoints
{
    public static IEndpointRouteBuilder MapTurmaEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/academico").WithTags("Academico");

        group.MapPost("/turmas", async (
            [FromBody] TurmaRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var response = await sender.Send(new CriarTurmaCommand(request), cancellationToken);

                return Results.Created($"/academico/turmas/{response.Id}", response);
            }
            catch (ValidationException validationException)
            {
                return CriarValidationProblem(validationException);
            }
            catch (BusinessRuleException businessRuleException)
            {
                return Results.BadRequest(new { message = businessRuleException.Message });
            }
        })
        .WithName("CriarTurma")
        .WithSummary("Cria uma nova turma");

        group.MapGet("/turmas", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new ListarTurmasQuery(), cancellationToken);
            return Results.Ok(response);
        })
        .WithName("ListarTurmas")
        .WithSummary("Lista todas as turmas");

        group.MapGet("/turmas/{id:int}", async (
            int id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var response = await sender.Send(new ObterTurmaPorIdQuery(id), cancellationToken);

                return response is null ? Results.NotFound() : Results.Ok(response);
            }
            catch (ValidationException validationException)
            {
                return CriarValidationProblem(validationException);
            }
        })
        .WithName("ObterTurmaPorId")
        .WithSummary("Obtem uma turma por identificador");

        group.MapPut("/turmas/{id:int}", async (
            int id,
            [FromBody] TurmaRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var response = await sender.Send(new AtualizarTurmaCommand(id, request), cancellationToken);

                return Results.Ok(response);
            }
            catch (ValidationException validationException)
            {
                return CriarValidationProblem(validationException);
            }
            catch (BusinessRuleException businessRuleException)
            {
                return Results.BadRequest(new { message = businessRuleException.Message });
            }
        })
        .WithName("AtualizarTurma")
        .WithSummary("Atualiza os dados de uma turma");

        group.MapDelete("/turmas/{id:int}", async (
            int id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            try
            {
                await sender.Send(new ExcluirTurmaCommand(id), cancellationToken);

                return Results.NoContent();
            }
            catch (ValidationException validationException)
            {
                return CriarValidationProblem(validationException);
            }
            catch (BusinessRuleException businessRuleException)
            {
                return Results.BadRequest(new { message = businessRuleException.Message });
            }
        })
        .WithName("ExcluirTurma")
        .WithSummary("Remove uma turma");

        return app;
    }

    private static IResult CriarValidationProblem(ValidationException validationException)
    {
        var errors = validationException.Errors
            .GroupBy(error => error.PropertyName)
            .ToDictionary(
                groupBy => groupBy.Key,
                groupBy => groupBy.Select(error => error.ErrorMessage).ToArray());

        return Results.ValidationProblem(errors);
    }
}
