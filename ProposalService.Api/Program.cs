using FluentValidation;
using FluentValidation.AspNetCore;
using ProposalService.Application.Proposals;
using ProposalService.Application.Proposals.DTOs;
using ProposalService.Application.Proposals.UseCases;
using ProposalService.Application.Proposals.Validators;
using ProposalService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Hex infra + casos de uso
builder.Services.AddProposalInfrastructure(builder.Configuration);

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProposalValidator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok(new { ok = true }));

// Listar propostas
app.MapGet("/proposals", async (IListProposals uc, CancellationToken ct) =>
{
    var list = await uc.ExecuteAsync(ct);
    return Results.Ok(list);
});

// Criar proposta
app.MapPost("/proposals", async (CreateProposalRequest req, IValidator<CreateProposalRequest> validator, ICreateProposal uc, CancellationToken ct) =>
{
    var result = await validator.ValidateAsync(req, ct);
    if (!result.IsValid) return Results.ValidationProblem(result.ToDictionary());

    var created = await uc.ExecuteAsync(req, ct);
    return Results.Created($"/proposals/{created.Id}", created);
});

// Alterar status
app.MapPatch("/proposals/{id:guid}/status", async (Guid id, ChangeStatusRequest req, IChangeProposalStatus uc, CancellationToken ct) =>
{
    try
    {
        var updated = await uc.ExecuteAsync(id, req, ct);
        return updated is null ? Results.NotFound() : Results.Ok(updated);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.Run();

public partial class Program { }
