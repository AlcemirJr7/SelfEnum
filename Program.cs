using Microsoft.OpenApi.Models;
using SmartEnum;
using SelfEnumPersonType.SmartEnum;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(o =>
{
    o.MapType<PersonType>(() => new OpenApiSchema
    {
        Type = "string", // Show as string Swagger
        Description = PersonType.Descriptions,
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

app.MapGet("/GetPersonType", () =>
{
    var result = new
    {
        PersonType = PersonType.Descriptions
    };

    return Results.Ok(result);
})
.WithName("GetPersonType");

app.MapPost("/CreatePerson", (PersonRequest request) =>
{
    var result = new
    {
        Message = "Pessoa criada com sucesso!",
        request.Name,
        PersonType = request.PersonType.Value
    };

    return Results.Ok(result);
})
.WithName("CreatePerson");

app.Run();
