using Microsoft.OpenApi.Models;
using SmartEnum;
using SelfEnumTipoPessoa.SmartEnum;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(o =>
{
    o.MapType<TipoPessoa>(() => new OpenApiSchema
    {
        Type = "string", // Mostrar como string no Swagger
        Description = TipoPessoa.Descriptions,
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapGet("/GetTipoPessoa", () =>
{
    var result = new
    {
        TiposPessoa = TipoPessoa.Descriptions
    };

    return Results.Ok(result);
})
.WithName("GetTipoPessoa");

app.MapPost("/CreatePessoa", (PessoaRequest request) =>
{
    var result = new
    {
        Message = "Pessoa criada com sucesso!",
        request.Nome,
        TipoPessoa = request.TipoPessoa.Value
    };

    return Results.Ok(result);
})
.WithName("CreatePessoa");

app.Run();
