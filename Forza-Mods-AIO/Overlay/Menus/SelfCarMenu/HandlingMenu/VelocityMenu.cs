using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu.HandlingMenu;

public abstract class VelocityMenu
{
    private static readonly string[] Selections = { "Dynamic", "Static" };
    private static readonly SelectionOption VelocityMode = new("Mode", 0, Selections);
    private static readonly FloatOption VelocityValue = new("Value", 5, Shp.VelValueNum.Minimum, Shp.VelValueNum.Maximum);
    private static readonly IntOption VelocityLimit = new("Limit", 300, Shp.VelLimitBox.Minimum, Shp.VelLimitBox.Maximum);
    private static readonly ToggleOption VelocityToggle = new("Enable", false);

    public static void InitiateSubMenu()
    {
        VelocityMode.SelectionChanged += VelocitySelectionChanged;
        VelocityValue.ValueChanged += VelocityValueChanged;
        VelocityLimit.ValueChanged += VelocityLimitChanged;
        VelocityToggle.Toggled += VelocityToggleChanged;
    }

    private static void VelocitySelectionChanged(object s, EventArgs e)
    {
        Shp.VelModeBox.SelectedIndex = VelocityMode.Index;
    }
    
    private static void VelocityValueChanged(object s, EventArgs e)
    {
        Shp.VelValueNum.Value = VelocityValue.Value;
    }

    private static void VelocityLimitChanged(object s, EventArgs e)
    {
        Shp.VelLimitBox.Value = VelocityLimit.Value;
    }

    private static void VelocityToggleChanged(object s, EventArgs e)
    {
        Shp.VelocitySwitch.IsOn = VelocityToggle.IsOn;
    }

    public static readonly List<MenuOption> VelocityOptions = new()
    {
        new SubHeaderOption("Velocity"),
        VelocityMode,
        VelocityValue,
        VelocityLimit,
        VelocityToggle
    };
}