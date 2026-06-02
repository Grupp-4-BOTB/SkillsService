using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillsService.Domain.Repositories;
using SkillsService.Infrastructure.Persistance;
using SkillsService.Infrastructure.Persistance.Repositories;

namespace SkillsService.Infrastructure;

public static class DependenyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SkillsDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ISkillCatalogRepository, SkillCatalogRepository>();
        services.AddScoped<IUserSkillRepository, UserSkillRepository>();

        return services;
    }
}
