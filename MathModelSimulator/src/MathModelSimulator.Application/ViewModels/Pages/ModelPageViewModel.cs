#region Usings

using System.ComponentModel;
using System.Linq;
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
    public ModelPageViewModel() : base("MathModel", "MathFormulaRegular")
    {
        InitializePlot();
    }

    private void InitializePlot()
    {
        PlotTitle = Title;
        
        _function = new LinearFunction();
        
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
}