using Avalonia.Media;

namespace MathModelSimulator.Application.ViewModels.Pages;

public interface IPageViewModel
{
    string Title { get; }
    StreamGeometry Icon { get; }
}