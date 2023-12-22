using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static System.Convert;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Tires;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Tires;

public abstract class Tires
{
    private static readonly FloatOption TireFrontLeftValue = new ("Left", 0f);
    private static readonly FloatOption TireFrontRightValue = new ("Right", 0f);
    private static readonly FloatOption TireRearLeftValue = new ("Left", 0f);
    private static readonly FloatOption TireRearRightValue = new ("Right", 0f);

    private static readonly ButtonOption RearTiresPull = new("Pull values", () =>
    {
        TireRearLeftValue.Value = ToSingle(T.TireRearLeftBox.Value);    
        TireRearRightValue.Value = ToSingle(T.TireRearRightBox.Value);    
    });
    
    private static readonly ButtonOption FrontTiresPull = new("Pull values", () =>
    {
        TireFrontLeftValue.Value = ToSingle(T.TireFrontLeftBox.Value);
        TireFrontRightValue.Value = ToSingle(T.TireFrontRightBox.Value);
    });

    private static void TireFrontLeftValueChanged(object s, EventArgs e)
    {
        var tires = T;
        tires.Dispatcher.Invoke(() =>
        {
            tires.TireFrontLeftBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void TireFrontRightValueChanged(object s, EventArgs e)
    {
        var tires = T;
        tires.Dispatcher.Invoke(() =>
        {
            tires.TireFrontRightBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void TireRearLeftValueChanged(object s, EventArgs e)
    {
        var tires = T;
        tires.Dispatcher.Invoke(() =>
        {
            tires.TireRearLeftBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void TireRearRightValueChanged(object s, EventArgs e)
    {
        var tires = T;
        tires.Dispatcher.Invoke(() =>
        {
            tires.TireRearRightBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    public static readonly List<MenuOption> TiresOptions = new()
    {
        new SubHeaderOption("Front Tires"),
        TireFrontLeftValue,
        TireFrontRightValue,
        FrontTiresPull,
        new SubHeaderOption("Rear Tires"),
        TireRearLeftValue,
        TireRearRightValue,
        RearTiresPull
    };
    
    public static void InitiateSubMenu() 
    {
        TireFrontLeftValue.ValueChangedEventHandler += TireFrontLeftValueChanged;
        TireFrontRightValue.ValueChangedEventHandler += TireFrontRightValueChanged;
        TireRearLeftValue.ValueChangedEventHandler += TireRearLeftValueChanged;
        TireRearRightValue.ValueChangedEventHandler += TireRearRightValueChanged;
    }
}