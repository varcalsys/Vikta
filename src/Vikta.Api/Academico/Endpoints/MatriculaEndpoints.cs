using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vikta.Application.Academico.Commands;
using Vikta.Application.Academico.Requests;
using Vikta.Domain.Common;

namespace Vikta.Api.Academico.Endpoints;

public static class MatriculaEndpoints
{
    public static IEndpointRouteBuilder MapAcademicoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/academico").WithTags("Academico");

        group.MapPost("/matriculas", async (
            [FromBody] MatricularAlunoRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var response = await sender.Send(new MatricularAlunoCommand(request), cancellationToken);

                return Results.Created($"/academico/matriculas/{response.Numero}", response);
            }
            catch (ValidationException validationException)
            {
                var errors = validationException.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        groupBy => groupBy.Key,
                        groupBy => groupBy.Select(error => error.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }
            catch (BusinessRuleException businessRuleException)
            {
                return Results.BadRequest(new { message = businessRuleException.Message });
            }
        })
        .WithName("MatricularAluno")
        .WithSummary("Realiza uma nova matricula academica");

        return app;
    }
}

