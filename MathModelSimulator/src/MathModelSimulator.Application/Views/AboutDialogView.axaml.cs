#region Usings

using Avalonia.Controls;
using MathModelSimulator.Application.Configuration;
using MathModelSimulator.Application.ViewModels;
using Microsoft.Extensions.Options;

#endregion

namespace MathModelSimulator.Application.Views;

public partial class AboutDialogView : Window
{
    public AboutDialogView(IOptionsMonitor<AppSettingsOptions> options)
    {
        InitializeComponent();

        DataContext = new AboutDialogViewModel(options);

        Closing += (s, e) =>
                   {
                       ((Window)s!).Hide();
                       e.Cancel = true;
                   };
    }
}