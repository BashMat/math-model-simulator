#region Usings

using System;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MathModelSimulator.Application.Configuration;
using Microsoft.Extensions.Options;

#endregion

namespace MathModelSimulator.Application.ViewModels.Pages;

public class HomePageViewModel : PageViewModel, IHomePageViewModel
{
    private readonly IOptionsMonitor<AppSettingsOptions> _appSettingsOptions;
    public ICommand RedirectToSourceCodeCommand { get; }

    public HomePageViewModel(IOptionsMonitor<AppSettingsOptions> options) : base("Home", "HomeRegular")
    {
        _appSettingsOptions = options;
        RedirectToSourceCodeCommand = new RelayCommand(OnRedirectToSourceCodeCommandExecuted);
    }

    private void OnRedirectToSourceCodeCommandExecuted()
    {
        try
        {
            Process.Start(new ProcessStartInfo(_appSettingsOptions.CurrentValue.SourceCodeLink)
                          {
                              UseShellExecute = true
                          });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}