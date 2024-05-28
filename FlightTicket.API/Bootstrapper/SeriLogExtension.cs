using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;

namespace FlightTicket.API.Bootstrapper;

public static class SerilogExtension
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        var logPath = builder.Configuration.GetSection("Log").GetSection("Path").Value;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Warning()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
            .MinimumLevel.Override("System", LogEventLevel.Fatal)
            .Enrich.FromLogContext()
            .Enrich.WithCorrelationId()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ApplicationName", $"İşEkspres API")
            .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
            .WriteTo.Async(writeTo => writeTo.File(path: logPath, rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {CorrelationId} {Level:u3} ] {Username} {Message:lj} {Exception}{NewLine}"))
           .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Host.UseSerilog(Log.Logger, true);

        return builder;
    }
}
