#region Usings

using System;
using System.Globalization;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using MathModelSimulator.Application.Configuration;
using MathModelSimulator.Application.Extensions;
using MathModelSimulator.Application.Utils;
using MathModelSimulator.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MainWindow = MathModelSimulator.Application.Views.MainWindow;

#endregion

namespace MathModelSimulator.Application;

public partial class App : Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        LocalizationManager.Instance.CurrentCulture = CultureInfo.CurrentCulture;

        var services = new ServiceCollection();
        
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                          .AddJsonFile($"appsettings{Environment.GetEnvironmentVariable("ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                                                          .AddUserSecrets<Program>()
                                                          .AddEnvironmentVariables()
                                                          .Build();
        
        services.Configure<AppSettingsOptions>(config.GetSection("AppSettings"));
        
        services.RegisterServices();
        
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            
            desktop.MainWindow = new MainWindow
                                 {
                                     DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>(),
                                 };
            if (desktop.MainWindow.DataContext is ICloseable closeable)
            {
                closeable.Close += (sender, eventArgs) => desktop.MainWindow.Close();
            }
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}