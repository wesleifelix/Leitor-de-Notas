using LeitorDeNotas.ClearArch.Application.UseCases.Notas;
using LeitorDeNotas.ClearArch.Infrastructure.Data;
using LeitorDeNotas.ClearArch.Infrastructure.Repositories;
using LeitorDeNotas.ClearArch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Host=localhost;Database=LeitorDeNotas;Username=postgres;Password=postgres";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
builder.Services.AddScoped<INotaFiscalXmlParser, NotaFiscalXmlParser>();

builder.Services.AddSingleton<INotaRepository, NotaRepository>();
builder.Services.AddScoped<GetNotasQuery>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/notas", async (GetNotasQuery query) => await query.ExecuteAsync());

app.Run();
