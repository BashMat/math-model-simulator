#region Usings

using System.ComponentModel;
using System.Globalization;
using System.Resources;
using MathModelSimulator.Application.Views;

#endregion

namespace MathModelSimulator.Application.Utils;

/// <summary>
///     Represents a singleton object responsible for application localization.
/// </summary>
public class LocalizationManager : INotifyPropertyChanged
{
    private LocalizationManager() {}
    
    private static LocalizationManager? _instance;
    
    private static readonly object _lock = new();

    /// <summary>
    ///     Gets <see cref="LocalizationManager"/> instance.
    /// </summary>
    public static LocalizationManager Instance
    {
        get
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    if (_instance is null)
                    {
                        _instance = new LocalizationManager();
                        return _instance;
                    }
                }
            }

            return _instance;
        }
    }

    // TODO: Remove dependency, make assignable from View.
    private readonly ResourceManager _resManager = MainWindowResources.ResourceManager;
    private CultureInfo _currentCulture = CultureInfo.CurrentCulture;

    /// <summary>
    ///     Gets localization resource by its key.
    /// </summary>
    /// <param name="key">Resource key</param>
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