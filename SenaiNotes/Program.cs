using Microsoft.OpenApi.Models;
using SenaiNotes.Context;

using APISenaiNotes.Interfaces;
using APISenaiNotes.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ProjetoSenaiNotes",
            ValidAudience = "ProjetoAPISenaiNotes",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SenhaMaximaDoProjeto")),
        };
    });

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();


''
app.Run();