using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Agregar OpenAPI / Swagger al contenedor
builder.Services.AddEndpointsApiExplorer(); // Esto agrega la capacidad de descubrir endpoints
builder.Services.AddSwaggerGen(); // Esto agrega la generación de Swagger

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ManagementDbContext>(options =>
{
    options.UseInMemoryDatabase("WpmManagement");
});


var app = builder.Build();


//asegurandose de que la base de datos fue creada.
app.EnsureDbIsCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Activa Swagger
    app.UseSwaggerUI(); // Activa la interfaz de usuario de Swagger
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
