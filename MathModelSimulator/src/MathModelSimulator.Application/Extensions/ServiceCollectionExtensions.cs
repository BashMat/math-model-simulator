#region Usings

using MathModelSimulator.Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MathModelSimulator.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddTransient<IPageViewModel, HomePageViewModel>()
                                .AddTransient<MainWindowViewModel>();
    }
}