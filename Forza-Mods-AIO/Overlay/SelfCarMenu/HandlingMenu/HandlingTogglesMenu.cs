using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu;

public abstract class HandlingTogglesMenu
{
    #region Submenu Options

    private static readonly Overlay.MenuOption CarNoClipToggle = new("Car No-Clip", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption WallNoClipToggle = new("Wall No-Clip", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption StopAllWheelsToggle = new("Stop All Wheels", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption SuperBrakeToggle = new("Super Brake", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption SuperCarToggle = new("Super Car", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption StopWaterDragToggle = new("Stop Water Drag", Overlay.MenuOption.OptionType.Bool, false);

    #endregion
    
    public static readonly List<Overlay.MenuOption> HandlingTogglesOptions = new()
    {
        new ("No-Clips", Overlay.MenuOption.OptionType.SubHeader),
        CarNoClipToggle,
        WallNoClipToggle,
        new ("Braking", Overlay.MenuOption.OptionType.SubHeader),
        StopAllWheelsToggle,
        SuperBrakeToggle,
        new ("Other", Overlay.MenuOption.OptionType.SubHeader),
        SuperCarToggle,
        StopWaterDragToggle
    };

    public static void InitiateSubMenu()
    {
        CarNoClipToggle.ValueChangedHandler += NoClip_OnToggled;
        WallNoClipToggle.ValueChangedHandler += NoClip_OnToggled;

        StopAllWheelsToggle.ValueChangedHandler += Braking_OnToggled;
        SuperBrakeToggle.ValueChangedHandler += Braking_OnToggled;
        
        SuperCarToggle.ValueChangedHandler += Other_OnToggled;
        StopWaterDragToggle.ValueChangedHandler += Other_OnToggled;
    }

    #region NoClip Toggles

    private static void NoClip_OnToggled(object? s, EventArgs e)
    {
        HandlingPage.shp.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Car No-Clip":
                {
                    HandlingPage.shp.CarNoclipSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Wall No-Clip":
                {
                    HandlingPage.shp.WallNoclipSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
            
        });
    }

    #endregion

    #region Braking Toggles

    private static void Braking_OnToggled(object? s, EventArgs e)
    {
        HandlingPage.shp.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Stop All Wheels":
                {
                    SuperBrakeToggle.IsEnabled = !SuperBrakeToggle.IsEnabled;
                    HandlingPage.shp.StopAllWheelsSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Super Brake":
                {                    
                    StopAllWheelsToggle.IsEnabled = !StopAllWheelsToggle.IsEnabled;
                    HandlingPage.shp.SuperBrakeSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }

    #endregion

    #region Other Toggles

    private static void Other_OnToggled(object? s, EventArgs e)
    {
        HandlingPage.shp.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Super Car":
                {
                    HandlingPage.shp.SuperCarSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Stop Water Drag":
                {                    
                    HandlingPage.shp.WaterDragSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }

    #endregion
}