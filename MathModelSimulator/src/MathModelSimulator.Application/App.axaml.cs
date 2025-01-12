#region Usings

using System.Globalization;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using MathModelSimulator.Application.Utils;
using MathModelSimulator.Application.ViewModels;
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
        LocalizationProvider.Instance.CurrentCulture = CultureInfo.CurrentCulture;
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
                                 {
                                     DataContext = new MainWindowViewModel(),
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