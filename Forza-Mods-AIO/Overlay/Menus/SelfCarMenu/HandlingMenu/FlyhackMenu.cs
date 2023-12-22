using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu.HandlingMenu;

public abstract class FlyhackMenu
{
    private static readonly IntOption FlyhackRotationSpeed = new("Rotation Speed", 1, Shp.FlyHackRotSpeedNum.Minimum, Shp.FlyHackRotSpeedNum.Maximum);
    private static readonly IntOption FlyhackMovementSpeed = new("Movement Speed", 1, Shp.FlyHackMoveSpeedNum.Minimum, Shp.FlyHackMoveSpeedNum.Maximum);
    private static readonly ToggleOption FlyhackToggle = new("Toggle",false);

    public static readonly List<MenuOption> FlyhackOptions = new()
    {
        new SubHeaderOption("Flyhack"),
        FlyhackRotationSpeed,
        FlyhackMovementSpeed,
        FlyhackToggle
    };

    public static void InitiateSubMenu()
    {
        FlyhackRotationSpeed.ValueChangedEventHandler += FlyhackRotationSpeedChanged;
        FlyhackMovementSpeed.ValueChangedEventHandler += FlyhackMovementSpeedChanged;
        FlyhackToggle.ToggledEventHandler += FlyhackToggled;
    }

    #region Eventhandlers

    private static void FlyhackRotationSpeedChanged(object s, EventArgs e)
    {
        if (s is not IntOption intOption)
        {
            return;
        }

        Shp.FlyHackRotSpeedNum.Value = intOption.Value;
    }

    private static void FlyhackMovementSpeedChanged(object s, EventArgs e)
    {
        if (s is not IntOption intOption)
        {
            return;
        }
        
        Shp.FlyHackMoveSpeedNum.Value = intOption.Value;
    }

    private static void FlyhackToggled(object s, EventArgs e)
    {
        if (s is not ToggleOption toggleOption)
        {
            return;
        }
        
        Shp.FlyHackSwitch.IsOn = toggleOption.IsOn;
    }

    #endregion

}