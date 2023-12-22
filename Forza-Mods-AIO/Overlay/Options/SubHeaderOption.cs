using System;

namespace Forza_Mods_AIO.Overlay.Options;

public class SubHeaderOption : MenuOption
{
    public SubHeaderOption(string name, string? description = null, bool isEnabled = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException(@"Name cannot be null or empty.", nameof(name));
        }

        Name = name;
        Description = description;
        IsEnabled = isEnabled;
    }
}