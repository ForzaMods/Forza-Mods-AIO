using System;
using System.Windows;

namespace Forza_Mods_AIO.Overlay.Options;

public sealed class FloatOption : MenuOption
{
    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged();
            OnMinimumReached();
            OnMaximumReached();
        }
    }
    private float _value;

    public float Minimum { get; } = float.MinValue;
    public float Maximum { get; } = float.MaxValue;

    public event EventHandler? ValueChangedEventHandler;
    public event EventHandler? MinimumReachedEventHandler;
    public event EventHandler? MaximumReachedEventHandler;

    private void OnValueChanged()
    {
        var handler = ValueChangedEventHandler;
        if (handler == null)
        {
            return;
        }

        Application.Current.Dispatcher.BeginInvoke(() => handler(this, EventArgs.Empty));
    }

    private void OnMinimumReached()
    {
        var handler = MinimumReachedEventHandler;
        if (handler == null)
        {
            return;
        }

        Application.Current.Dispatcher.BeginInvoke(() => handler(this, EventArgs.Empty));
    }

    private void OnMaximumReached()
    {
        var handler = MaximumReachedEventHandler;
        if (handler == null)
        {
            return;
        }

        Application.Current.Dispatcher.BeginInvoke(() => handler(this, EventArgs.Empty));
    }
    
    public static FloatOption CreateWithMinimum(string name, float value, float minimum, string? description = null,
        bool isEnabled = true)
    {
        return new FloatOption(name, value, minimum, float.MaxValue, description, isEnabled);
    }    
    
    public static FloatOption CreateWithMinimum(string name, float value, double minimum, string? description = null,
        bool isEnabled = true)
    {
        return new FloatOption(name, value, minimum, float.MaxValue, description, isEnabled);
    }

    public static FloatOption CreateWithMaximum(string name, float value, float maximum, string? description = null,
        bool isEnabled = true)
    {
        return new FloatOption(name,value, float.MinValue, maximum, description, isEnabled);
    }
    
    public static FloatOption CreateWithMaximum(string name, float value, double maximum, string? description = null,
        bool isEnabled = true)
    {
        return new FloatOption(name,value, float.MinValue, maximum, description, isEnabled);
    }
    
    public FloatOption(string name, float value, string? description = null, bool isEnabled = true)
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
    
    public FloatOption(string name, float value, float minimum, float maximum, string? description = null,
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
    
    public FloatOption(string name, float value, double minimum, double maximum, string? description = null,
        bool isEnabled = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException(@"Name cannot be null or empty.", nameof(name));
        }

        Name = name;
        Value = value;
        Minimum = Convert.ToSingle(minimum);
        Maximum = Convert.ToSingle(maximum);
        Description = description;
        IsEnabled = isEnabled;
    }
}