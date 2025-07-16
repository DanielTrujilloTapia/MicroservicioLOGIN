using Microsoft.AspNetCore.Builder;
using Microservicio.Login.Api.Extensions; // Cambia al namespace correcto de tu extensi�n

var builder = WebApplication.CreateBuilder(args);

// CORS: Permitir todo (ajusta si quieres seguridad m�s estricta)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Agrega controladores y validaci�n
builder.Services.AddControllers();

// Swagger / OpenAPI para documentaci�n
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Aqu� registras todos tus servicios personalizados, incluidos Mongo, AutoMapper, MediatR y FluentValidation
builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
