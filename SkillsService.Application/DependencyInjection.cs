using Microsoft.Extensions.DependencyInjection;
using SkillsService.Application.Services;

namespace SkillsService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<SkillsAppService>();
        return services;
    }
}
