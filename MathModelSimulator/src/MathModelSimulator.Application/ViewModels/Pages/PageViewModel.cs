using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MathModelSimulator.Application.ViewModels.Pages;

public abstract partial class PageViewModel : ViewModelBase, IPageViewModel
{
    [ObservableProperty]
    private string _title = null!;

    [ObservableProperty]
    private StreamGeometry _icon = null!;
}