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
        BindingInProgress = true;
        
        PlotTitle = Title;
        
        _function = new LinearFunction();
        
        PlotModel = new PlotModel
                    {
                        Title = PlotTitle
                    };
        
        LinearAxis xAxis = new LinearAxis
                           {
                               Position = AxisPosition.Bottom,
                               Minimum = -100,
                               Maximum = 100
                           };
        PlotModel.Axes.Add(xAxis);
        
        LinearAxis yAxis = new LinearAxis
                           {
                               Position = AxisPosition.Left,
                               Minimum = -100,
                               Maximum = 100
                           };
        PlotModel.Axes.Add(yAxis);
        
        foreach (var plotModelAxis in PlotModel.Axes)
        {
            plotModelAxis.AxisChanged += OnAxisChanged;
        }
        
        LeftXRangeBorder = PlotModel.Axes.First(axis => axis.Position == AxisPosition.Bottom).Minimum;
        RightXRangeBorder = PlotModel.Axes.First(axis => axis.Position == AxisPosition.Bottom).Maximum;
        
        Slope = _function.Parameters.Slope;
        InitialValue = _function.Parameters.Slope;
        
        RedrawPlot();
        
        BindingInProgress = false;
    }

    private void RedrawPlot()
    {
        PlotModel.Series.Clear();
        PlotModel.Series.Add(new FunctionSeries(_function.CalculateSingleValue, 
                                                LeftXRangeBorder, 
                                                RightXRangeBorder, 
                                                0.2));
        PlotModel.InvalidatePlot(true);
    }
    
    [ObservableProperty]
    private double _leftXRangeBorder;
    
    [ObservableProperty]
    private double _rightXRangeBorder;

    public bool BindingInProgress { get; set; } = false;
    
    private void OnAxisChanged(object? sender, AxisChangedEventArgs e)
    {
        if (BindingInProgress)
        {
            return;
        }

        BindingInProgress = true;
        
        LeftXRangeBorder = PlotModel.Axes.First(axis => axis.Position == AxisPosition.Bottom).ActualMinimum;
        RightXRangeBorder = PlotModel.Axes.First(axis => axis.Position == AxisPosition.Bottom).ActualMaximum;

        RedrawPlot();
        
        BindingInProgress = false;
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
        if (BindingInProgress)
        {
            return;
        }
        
        _function.Parameters.Slope = value;
        
        RedrawPlot();
    }

    [ObservableProperty]
    private double _initialValue;

    partial void OnInitialValueChanged(double value)
    {
        if (BindingInProgress)
        {
            return;
        }
        
        _function.Parameters.InitialValue = value;
        
        RedrawPlot();
    }
}