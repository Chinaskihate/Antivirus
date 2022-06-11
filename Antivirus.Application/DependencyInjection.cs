using Antivirus.Application.Interfaces.ScanManagers;
using Antivirus.Application.Interfaces.ScanServices;
using Antivirus.Application.Services.ScanManagers;
using Antivirus.Application.Services.ScanServices;
using Microsoft.Extensions.DependencyInjection;

namespace Antivirus.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IScanService, ScanService>();
        services.AddSingleton<IScanManager, ScanManager>();

        return services;
    }
}