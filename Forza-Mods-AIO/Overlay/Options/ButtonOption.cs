using System;

namespace Forza_Mods_AIO.Overlay.Options;

public class ButtonOption : MenuOption
{
    public Action Action { get; }

    public ButtonOption(string name, Action action, string? description = null, bool isEnabled = true)
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