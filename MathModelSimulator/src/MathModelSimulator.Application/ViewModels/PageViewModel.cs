using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MathModelSimulator.Application.ViewModels;

public abstract partial class PageViewModel : ViewModelBase, IPageViewModel
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private StreamGeometry _icon;
}