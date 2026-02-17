using Vikta.Application;
using Vikta.Api.Academico.Endpoints;
using Vikta.Data.Academico;
using Vikta.Domain.Academico.Repositories;
using Vikta.Data.Academico.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();

var connectionString = builder.Configuration.GetConnectionString("AcademicoDb")
    ?? "Server=(localdb)\\mssqllocaldb;Database=ViktaAcademicoDb;Trusted_Connection=True;TrustServerCertificate=True;";

builder.Services.AddDbContext<AcademicoDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IMatriculaRepository, MatriculaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapAcademicoEndpoints();

app.Run();
