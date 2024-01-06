using System;
using System.Windows;

namespace Forza_Mods_AIO.Overlay.Options;

public sealed class SelectionOption : MenuOption
{
    public int Index
    {
        get => _index;
        set
        {
            _index = value;
            OnSelectionChanged();
        }
    }

    private int _index;
    public string[] Selections { get; }

    public event RoutedEventHandler? SelectionChanged;

    public SelectionOption(string name, int index, string[] selections, string? description = null, bool isEnabled = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException(@"Name cannot be null or empty.", nameof(name));
        }

        if (selections == null || selections.Length == 0)
        {
            throw new ArgumentException(@"Selections cannot be null or empty.", nameof(selections));
        }

        Name = name;
        Index = index;
        Selections = selections;
        Description = description;
        IsEnabled = isEnabled;
    }

    private void OnSelectionChanged()
    {
        var handler = SelectionChanged;
        if (handler == null)
        {
            return;
        }
        
        Application.Current.Dispatcher.BeginInvoke(() => handler(this, new RoutedEventArgs()));
    }
}
