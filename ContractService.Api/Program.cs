using ContractService.Application.Contracts;
using ContractService.Application.Contracts.DTOs;
using ContractService.Application.Contracts.UseCases;
using ContractService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infra + casos de uso
builder.Services.AddContractInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok(new { ok = true }));

app.MapGet("/contracts", async (IListContracts uc, CancellationToken ct) =>
{
    var list = await uc.ExecuteAsync(ct);
    return Results.Ok(list);
});

app.MapPost("/contracts", async (CreateContractRequest req, ICreateContract uc, CancellationToken ct) =>
{
    try
    {
        var created = await uc.ExecuteAsync(req, ct);
        return Results.Created($"/contracts/{created.Id}", created);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.Run();

public partial class Program { }
