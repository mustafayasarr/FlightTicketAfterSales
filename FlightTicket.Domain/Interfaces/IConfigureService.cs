using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FlightTicket.Domain.Interfaces;

public interface IConfigureService
{
    void Configure(IServiceCollection services, IConfiguration configuration);
}
