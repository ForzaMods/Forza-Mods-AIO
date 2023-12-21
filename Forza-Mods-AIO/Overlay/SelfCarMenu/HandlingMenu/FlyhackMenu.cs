using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu;

public abstract class FlyhackMenu
{
    private static readonly MenuOption FlyhackRotationSpeed = new("Rotation Speed", 1);
    private static readonly MenuOption FlyhackMovementSpeed = new("Movement Speed", 1);
    private static readonly MenuOption FlyhackToggle = new("Toggle",false);

    public static readonly List<MenuOption> FlyhackOptions = new()
    {
        new MenuOption("Flyhack", OptionType.SubHeader),
        FlyhackRotationSpeed,
        FlyhackMovementSpeed,
        FlyhackToggle
    };

    public static void InitiateSubMenu()
    {
        FlyhackRotationSpeed.ValueChangedHandler += FlyhackRotationSpeedChanged;
        FlyhackMovementSpeed.ValueChangedHandler += FlyhackMovementSpeedChanged;
        FlyhackToggle.ValueChangedHandler += FlyhackToggled;
    }

    #region Eventhandlers

    private static void FlyhackRotationSpeedChanged(object s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.Shp.FlyHackRotSpeedNum.Maximum)
                FlyhackRotationSpeed.Value = (int)HandlingPage.Shp.FlyHackRotSpeedNum.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.Shp.FlyHackRotSpeedNum.Minimum)
                FlyhackRotationSpeed.Value = (int)HandlingPage.Shp.FlyHackRotSpeedNum.Minimum;
            else
                HandlingPage.Shp.FlyHackRotSpeedNum.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
        });
    }

    private static void FlyhackMovementSpeedChanged(object s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.Shp.FlyHackMoveSpeedNum.Maximum)
                FlyhackMovementSpeed.Value = (int)HandlingPage.Shp.FlyHackMoveSpeedNum.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.Shp.FlyHackMoveSpeedNum.Minimum)
                FlyhackMovementSpeed.Value = (int)HandlingPage.Shp.FlyHackMoveSpeedNum.Minimum;
            else
                HandlingPage.Shp.FlyHackMoveSpeedNum.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
        });
    }

    private static void FlyhackToggled(object s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(() =>
        {
            HandlingPage.Shp.FlyHackSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    #endregion

}