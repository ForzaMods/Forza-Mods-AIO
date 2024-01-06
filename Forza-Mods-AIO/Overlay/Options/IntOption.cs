using System;
using System.Windows;

namespace Forza_Mods_AIO.Overlay.Options;

public sealed class IntOption : MenuOption
{
    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged();
            OnMinimumReached(value);
            OnMaximumReached(value);
        }
    }
    private int _value;

    public int Minimum { get; } = int.MinValue;
    public int Maximum { get; } = int.MaxValue;

    public event RoutedEventHandler? ValueChanged;
    public event RoutedEventHandler? MinimumReached;
    public event RoutedEventHandler? MaximumReached;

    private void OnValueChanged()
    {
        var handler = ValueChanged;
        if (handler == null)
        {
            return;
        }

        Application.Current.Dispatcher.BeginInvoke(() => handler(this, new RoutedEventArgs()));
    }

    private void OnMinimumReached(int value)
    {
        if (Minimum != value)
        {
            return;
        }

        var handler = MinimumReached;
        if (handler == null)
        {
            return;
        }
            
        Application.Current.Dispatcher.BeginInvoke(() => handler(this, new RoutedEventArgs()));
    }

    private void OnMaximumReached(int value)
    {
        if (Maximum != value)
        {
            return;
        }

        var handler = MaximumReached;
        if (handler == null)
        {
            return;
        }

        Application.Current.Dispatcher.BeginInvoke(() => handler(this, new RoutedEventArgs()));
    }
    
    public static IntOption CreateWithMinimum(string name, int value, int minimum, string? description = null, bool isEnabled = true)
    {
        return new IntOption(name, value, minimum, int.MaxValue, description, isEnabled);
    }    
    
    public static IntOption CreateWithMinimum(string name, int value, double minimum, string? description = null, bool isEnabled = true)
    {
        return new IntOption(name, value, minimum, int.MaxValue, description, isEnabled);
    }

    public static IntOption CreateWithMaximum(string name, int value, int maximum, string? description = null, bool isEnabled = true)
    {
        return new IntOption(name,value, int.MinValue, maximum, description, isEnabled);
    }
    
    public static IntOption CreateWithMaximum(string name, int value, double maximum, string? description = null, bool isEnabled = true)
    {
        return new IntOption(name,value, int.MinValue, maximum, description, isEnabled);
    }
    
    public IntOption(string name, int value, string? description = null, bool isEnabled = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException(@"Name cannot be null or empty.", nameof(name));
        }

        Name = name;
        Value = value;
        Description = description;
        IsEnabled = isEnabled;
    }
    
    public IntOption(string name, int value, int minimum, int maximum, string? description = null,
        bool isEnabled = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException(@"Name cannot be null or empty.", nameof(name));
        }

        Name = name;
        Value = value;
        Minimum = minimum;
        Maximum = maximum;
        Description = description;
        IsEnabled = isEnabled;
    }   
    
    public IntOption(string name, int value, double minimum, double maximum, string? description = null,
        bool isEnabled = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException(@"Name cannot be null or empty.", nameof(name));
        }

        Name = name;
        Value = value;
        Minimum = Convert.ToInt32(minimum);
        Maximum = Convert.ToInt32(maximum);
        Description = description;
        IsEnabled = isEnabled;
    }
}