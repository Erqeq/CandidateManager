using CandidateManager.Application.Interfaces;
using CandidateManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CandidateManager.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<ICandidateService, CandidateService>();

        return services;
    }
}
