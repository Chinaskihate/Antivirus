using Antivirus.Application.Interfaces;
using Antivirus.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Antivirus.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IScanService, ScanService>();

        return services;
    }
}