using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.FovMenu;

public abstract class FovLimiters
{
    public static readonly List<Overlay.MenuOption> FovLimiterOptions = new()
    {
        new ("Chase Limiters", Overlay.MenuOption.OptionType.MenuButton),
        new ("Driver Limiters", Overlay.MenuOption.OptionType.MenuButton),
        new ("Hood Limiters", Overlay.MenuOption.OptionType.MenuButton),
        new ("Bumper Limiters", Overlay.MenuOption.OptionType.MenuButton)
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

    private static readonly Overlay.MenuOption ChaseMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption ChaseMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption FarChaseMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption FarChaseMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption ChasePull = new ("Pull values", Overlay.MenuOption.OptionType.Button, () =>
    {
        var FovLimiters = FovPage._fovPage!;
        
        FovLimiters.Dispatcher.Invoke(() =>
        {
            if (FovLimiters.ChaseMinNum.Value == null || FovLimiters.ChaseMaxNum.Value == null)
                return;

            ChaseMinValue.Value = (float)FovLimiters.ChaseMinNum.Value;
            ChaseMaxValue.Value = (float)FovLimiters.ChaseMaxNum.Value;
        });
    });
    
    private static readonly Overlay.MenuOption FarChasePull = new ("Pull values", Overlay.MenuOption.OptionType.Button, () =>
    {
        var FovLimiters = FovPage._fovPage!;
        
        FovLimiters.Dispatcher.Invoke(() =>
        {
            if (FovLimiters.FarChaseMinNum.Value == null || FovLimiters.FarChaseMaxNum.Value == null)
                return;

            FarChaseMinValue.Value = (float)FovLimiters.FarChaseMinNum.Value;
            FarChaseMaxValue.Value = (float)FovLimiters.FarChaseMaxNum.Value;
        });
    });
    
    public static readonly List<Overlay.MenuOption> ChaseLimitersOptions = new()
    {
        new ("Chase Limiters", Overlay.MenuOption.OptionType.SubHeader),
        ChaseMinValue,
        ChaseMaxValue,
        ChasePull,
        new ("Far Chase Limiters", Overlay.MenuOption.OptionType.SubHeader),
        FarChaseMinValue,
        FarChaseMaxValue,
        FarChasePull
    };

    #endregion

    #region Driver
    
    private static readonly Overlay.MenuOption DriverMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption DriverMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption DriverPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, () =>
    {
        var FovLimiters = FovPage._fovPage!;
        
        FovLimiters.Dispatcher.Invoke(() =>
        {
            if (FovLimiters.DriverMinNum.Value == null || FovLimiters.DriverMaxNum.Value == null)
                return;

            DriverMinValue.Value = (float)FovLimiters.DriverMinNum.Value;
            DriverMaxValue.Value = (float)FovLimiters.DriverMaxNum.Value;
        });
    });
    
    public static readonly List<Overlay.MenuOption> DriverLimitersOptions = new()
    {
        new ("Driver Limiters", Overlay.MenuOption.OptionType.SubHeader),
        DriverMinValue,
        DriverMaxValue,
        DriverPull
    };
    #endregion

    #region Hood

    private static readonly Overlay.MenuOption HoodMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption HoodMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption HoodPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, () =>
    {
        var FovLimiters = FovPage._fovPage!;
        
        FovLimiters.Dispatcher.Invoke(() =>
        {
            if (FovLimiters.HoodMinNum.Value == null || FovLimiters.HoodMaxNum.Value == null)
                return;

            HoodMinValue.Value = (float)FovLimiters.HoodMinNum.Value;
            HoodMaxValue.Value = (float)FovLimiters.HoodMaxNum.Value;
        });
    });
        
    public static readonly List<Overlay.MenuOption> HoodLimitersOptions = new()
    {
        new ("Hood Limiters", Overlay.MenuOption.OptionType.SubHeader),
        HoodMinValue,
        HoodMaxValue,
        HoodPull
    };

    #endregion

    #region Bumper

    private static readonly Overlay.MenuOption BumperMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption BumperMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption BumperPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, () =>
    {
        var FovLimiters = FovPage._fovPage!;
        
        FovLimiters.Dispatcher.Invoke(() =>
        {
            if (FovLimiters.BumperMinNum.Value == null || FovLimiters.BumperMaxNum.Value == null)
                return;

            BumperMinValue.Value = (float)FovLimiters.BumperMinNum.Value;
            BumperMaxValue.Value = (float)FovLimiters.BumperMaxNum.Value;
        });
    });


    public static readonly List<Overlay.MenuOption> BumperLimitersOptions = new()
    {
        new("Bumper Limiters", Overlay.MenuOption.OptionType.SubHeader),
        BumperMinValue,
        BumperMaxValue,
        BumperPull
    };

    #endregion

    #region Event Handlers

    private static void ChaseLimiters_OnChanged(object? s, EventArgs e)
    {
        FovPage._fovPage!.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Min Value":
                {
                    FovPage._fovPage.ChaseMinNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Max Value":
                {
                    FovPage._fovPage.ChaseMaxNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }
    
    private static void FarChaseLimiters_OnChanged(object? s, EventArgs e)
    {
        FovPage._fovPage!.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Min Value":
                {
                    FovPage._fovPage.FarChaseMinNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Max Value":
                {
                    FovPage._fovPage.FarChaseMaxNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }
    
    private static void DriverLimiters_OnChanged(object? s, EventArgs e)
    {
        FovPage._fovPage!.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Min Value":
                {
                    FovPage._fovPage.DriverMinNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Max Value":
                {
                    FovPage._fovPage.DriverMaxNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }
    
    private static void HoodLimiters_OnChanged(object? s, EventArgs e)
    {
        FovPage._fovPage!.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Min Value":
                {
                    FovPage._fovPage.HoodMinNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Max Value":
                {
                    FovPage._fovPage.HoodMaxNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }
    
    private static void BumperLimiters_OnChanged(object? s, EventArgs e)
    {
        FovPage._fovPage!.Dispatcher.Invoke(() =>
        {
            switch ((string)s!.GetType().GetProperty("Name")!.GetValue(s)!)
            {
                case "Min Value":
                {
                    FovPage._fovPage.BumperMinNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
                case "Max Value":
                {
                    FovPage._fovPage.BumperMaxNum.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
                    break;
                }
            }
        });
    }

    #endregion

}