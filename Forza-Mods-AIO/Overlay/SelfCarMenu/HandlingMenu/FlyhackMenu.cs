using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu;

public abstract class FlyhackMenu
{
    private static readonly Overlay.MenuOption FlyhackRotationSpeed = new("Rotation Speed", Overlay.MenuOption.OptionType.Int, 1);
    private static readonly Overlay.MenuOption FlyhackMovementSpeed = new("Movement Speed", Overlay.MenuOption.OptionType.Int, 1);
    private static readonly Overlay.MenuOption FlyhackToggle = new("Toggle", Overlay.MenuOption.OptionType.Bool, false);

    public static readonly List<Overlay.MenuOption> FlyhackOptions = new()
    {
        new ("Flyhack", Overlay.MenuOption.OptionType.SubHeader),
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
        HandlingPage.shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.shp.FlyHackRotSpeedNum.Maximum)
                FlyhackRotationSpeed.Value = (int)HandlingPage.shp.FlyHackRotSpeedNum.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.shp.FlyHackRotSpeedNum.Minimum)
                FlyhackRotationSpeed.Value = (int)HandlingPage.shp.FlyHackRotSpeedNum.Minimum;
            else
                HandlingPage.shp.FlyHackRotSpeedNum.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
        });
    }

    private static void FlyhackMovementSpeedChanged(object s, EventArgs e)
    {
        HandlingPage.shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.shp.FlyHackMoveSpeedNum.Maximum)
                FlyhackMovementSpeed.Value = (int)HandlingPage.shp.FlyHackMoveSpeedNum.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.shp.FlyHackMoveSpeedNum.Minimum)
                FlyhackMovementSpeed.Value = (int)HandlingPage.shp.FlyHackMoveSpeedNum.Minimum;
            else
                HandlingPage.shp.FlyHackMoveSpeedNum.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
        });
    }

    private static void FlyhackToggled(object s, EventArgs e)
    {
        HandlingPage.shp.Dispatcher.Invoke(() =>
        {
            HandlingPage.shp.FlyHackSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    #endregion

}