using LeitorDeNotas.ClearArch.Application.Interfaces;
using LeitorDeNotas.ClearArch.Application.Services;
using LeitorDeNotas.ClearArch.Domain.Interfaces;
using LeitorDeNotas.ClearArch.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LeitorDeNotas.ClearArch.IoC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLeitorDeNotasServices(this IServiceCollection services)
    {
        services.AddSingleton<INotaRepository, NotaRepository>();
        services.AddScoped<GetNotasQuery>();
        services.AddScoped<INotaService, NotaService>();
        services.AddScoped<IBatchProcessingService, BatchProcessingService>();
        services.AddTransient<INotificationService, NotificationService>();

        return services;
    }
}
