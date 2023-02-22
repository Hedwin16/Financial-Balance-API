using ApiRepository.Interfaces;
using ApiRepository.Services;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Financial_Balance_API;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IRepository<Currency>, Repository<Currency>>();
        services.AddScoped<IRepository<Privilege>, Repository<Privilege>>();
        services.AddScoped<IRepository<User>, Repository<User>>();

        services.AddDbContext<ApiContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(ApiSettings.CONNECTION_STRING));
        });

        services.Configure<ApiSettings>(configuration.GetSection(ApiSettings.API_SETTINGS));

        return services;
    }
}
