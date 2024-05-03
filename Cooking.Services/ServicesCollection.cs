using Microsoft.Extensions.DependencyInjection;
using Cooking.Contracts.Services;

namespace Cooking.Services;

public static class ServicesCollection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IDishesService, DishesService>();

        return services;
    }
}
