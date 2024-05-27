using FlightTicket.Domain.Messages.Flight.Request;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FlightTicket.Domain;

public static class DependencyInjection
{
    public static void ConfigureDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(typeof(FindFlightRequestValidator).Assembly);        services.AddFluentValidationAutoValidation(); 
        services.AddFluentValidationClientsideAdapters();
    }
}
