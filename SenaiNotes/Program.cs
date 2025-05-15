using APISenaiNotes.Interfaces;
using APISenaiNotes.Repositories;
using Microsoft.OpenApi.Models;
using SenaiNotes.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"

    });
});


builder.Services.AddDbContext<SenaiNotesContext>();
builder.Services.AddTransient<INotaRepository, NotaRepository>();

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(
            name: "minhasOrigens",
            policy =>
            {
                // TODO: Alterar link para endereço do Front-End
                policy.WithOrigins("Endereço do Front-End: http://localhost:3000");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            }
        );
    });

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

app.MapControllers();

app.Run();