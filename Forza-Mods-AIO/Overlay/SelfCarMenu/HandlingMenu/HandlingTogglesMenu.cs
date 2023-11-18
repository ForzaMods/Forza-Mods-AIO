using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu;

public abstract class HandlingTogglesMenu
{
    #region Submenu Options

    private static readonly MenuOption CarNoClipToggle = new("Car No-Clip", OptionType.Bool, false);
    private static readonly MenuOption WallNoClipToggle = new("Wall No-Clip", OptionType.Bool, false);
    private static readonly MenuOption StopAllWheelsToggle = new("Stop All Wheels", OptionType.Bool, false);
    private static readonly MenuOption SuperBrakeToggle = new("Super Brake", OptionType.Bool, false);
    private static readonly MenuOption SuperCarToggle = new("Super Car", OptionType.Bool, false);
    private static readonly MenuOption StopWaterDragToggle = new("Stop Water Drag", OptionType.Bool, false);

    #endregion
    
    public static readonly List<MenuOption> HandlingTogglesOptions = new()
    {
        new ("No-Clips", OptionType.SubHeader),
        CarNoClipToggle,
        WallNoClipToggle,
        new ("Braking", OptionType.SubHeader),
        StopAllWheelsToggle,
        SuperBrakeToggle,
        new ("Other", OptionType.SubHeader),
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
        HandlingPage.Shp.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Car No-Clip":
                {
                    HandlingPage.Shp.CarNoclipSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Wall No-Clip":
                {
                    HandlingPage.Shp.WallNoclipSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
            
        });
    }

    #endregion

    #region Braking Toggles

    private static void Braking_OnToggled(object? s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Stop All Wheels":
                {
                    SuperBrakeToggle.IsEnabled = !SuperBrakeToggle.IsEnabled;
                    HandlingPage.Shp.StopAllWheelsSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Super Brake":
                {                    
                    StopAllWheelsToggle.IsEnabled = !StopAllWheelsToggle.IsEnabled;
                    HandlingPage.Shp.SuperBrakeSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }

    #endregion

    #region Other Toggles

    private static void Other_OnToggled(object? s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Super Car":
                {
                    HandlingPage.Shp.SuperCarSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Stop Water Drag":
                {                    
                    HandlingPage.Shp.WaterDragSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }

    #endregion
}