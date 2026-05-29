

using BackEnd.Bussines.ItemsTrabajo;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.Bussines;

public static class ServiceExtension
{
    public static IServiceCollection AddServicesExtension(this IServiceCollection services)
    {
       
       
        services.AddScoped<IItemTrabajoService, ItemTrabajoService> ();
    
        return services;
    }
}
