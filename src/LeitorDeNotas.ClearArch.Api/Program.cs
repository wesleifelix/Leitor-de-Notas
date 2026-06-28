using LeitorDeNotas.ClearArch.Application.UseCases.Notas;
using LeitorDeNotas.ClearArch.Infrastructure.Data;
using LeitorDeNotas.ClearArch.Infrastructure.Repositories;
using LeitorDeNotas.ClearArch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Host=localhost;Database=LeitorDeNotas;Username=postgres;Password=postgres";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddSingleton<INotaRepository, NotaRepository>();
builder.Services.AddScoped<GetNotasQuery>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Força o Swagger UI a procurar o arquivo correto de definição da API
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leitor de Notas API v1");
    });
}

app.MapGet("/notas", async (GetNotasQuery query) => await query.ExecuteAsync());

app.Run();
