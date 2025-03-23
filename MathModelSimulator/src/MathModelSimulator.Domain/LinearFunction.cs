#region Usings

using System.Collections.Generic;

#endregion

namespace MathModelSimulator.Domain;

public class LinearFunction
{
    public LinearFunctionParameters Parameters { get; set; }
    
    public LinearFunction()
    {
        Parameters = new LinearFunctionParameters();
    }
    
    public LinearFunction(LinearFunctionParameters parameters)
    {
        Parameters = parameters;
    }

    public double CalculateSingleValue(double input)
    {
        return Parameters.Slope * input + Parameters.InitialValue;
    }
    
    public IEnumerable<Point2D> CalculatePoints(IReadOnlyCollection<double> input)
    {
        foreach (var value in input)
        {
            yield return new Point2D
                         {
                             X = value,
                             Y = CalculateSingleValue(value)
                         };
        }
    }
}