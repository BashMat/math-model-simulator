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

    public static LocalizationProvider Instance
    {
        get { return instance; }
    }

    // TODO: Remove dependency, make assignable from View.
    private readonly ResourceManager resManager = MainWindowResources.ResourceManager;
    private CultureInfo currentCulture = null;

    public string? this[string key] => this.resManager.GetString(key, this.currentCulture);

    public CultureInfo CurrentCulture
    {
        get => currentCulture;
        set
        {
            if (Equals(currentCulture, value))
            {
                return;
            }
            
            currentCulture = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}