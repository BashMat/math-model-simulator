#region Usings

using System.Windows.Input;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Input;

#endregion

namespace MathModelSimulator.Application.ViewModels;

public class AppViewModel : ViewModelBase
{
    public ICommand CloseCommand { get; init; }

    public AppViewModel()
    {
        CloseCommand = new RelayCommand(OnCloseCommandExecuted);
    }

    private void OnCloseCommandExecuted()
    {
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }
}