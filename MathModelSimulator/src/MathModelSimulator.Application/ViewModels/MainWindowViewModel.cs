#region Usings

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MathModelSimulator.Application.Configuration;
using MathModelSimulator.Application.Utils;
using MathModelSimulator.Application.ViewModels.Pages;
using MathModelSimulator.Application.Views;
using Microsoft.Extensions.Options;

#endregion

namespace MathModelSimulator.Application.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, ICloseable
{
    public MainWindowViewModel(IOptionsMonitor<AppSettingsOptions> options)
    {
        UpdateLanguageCommand = new RelayCommand(OnUpdateLanguageCommandExecuted);
        CloseCommand = new RelayCommand(OnCloseCommandExecuted);
        OpenClosePaneCommand = new RelayCommand(OnOpenClosePaneCommandExecuted);
        ShowAboutCommand = new RelayCommand(OnShowAboutCommandExecuted);

        Pages = new ObservableCollection<IPageViewModel>();
        Pages.Add(new HomePageViewModel(options));
        Pages.Add(new ModelPageViewModel());
        
        SelectedPage = Pages.First(vm => vm.Title == MainWindowResources.HomePageTitle);

        _aboutDialog = new AboutDialogView(options);
    }

    public ObservableCollection<IPageViewModel> Pages { get; }
    
    [ObservableProperty]
    private IPageViewModel _selectedPage;

    partial void OnSelectedPageChanged(IPageViewModel value)
    {
        CurrentPage = value;
    }
    
    public ICommand CloseCommand { get; }

    public event EventHandler? Close;
    
    private void OnCloseCommandExecuted()
    {
        Close?.Invoke(null, EventArgs.Empty);
    }
    
    [ObservableProperty]
    private bool _isPaneOpen;
    
    [ObservableProperty]
    private IPageViewModel _currentPage;

    public ICommand OpenClosePaneCommand { get; }
    
    private void OnOpenClosePaneCommandExecuted()
    {
        IsPaneOpen = !IsPaneOpen;
    }
    
    public ICommand UpdateLanguageCommand { get; }

    private void OnUpdateLanguageCommandExecuted()
    {
        // LocalizationProvider.Instance.CurrentCulture = string.IsNullOrEmpty(cultureCode) 
        //                                                    ? CultureInfo.CurrentCulture 
        //                                                    : new CultureInfo(cultureCode);

        var cultureInfoToBeUsed = new CultureInfo("ru-RU");
        if (Equals(LocalizationManager.Instance.CurrentCulture, cultureInfoToBeUsed))
        {
            cultureInfoToBeUsed = new CultureInfo("en");
        }

        LocalizationManager.Instance.CurrentCulture = cultureInfoToBeUsed;
    }

    private AboutDialogView _aboutDialog;
    public ICommand ShowAboutCommand { get; init; }
    
    private void OnShowAboutCommandExecuted()
    {
        _aboutDialog.Show();
    }
}