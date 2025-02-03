#region Usings

using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MathModelSimulator.Application.Utils;

#endregion

namespace MathModelSimulator.Application.ViewModels.Pages;

public abstract partial class PageViewModel : ViewModelBase, IPageViewModel
{
    [ObservableProperty]
    private string _title = null!;

    [ObservableProperty]
    private StreamGeometry _icon = null!;

    public virtual string PageName => string.Empty;
    public string GetPageTitle()
    {
        return LocalizationProvider.Instance[$"{PageName}PageTitle"]!;
    }
}