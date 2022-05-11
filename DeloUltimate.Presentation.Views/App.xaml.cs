using DeloUltimate.Eos.AppServices;
using DeloUltimate.Infrastructure.Data;
using DeloUltimate.Presentation.ViewModels.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace DeloUltimate.Presentation.Views
{
    public partial class App : Application
    {
        private static readonly IHost _host;

        public static IHost Host => _host
            ?? CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Host.Services;

        public static IHostBuilder CreateHostBuilder(string[] args) => Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(App.ConfigureServices);


        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddDatabase()
            .AddServices()
            .AddViewModels()
        ;

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);
            await Host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (var host = Host) 
            { 
                base.OnExit(e);
                await Host.StopAsync();
            }
        }
    }
}
