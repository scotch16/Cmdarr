using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.IO;

namespace cmdarr
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        
        static void Main(string[] args)
        {
            RegisterServices(args);
            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<ConsoleApplication>().Run();
            DisposeServices();
        }

        private static void RegisterServices(string[] args)
        {
            var services = new ServiceCollection();
            IConfiguration configuration = SetupConfiguration(args);

            services.AddSingleton(configuration);
            services.AddLogging(configure=> configure.AddConsole());
            services.AddLogging();
            services.AddSingleton<ConsoleApplication>();            
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static IConfiguration SetupConfiguration(string[] args)
        {
            var appSettingsFilePath = File.Exists("/config/appsettings.json") ? "/config/appsettings.json" : AppContext.BaseDirectory + "/config/appsettings.json";
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(appSettingsFilePath, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
