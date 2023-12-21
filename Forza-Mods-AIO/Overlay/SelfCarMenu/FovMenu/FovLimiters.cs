using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.FovMenu;

public abstract class FovLimiters
{
    public static readonly List<MenuOption> FovLimiterOptions = new()
    {
        new ("Chase Limiters", OptionType.MenuButton),
        new ("Driver Limiters", OptionType.MenuButton),
        new ("Hood Limiters", OptionType.MenuButton),
        new ("Bumper Limiters", OptionType.MenuButton)
    };
    
    public static void InitiateSubMenu()
    {
        ChaseMinValue.ValueChangedHandler += ChaseLimiters_OnChanged;
        ChaseMaxValue.ValueChangedHandler += ChaseLimiters_OnChanged;

        FarChaseMinValue.ValueChangedHandler += FarChaseLimiters_OnChanged;
        FarChaseMaxValue.ValueChangedHandler += FarChaseLimiters_OnChanged;
        
        DriverMinValue.ValueChangedHandler += DriverLimiters_OnChanged;
        DriverMaxValue.ValueChangedHandler += DriverLimiters_OnChanged;      
        
        HoodMinValue.ValueChangedHandler += HoodLimiters_OnChanged;
        HoodMaxValue.ValueChangedHandler += HoodLimiters_OnChanged;
        
        BumperMinValue.ValueChangedHandler += BumperLimiters_OnChanged;
        BumperMaxValue.ValueChangedHandler += BumperLimiters_OnChanged;
    }

    #region Chase and Far Chase

    private static readonly MenuOption ChaseMinValue = new ("Min Value", 0f);
    private static readonly MenuOption ChaseMaxValue = new ("Max Value", 0f);
    private static readonly MenuOption FarChaseMinValue = new ("Min Value", 0f);
    private static readonly MenuOption FarChaseMaxValue = new ("Max Value", 0f);
    
    private static readonly MenuOption ChasePull = new ("Pull values",  () =>
    {
        var fovLimiters = FovPage.Fov!;
        
        fovLimiters.Dispatcher.Invoke(() =>
        {
            if (fovLimiters.ChaseMinNum.Value == null || fovLimiters.ChaseMaxNum.Value == null)
                return;

            ChaseMinValue.Value = (float)fovLimiters.ChaseMinNum.Value;
            ChaseMaxValue.Value = (float)fovLimiters.ChaseMaxNum.Value;
        });
    });
    
    private static readonly MenuOption FarChasePull = new ("Pull values",  () =>
    {
        var fovLimiters = FovPage.Fov!;
        
        fovLimiters.Dispatcher.Invoke(() =>
        {
            if (fovLimiters.FarChaseMinNum.Value == null || fovLimiters.FarChaseMaxNum.Value == null)
                return;

            FarChaseMinValue.Value = (float)fovLimiters.FarChaseMinNum.Value;
            FarChaseMaxValue.Value = (float)fovLimiters.FarChaseMaxNum.Value;
        });
    });
    
    public static readonly List<MenuOption> ChaseLimitersOptions = new()
    {
        new ("Chase Limiters", OptionType.SubHeader),
        ChaseMinValue,
        ChaseMaxValue,
        ChasePull,
        new ("Far Chase Limiters", OptionType.SubHeader),
        FarChaseMinValue,
        FarChaseMaxValue,
        FarChasePull
    };

    #endregion

    #region Driver
    
    private static readonly MenuOption DriverMinValue = new ("Min Value", 0f);
    private static readonly MenuOption DriverMaxValue = new ("Max Value", 0f);
    
    private static readonly MenuOption DriverPull = new ("Pull values",  () =>
    {
        var fovLimiters = FovPage.Fov!;
        
        fovLimiters.Dispatcher.Invoke(() =>
        {
            if (fovLimiters.DriverMinNum.Value == null || fovLimiters.DriverMaxNum.Value == null)
                return;

            DriverMinValue.Value = (float)fovLimiters.DriverMinNum.Value;
            DriverMaxValue.Value = (float)fovLimiters.DriverMaxNum.Value;
        });
    });
    
    public static readonly List<MenuOption> DriverLimitersOptions = new()
    {
        new ("Driver Limiters", OptionType.SubHeader),
        DriverMinValue,
        DriverMaxValue,
        DriverPull
    };
    #endregion

    #region Hood

    private static readonly MenuOption HoodMinValue = new ("Min Value", 0f);
    private static readonly MenuOption HoodMaxValue = new ("Max Value", 0f);
    
    private static readonly MenuOption HoodPull = new ("Pull values",  () =>
    {
        var fovLimiters = FovPage.Fov!;
        
        fovLimiters.Dispatcher.Invoke(() =>
        {
            if (fovLimiters.HoodMinNum.Value == null || fovLimiters.HoodMaxNum.Value == null)
                return;

            HoodMinValue.Value = (float)fovLimiters.HoodMinNum.Value;
            HoodMaxValue.Value = (float)fovLimiters.HoodMaxNum.Value;
        });
    });
        
    public static readonly List<MenuOption> HoodLimitersOptions = new()
    {
        new ("Hood Limiters", OptionType.SubHeader),
        HoodMinValue,
        HoodMaxValue,
        HoodPull
    };

    #endregion

    #region Bumper

    private static readonly MenuOption BumperMinValue = new ("Min Value", 0f);
    private static readonly MenuOption BumperMaxValue = new ("Max Value", 0f);
    
    private static readonly MenuOption BumperPull = new ("Pull values",  () =>
    {
        var fovLimiters = FovPage.Fov!;
        
        fovLimiters.Dispatcher.Invoke(() =>
        {
            if (fovLimiters.BumperMinNum.Value == null || fovLimiters.BumperMaxNum.Value == null)
                return;

            BumperMinValue.Value = (float)fovLimiters.BumperMinNum.Value;
            BumperMaxValue.Value = (float)fovLimiters.BumperMaxNum.Value;
        });
    });


    public static readonly List<MenuOption> BumperLimitersOptions = new()
    {
        new("Bumper Limiters", OptionType.SubHeader),
        BumperMinValue,
        BumperMaxValue,
        BumperPull
    };

    #endregion

    #region Event Handlers

    private static void ChaseLimiters_OnChanged(object? s, EventArgs e)
    {
        FovPage.Fov!.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Min Value":
                {
                    FovPage.Fov.ChaseMinNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Max Value":
                {
                    FovPage.Fov.ChaseMaxNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }
    
    private static void FarChaseLimiters_OnChanged(object? s, EventArgs e)
    {
        FovPage.Fov!.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Min Value":
                {
                    FovPage.Fov.FarChaseMinNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Max Value":
                {
                    FovPage.Fov.FarChaseMaxNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }
    
    private static void DriverLimiters_OnChanged(object? s, EventArgs e)
    {
        FovPage.Fov!.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Min Value":
                {
                    FovPage.Fov.DriverMinNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Max Value":
                {
                    FovPage.Fov.DriverMaxNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }
    
    private static void HoodLimiters_OnChanged(object? s, EventArgs e)
    {
        FovPage.Fov!.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Min Value":
                {
                    FovPage.Fov.HoodMinNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Max Value":
                {
                    FovPage.Fov.HoodMaxNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }
    
    private static void BumperLimiters_OnChanged(object? s, EventArgs e)
    {
        FovPage.Fov!.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Min Value":
                {
                    FovPage.Fov.BumperMinNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Max Value":
                {
                    FovPage.Fov.BumperMaxNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }

    #endregion

}