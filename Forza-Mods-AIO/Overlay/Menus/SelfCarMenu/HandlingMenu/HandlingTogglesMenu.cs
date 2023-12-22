using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu.HandlingMenu;

public abstract class HandlingTogglesMenu
{
    #region Submenu Options

    private static readonly ToggleOption CarNoClipToggle = new("Car No-Clip", false);
    private static readonly ToggleOption WallNoClipToggle = new("Wall No-Clip", false);
    private static readonly ToggleOption StopAllWheelsToggle = new("Stop All Wheels", false);
    private static readonly ToggleOption SuperBrakeToggle = new("Super Brake", false);
    private static readonly ToggleOption SuperCarToggle = new("Super Car", false);
    private static readonly ToggleOption StopWaterDragToggle = new("Stop Water Drag", false);

    #endregion
    
    public static readonly List<MenuOption> HandlingTogglesOptions = new()
    {
        new SubHeaderOption("No-Clips"),
        CarNoClipToggle,
        WallNoClipToggle,
        new SubHeaderOption("Braking"),
        StopAllWheelsToggle,
        SuperBrakeToggle,
        new SubHeaderOption("Other"),
        SuperCarToggle,
        StopWaterDragToggle
    };

    public static void InitiateSubMenu()
    {
        CarNoClipToggle.ToggledEventHandler += NoClip_OnToggled;
        WallNoClipToggle.ToggledEventHandler += NoClip_OnToggled;

        StopAllWheelsToggle.ToggledEventHandler += Braking_OnToggled;
        SuperBrakeToggle.ToggledEventHandler += Braking_OnToggled;
        
        SuperCarToggle.ToggledEventHandler += Other_OnToggled;
        StopWaterDragToggle.ToggledEventHandler += Other_OnToggled;
    }

    #region NoClip Toggles

    private static void NoClip_OnToggled(object? s, EventArgs e)
    {
        if (s is not ToggleOption toggleOption)
        {
            return;
        }
        
        switch (toggleOption.Name)
        {
            case "Car No-Clip":
            {
                Shp.CarNoclipSwitch.IsOn = toggleOption.IsOn;
                break;
            }
            case "Wall No-Clip":
            {
                Shp.WallNoclipSwitch.IsOn = toggleOption.IsOn;
                break;
            }
        }    }

    #endregion

    #region Braking Toggles

    private static void Braking_OnToggled(object? s, EventArgs e)
    {
        if (s is not ToggleOption toggleOption)
        {
            return;
        }
        
        switch (toggleOption.Name)
        {
            case "Stop All Wheels":
            {
                SuperBrakeToggle.IsEnabled = !SuperBrakeToggle.IsEnabled;
                Shp.StopAllWheelsSwitch.IsOn = toggleOption.IsOn;
                break;
            }
            case "Super Brake":
            {                    
                StopAllWheelsToggle.IsEnabled = !StopAllWheelsToggle.IsEnabled;
                Shp.SuperBrakeSwitch.IsOn = toggleOption.IsOn;
                break;
            }
        }
    }

    #endregion

    #region Other Toggles

    private static void Other_OnToggled(object? s, EventArgs e)
    {
        if (s is not ToggleOption toggleOption)
        {
            return;
        }

        switch (toggleOption.Name)
        {
            case "Super Car":
            {
                Shp.SuperCarSwitch.IsOn = toggleOption.IsOn;
                break;
            }
            case "Stop Water Drag":
            {                    
                Shp.WaterDragSwitch.IsOn = toggleOption.IsOn;
                break;
            }
        }
    }

    #endregion
}