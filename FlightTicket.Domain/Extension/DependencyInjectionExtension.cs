using FlightTicket.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicket.Domain.Extension
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection UseConfigureService(this IServiceCollection service, IConfiguration configuration)
        {
            (from t in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly f) => f.GetTypes())
             where t.IsClass && t.GetInterfaces().Contains<Type>(typeof(IConfigureService))
             select t).Select(Activator.CreateInstance).Cast<IConfigureService>().ToList()
                .ForEach(delegate (IConfigureService f)
                {
                    f.Configure(service, configuration);
                });
            return service;
        }
    }
}
