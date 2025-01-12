#region Usings

using System;

#endregion

namespace MathModelSimulator.Application.ViewModels;

public interface ICloseable
{
    public event EventHandler? Close;
}