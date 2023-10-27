using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.FovMenu;

public abstract class FovLimiters
{
    public static readonly List<Overlay.MenuOption> FovLimiterOptions = new()
    {
        new ("Chase Limiters", "MenuButton"),
        new ("Driver Limiters", "MenuButton"),
        new ("Hood Limiters", "MenuButton"),
        new ("Bumper Limiters", "MenuButton")
    };
    
    
    private static readonly Overlay.MenuOption ChaseMinValue = new ("Min Value", "Float", 0f);
    private static readonly Overlay.MenuOption ChaseMaxValue = new ("Max Value", "Float", 0f);
    private static readonly Overlay.MenuOption FarChaseMinValue = new ("Min Value", "Float", 0f);
    private static readonly Overlay.MenuOption FarChaseMaxValue = new ("Max Value", "Float", 0f);
    
    private static readonly Overlay.MenuOption ChasePull = new ("Pull values", "Button", new Action(() =>
    {
        var FovLimiters = Tabs.Self_Vehicle.DropDownTabs.FovPage._fovPage;
        
        FovLimiters.Dispatcher.Invoke(() =>
        {
            if (FovLimiters.ChaseMinNum.Value == null || FovLimiters.ChaseMaxNum.Value == null)
                return;

            HoodMinValue.Value = (float)FovLimiters.ChaseMinNum.Value;
            HoodMaxValue.Value = (float)FovLimiters.ChaseMaxNum.Value;
        });
    }));
    
    private static readonly Overlay.MenuOption FarChasePull = new ("Pull values", "Button", new Action(() =>
    {
        var FovLimiters = Tabs.Self_Vehicle.DropDownTabs.FovPage._fovPage;
        
        FovLimiters.Dispatcher.Invoke(() =>
        {
            if (FovLimiters.FarChaseMinNum.Value == null || FovLimiters.FarChaseMaxNum.Value == null)
                return;

            HoodMinValue.Value = (float)FovLimiters.FarChaseMinNum.Value;
            HoodMaxValue.Value = (float)FovLimiters.FarChaseMaxNum.Value;
        });
    }));
    
    public static readonly List<Overlay.MenuOption> ChaseLimitersOptions = new()
    {
        new ("Chase Limiters", "SubHeader"),
        ChaseMinValue,
        ChaseMaxValue,
        ChasePull,
        new ("Far Chase Limiters", "SubHeader"),
        FarChaseMinValue,
        FarChaseMaxValue,
        FarChasePull
    };
    
    
    private static readonly Overlay.MenuOption DriverMinValue = new ("Min Value", "Float", 0f);
    private static readonly Overlay.MenuOption DriverMaxValue = new ("Max Value", "Float", 0f);
    
    private static readonly Overlay.MenuOption DriverPull = new ("Pull values", "Button", new Action(() =>
    {
        var FovPage = Tabs.Self_Vehicle.DropDownTabs.FovPage._fovPage;
        
        FovPage.Dispatcher.Invoke(() =>
        {
            if (FovPage.DriverMinNum.Value == null || FovPage.DriverMaxNum.Value == null)
                return;

            HoodMinValue.Value = (float)FovPage.DriverMinNum.Value;
            HoodMaxValue.Value = (float)FovPage.DriverMaxNum.Value;
        });
    }));
    
    public static readonly List<Overlay.MenuOption> DriverLimitersOptions = new()
    {
        new ("Driver Limiters", "SubHeader"),
        DriverMinValue,
        DriverMaxValue,
        DriverPull
    };
    
    private static readonly Overlay.MenuOption HoodMinValue = new ("Min Value", "Float", 0f);
    private static readonly Overlay.MenuOption HoodMaxValue = new ("Max Value", "Float", 0f);
    private static readonly Overlay.MenuOption BumperMinValue = new ("Min Value", "Float", 0f);
    private static readonly Overlay.MenuOption BumperMaxValue = new ("Max Value", "Float", 0f);
    
    private static readonly Overlay.MenuOption HoodPull = new ("Pull values", "Button", new Action(() =>
    {
        var FovPage = Tabs.Self_Vehicle.DropDownTabs.FovPage._fovPage;
        
        FovPage.Dispatcher.Invoke(() =>
        {
            if (FovPage.HoodMinNum.Value == null || FovPage.HoodMaxNum.Value == null)
                return;

            HoodMinValue.Value = (float)FovPage.HoodMinNum.Value;
            HoodMaxValue.Value = (float)FovPage.HoodMaxNum.Value;
        });
    }));
    
    private static readonly Overlay.MenuOption BumperPull = new ("Pull values", "Button", new Action(() =>
    {
        var FovPage = Tabs.Self_Vehicle.DropDownTabs.FovPage._fovPage;
        
        FovPage.Dispatcher.Invoke(() =>
        {
            if (FovPage.BumperMinNum.Value == null || FovPage.BumperMaxNum.Value == null)
                return;

            HoodMinValue.Value = (float)FovPage.BumperMinNum.Value;
            HoodMaxValue.Value = (float)FovPage.BumperMaxNum.Value;
        });
    }));
    
    public static readonly List<Overlay.MenuOption> HoodLimitersOptions = new()
    {
        new ("Hood Limiters", "SubHeader"),
        HoodMinValue,
        HoodMaxValue,
        HoodPull
    };

    public static readonly List<Overlay.MenuOption> BumperLimitersOptions = new()
    {
        new("Bumper Limiters", "SubHeader"),
        BumperMinValue,
        BumperMaxValue,
        BumperPull
    };

    public static void InitiateSubMenu()
    {
        
    }
}