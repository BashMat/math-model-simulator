#region Usings

using System;
using System.Globalization;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MathModelSimulator.Application.Utils;

#endregion

namespace MathModelSimulator.Application.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, ICloseable
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    public event EventHandler? Close;
    
    public ICommand UpdateLanguageCommand { get; }
    public ICommand CloseCommand { get; }
    public ICommand OpenClosePaneCommand { get; }
    
    private bool _isPaneOpen;
    
    public bool IsPaneOpen
    {
        get => _isPaneOpen;
        set => SetProperty(ref _isPaneOpen, value);
    }

    private IPageViewModel _currentPageViewModel;

    public IPageViewModel CurrentPageViewModel
    {
        get => _currentPageViewModel;
        set => SetProperty(ref _currentPageViewModel, value);
    }

    public MainWindowViewModel(IPageViewModel defaultPageViewModel)
    {
        _currentPageViewModel = defaultPageViewModel;
        UpdateLanguageCommand = new RelayCommand(OnUpdateLanguageCommandExecuted);
        CloseCommand = new RelayCommand(OnCloseCommandExecuted);
        OpenClosePaneCommand = new RelayCommand(OnOpenClosePaneCommandExecuted);
    }

    private void OnOpenClosePaneCommandExecuted()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    private void OnUpdateLanguageCommandExecuted()
    {
        // LocalizationProvider.Instance.CurrentCulture = string.IsNullOrEmpty(cultureCode) 
        //                                                    ? CultureInfo.CurrentCulture 
        //                                                    : new CultureInfo(cultureCode);

        CultureInfo cultureInfoToBeUsed = new CultureInfo("ru-RU");
        if (Equals(LocalizationProvider.Instance.CurrentCulture, cultureInfoToBeUsed))
        {
            cultureInfoToBeUsed = new CultureInfo("en");
        }

        LocalizationProvider.Instance.CurrentCulture = cultureInfoToBeUsed;
    }

    private void OnCloseCommandExecuted()
    {
        Close?.Invoke(null, EventArgs.Empty);
    }
}