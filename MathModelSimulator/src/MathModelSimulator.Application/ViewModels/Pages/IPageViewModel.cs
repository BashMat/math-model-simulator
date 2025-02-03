using Avalonia.Media;

namespace MathModelSimulator.Application.ViewModels.Pages;

public interface IPageViewModel
{
    string PageName { get; }
    string GetPageTitle();
    string Title { get; }
    StreamGeometry Icon { get; }
}