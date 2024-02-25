using System.Windows;
using Forza_Mods_AIO.Services;
using Forza_Mods_AIO.ViewModels.Windows;
using Forza_Mods_AIO.Views.Windows;
using MahApps.Metro.Controls;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Forza_Mods_AIO;

public partial class App
{
    private static readonly IHost Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(c =>
        {
            c.SetBasePath(AppContext.BaseDirectory);
        }).
        ConfigureServices((_, services) =>
        {
            services.AddHostedService<ApplicationHostService>();
            
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MetroWindow, MainWindow>();

            services.AddSingleton<DebugWindowViewModel>();
            services.AddSingleton<DebugWindow>();
        }).Build();
    
    public static T GetRequiredService<T>() where T : class
    {
        return Host.Services.GetRequiredService<T>();
    }
    
    private async void App_OnStartup(object sender, StartupEventArgs e)
    {
        await Host.StartAsync();
    }

    private async void App_OnExit(object sender, ExitEventArgs e)
    {
        await Host.StopAsync();
        Host.Dispose();
    }
}