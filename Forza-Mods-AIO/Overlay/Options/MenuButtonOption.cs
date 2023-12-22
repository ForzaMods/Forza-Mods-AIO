using System;

namespace Forza_Mods_AIO.Overlay.Options;

public class MenuButtonOption : MenuOption
{
    public Action? Action { get; }
    
    public MenuButtonOption(string name, string? description = null, bool isEnabled = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException(@"Name cannot be null or empty.", nameof(name));
        }

        Name = name;
        Description = description;
        IsEnabled = isEnabled;
    }

    public MenuButtonOption(string name, Action action, string? description = null, bool isEnabled = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException(@"Name cannot be null or empty.", nameof(name));
        }

        Name = name;
        Action = action;
        Description = description;
        IsEnabled = isEnabled;
    }
}