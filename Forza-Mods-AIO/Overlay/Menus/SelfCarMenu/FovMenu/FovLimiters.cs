using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.CameraPage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu.FovMenu;

public abstract class FovLimiters
{
    public static readonly List<MenuOption> FovLimiterOptions = new()
    {
        new MenuButtonOption("Chase Limiters"),
        new MenuButtonOption("Driver Limiters"),
        new MenuButtonOption("Hood Limiters"),
        new MenuButtonOption("Bumper Limiters")
    };
    
    public static void InitiateSubMenu()
    {
        ChaseMinValue.ValueChanged += ChaseLimiters_OnChanged;
        ChaseMaxValue.ValueChanged += ChaseLimiters_OnChanged;

        FarChaseMinValue.ValueChanged += FarChaseLimiters_OnChanged;
        FarChaseMaxValue.ValueChanged += FarChaseLimiters_OnChanged;
        
        DriverMinValue.ValueChanged += DriverLimiters_OnChanged;
        DriverMaxValue.ValueChanged += DriverLimiters_OnChanged;      
        
        HoodMinValue.ValueChanged += HoodLimiters_OnChanged;
        HoodMaxValue.ValueChanged += HoodLimiters_OnChanged;
        
        BumperMinValue.ValueChanged += BumperLimiters_OnChanged;
        BumperMaxValue.ValueChanged += BumperLimiters_OnChanged;
    }

    #region Chase and Far Chase

    private static readonly FloatOption ChaseMinValue = new("Min Value", 0f);
    private static readonly FloatOption ChaseMaxValue = new("Max Value", 0f);
    private static readonly FloatOption FarChaseMinValue = new("Min Value", 0f);
    private static readonly FloatOption FarChaseMaxValue = new("Max Value", 0f);
    
    private static readonly ButtonOption ChasePull = new("Pull values", () =>
    {
        ChaseMinValue.Value = Convert.ToSingle(Camera.ChaseMinNum.Value);
        ChaseMaxValue.Value = Convert.ToSingle(Camera.ChaseMaxNum.Value);
    });
    
    private static readonly ButtonOption FarChasePull = new("Pull values", () =>
    {
        FarChaseMinValue.Value = Convert.ToSingle(Camera.FarChaseMinNum.Value);
        FarChaseMaxValue.Value = Convert.ToSingle(Camera.FarChaseMaxNum.Value);
    });
    
    public static readonly List<MenuOption> ChaseLimitersOptions = new()
    {
        new SubHeaderOption("Chase Limiters"),
        ChaseMinValue,
        ChaseMaxValue,
        ChasePull,
        new SubHeaderOption("Far Chase Limiters"),
        FarChaseMinValue,
        FarChaseMaxValue,
        FarChasePull
    };

    #endregion

    #region Driver
    
    private static readonly FloatOption DriverMinValue = new("Min Value", 0f);
    private static readonly FloatOption DriverMaxValue = new("Max Value", 0f);
    
    private static readonly ButtonOption DriverPull = new("Pull values", () =>
    {
        DriverMinValue.Value = Convert.ToSingle(Camera.DriverMinNum.Value);
        DriverMaxValue.Value = Convert.ToSingle(Camera.DriverMaxNum.Value);
    });
    
    public static readonly List<MenuOption> DriverLimitersOptions = new()
    {
        new SubHeaderOption("Driver Limiters"),
        DriverMinValue,
        DriverMaxValue,
        DriverPull
    };
    #endregion

    #region Hood

    private static readonly FloatOption HoodMinValue = new("Min Value", 0f);
    private static readonly FloatOption HoodMaxValue = new("Max Value", 0f);
    
    private static readonly ButtonOption HoodPull = new("Pull values", () =>
    {
        HoodMinValue.Value = Convert.ToSingle(Camera.HoodMinNum.Value);
        HoodMaxValue.Value = Convert.ToSingle(Camera.HoodMaxNum.Value);
    });
        
    public static readonly List<MenuOption> HoodLimitersOptions = new()
    {
        new SubHeaderOption("Hood Limiters"),
        HoodMinValue,
        HoodMaxValue,
        HoodPull
    };

    #endregion

    #region Bumper

    private static readonly FloatOption BumperMinValue = new("Min Value", 0f);
    private static readonly FloatOption BumperMaxValue = new("Max Value", 0f);
    
    private static readonly ButtonOption BumperPull = new("Pull values", () =>
    {
        BumperMinValue.Value = Convert.ToSingle(Camera.BumperMinNum.Value);
        BumperMaxValue.Value = Convert.ToSingle(Camera.BumperMaxNum.Value);
    });

    public static readonly List<MenuOption> BumperLimitersOptions = new()
    {
        new SubHeaderOption("Bumper Limiters"),
        BumperMinValue,
        BumperMaxValue,
        BumperPull
    };

    #endregion

    #region Event Handlers

    private static void ChaseLimiters_OnChanged(object? s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }
        
        switch (floatOption.Name)
        {
            case "Min Value":
            {
                Camera.ChaseMinNum.Value = floatOption.Value;
                break;
            }
            case "Max Value":
            {
                Camera.ChaseMaxNum.Value = floatOption.Value;
                break;
            }
        }
    }
    
    private static void FarChaseLimiters_OnChanged(object? s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        Camera!.Dispatcher.Invoke(() =>
        {
            switch (floatOption.Name)
            {
                case "Min Value":
                {
                    Camera.FarChaseMinNum.Value = floatOption.Value;
                    break;
                }
                case "Max Value":
                {
                    Camera.FarChaseMaxNum.Value = floatOption.Value;
                    break;
                }
            }
        });
    }
    
    private static void DriverLimiters_OnChanged(object? s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        Camera!.Dispatcher.Invoke(() =>
        {
            switch (floatOption.Name)
            {
                case "Min Value":
                {
                    Camera.DriverMinNum.Value = floatOption.Value;
                    break;
                }
                case "Max Value":
                {
                    Camera.DriverMaxNum.Value = floatOption.Value;
                    break;
                }
            }
        });
    }
    
    private static void HoodLimiters_OnChanged(object? s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        Camera.Dispatcher.Invoke(() =>
        {
            switch (floatOption.Name)
            {
                case "Min Value":
                {
                    Camera.HoodMinNum.Value = floatOption.Value;
                    break;
                }
                case "Max Value":
                {
                    Camera.HoodMaxNum.Value = floatOption.Value;
                    break;
                }
            }
        });
    }
    
    private static void BumperLimiters_OnChanged(object? s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        Camera.Dispatcher.Invoke(() =>
        {
            switch (floatOption.Name)
            {
                case "Min Value":
                {
                    Camera.BumperMinNum.Value = floatOption.Value;
                    break;
                }
                case "Max Value":
                {
                    Camera.BumperMaxNum.Value = floatOption.Value;
                    break;
                }
            }
        });
    }

    #endregion

}