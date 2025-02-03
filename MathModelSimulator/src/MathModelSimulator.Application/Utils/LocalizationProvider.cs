#region Usings

using System.ComponentModel;
using System.Globalization;
using System.Resources;
using MathModelSimulator.Application.Views;

#endregion

namespace MathModelSimulator.Application.Utils;

public class LocalizationProvider : INotifyPropertyChanged
{
    private static readonly LocalizationProvider instance = new ();

    public static LocalizationProvider Instance => instance;

    // TODO: Remove dependency, make assignable from View.
    private readonly ResourceManager _resManager = MainWindowResources.ResourceManager;
    private CultureInfo _currentCulture = CultureInfo.CurrentCulture;

    public string? this[string key] => _resManager.GetString(key, _currentCulture);

    public CultureInfo CurrentCulture
    {
        get => _currentCulture;
        set
        {
            if (Equals(_currentCulture, value))
            {
                return;
            }
            
            _currentCulture = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}