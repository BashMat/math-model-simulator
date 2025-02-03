using Avalonia.Media;

namespace MathModelSimulator.Application.ViewModels;

public interface IPageViewModel
{
    string Title { get; }
    StreamGeometry Icon { get; }
}