#region Usings

using System;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MathModelSimulator.Application.Configuration;
using Microsoft.Extensions.Options;

#endregion

namespace MathModelSimulator.Application.ViewModels;

public class HomePageViewModel : ViewModelBase, IHomePageViewModel
{
    private readonly IOptionsMonitor<AppSettingsOptions> _appSettingsOptionsDelegate;
    public ICommand RedirectToSourceCodeCommand { get; }

    public HomePageViewModel(IOptionsMonitor<AppSettingsOptions> options)
    {
        _appSettingsOptionsDelegate = options;
        RedirectToSourceCodeCommand = new RelayCommand(OnRedirectToSourceCodeCommandExecuted);
    }

    private void OnRedirectToSourceCodeCommandExecuted()
    {
        try
        {
            Process.Start(new ProcessStartInfo(_appSettingsOptionsDelegate.CurrentValue.SourceCodeLink)
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