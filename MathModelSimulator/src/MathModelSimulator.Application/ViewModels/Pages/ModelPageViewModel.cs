#region Usings

using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MathModelSimulator.Application.Utils;
using MathModelSimulator.Domain;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

#endregion

namespace MathModelSimulator.Application.ViewModels.Pages;

public partial class ModelPageViewModel : PageViewModel
{
    public ModelPageViewModel()
    {
        LocalizationProvider.Instance.PropertyChanged += UpdateTitle;
        Title = GetPageTitle();
        PlotTitle = Title;
        if (Avalonia.Application.Current!.TryFindResource("MathFormulaRegular", out var icon))
        {
            Icon = (StreamGeometry)icon!;
        }
        else
        {
            Icon = StreamGeometry.Parse("M7 12C7.55228 12 8 11.5523 8 11C8 10.4477 7.55228 10 7 10C6.44772 10 6 10.4477 6 11C6 11.5523 6.44772 12 7 12Z M11 11C11 11.5523 10.5523 12 10 12C9.44772 12 9 11.5523 9 11C9 10.4477 9.44772 10 10 10C10.5523 10 11 10.4477 11 11Z M13 12C13.5523 12 14 11.5523 14 11C14 10.4477 13.5523 10 13 10C12.4477 10 12 10.4477 12 11C12 11.5523 12.4477 12 13 12Z M3 5.5C3 4.11929 4.11929 3 5.5 3H14.5C15.8807 3 17 4.11929 17 5.5V14.5C17 15.8807 15.8807 17 14.5 17H5.5C4.11929 17 3 15.8807 3 14.5V5.5ZM5.5 4C4.67157 4 4 4.67157 4 5.5V14.5C4 15.3284 4.67157 16 5.5 16H14.5C15.3284 16 16 15.3284 16 14.5V7H9.5C8.67157 7 8 6.32843 8 5.5V4H5.5ZM16 5.5C16 4.67157 15.3284 4 14.5 4H9V5.5C9 5.77614 9.22386 6 9.5 6H16V5.5Z");
        }
        
        _function = new LinearFunction();
        InitializePlot();
    }

    private void InitializePlot()
    {
        PlotModel = new PlotModel
                    {
                        Title = PlotTitle
                    };
        var functionSeries = new FunctionSeries(_function.CalculateSingleValue, 
                                                -100.0, 
                                                100.0, 
                                                0.2);
        PlotModel.Series.Add(functionSeries);
        PlotModel.Axes.Add(new LinearAxis{ Position = AxisPosition.Bottom });
        PlotModel.Axes.Add(new LinearAxis{ Position = AxisPosition.Left });
    }

    private LinearFunction _function;

    [ObservableProperty]
    private PlotModel _plotModel;

    public override string PageName => "Model";

    [ObservableProperty]
    private string _plotTitle;

    [ObservableProperty]
    private double _slope;

    partial void OnSlopeChanged(double value)
    {
        _function.Parameters.Slope = value;
        PlotModel.Series[0] = new FunctionSeries(_function.CalculateSingleValue, 
                                                 -100.0, 
                                                 100.0, 
                                                 0.2);
        PlotModel.InvalidatePlot(true);
    }

    [ObservableProperty]
    private double _initialValue;

    partial void OnInitialValueChanged(double value)
    {
        _function.Parameters.InitialValue = value;
        PlotModel.Series[0] = new FunctionSeries(_function.CalculateSingleValue, 
                                                 -100.0, 
                                                 100.0, 
                                                 0.2);
        PlotModel.InvalidatePlot(true);
    }

    private void UpdateTitle(object? sender, PropertyChangedEventArgs e)
    {
        Title = GetPageTitle();
    }
}