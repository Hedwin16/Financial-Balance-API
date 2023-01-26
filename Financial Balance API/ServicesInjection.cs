using Financial_Balance_API.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

namespace Financial_Balance_API;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApiContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(ApiSettings.CONNECTION_STRING));
        });

        services.Configure<ApiSettings>(configuration.GetSection(ApiSettings.API_SETTINGS));

        return services;
    }
}
