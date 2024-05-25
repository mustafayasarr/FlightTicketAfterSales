using FlightTicket.Domain.Interfaces;
using FlightTicket.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FlightTicket.Infrastructure.Persistence.Interceptor;


namespace FlightTicket.Infrastructure
{
    public static class DependencyInjection
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), x =>
                {
                    x.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                });
            });
        }
    }
}
