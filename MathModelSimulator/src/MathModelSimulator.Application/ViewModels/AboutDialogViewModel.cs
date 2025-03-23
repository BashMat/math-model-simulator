using System;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MathModelSimulator.Application.Configuration;
using Microsoft.Extensions.Options;

namespace MathModelSimulator.Application.ViewModels;

public class AboutDialogViewModel : ViewModelBase
{
    private readonly IOptionsMonitor<AppSettingsOptions> _appSettingsOptions;
    
    public AboutDialogViewModel(IOptionsMonitor<AppSettingsOptions> options)
    {
        _appSettingsOptions = options;
        RedirectToSourceCodeCommand = new RelayCommand(OnRedirectToSourceCodeCommandExecuted);
    }
    
    public ICommand RedirectToSourceCodeCommand { get; }
    
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