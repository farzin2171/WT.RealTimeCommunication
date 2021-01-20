using System.Reflection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using WT.RealTime.MobileWebServices.Infrastructure.Logging;

namespace WT.RealTime.MobileWebServices.Infrastructure.Extentions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureLogger(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                var assembly = Assembly.GetEntryAssembly();
                var version = assembly?.GetName().Version.ToString();
                var informationVersion = assembly
                    ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

                var defaultLoggerEnricherOptions = new DefaultLoggerEnricherOptions
                {
                    Application = "MobileWebServices",
                    ApplicationVersion = version,
                    ApplicationInformationalVersion = informationVersion
                };

                loggerConfiguration
                    .Enrich.WithMachineName()
                    .Enrich.WithExceptionDetails()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning);

                loggerConfiguration
                    .Enrich.With(new DefaultLoggerEnricher(defaultLoggerEnricherOptions))
                    .Enrich.FromLogContext();

                if (hostingContext.HostingEnvironment.IsDevelopment())
                {
                    loggerConfiguration
                        .MinimumLevel.Debug()
                        .WriteTo.Console();
                }
                else
                {
                    loggerConfiguration
                        .MinimumLevel.Information();
                }

                //var loggingServiceApiKey = hostingContext.Configuration["LoggingService:ApiKey"];
                //var loggingServiceClientId = hostingContext.Configuration["LoggingService:ClientId"];
                //var loggingServiceEndpoint =
                //    $"{hostingContext.Configuration["LoggingService:EndPoint"]}/api/v1/log";

                //if (!string.IsNullOrEmpty(loggingServiceApiKey) &&
                //    !string.IsNullOrEmpty(loggingServiceClientId) &&
                //    !string.IsNullOrEmpty(loggingServiceEndpoint))
                //{
                //    var httpLoggingClient =
                //        new HttpLoggingClient(loggingServiceApiKey, loggingServiceClientId);
                //    loggerConfiguration.WriteTo.Http($"{loggingServiceEndpoint}",
                //        httpClient: httpLoggingClient, textFormatter: new CompactJsonFormatter());
                //}
            });

            return hostBuilder;
        }

    }
}
