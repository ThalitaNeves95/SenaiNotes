using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SenaiNotes.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();




app.UseSwagger();
app.UseSwaggerUI();

app.Run();