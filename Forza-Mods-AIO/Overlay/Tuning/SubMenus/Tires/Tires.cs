using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Tires;

public abstract class Tires
{
    private static readonly MenuOption TireFrontLeftValue = new ("Left", OptionType.Float, 0f);
    private static readonly MenuOption TireFrontRightValue = new ("Right", OptionType.Float, 0f);
    private static readonly MenuOption TireRearLeftValue = new ("Left", OptionType.Float, 0f);
    private static readonly MenuOption TireRearRightValue = new ("Right", OptionType.Float, 0f);

    private static readonly MenuOption RearTiresPull = new ("Pull values", OptionType.Button, () =>
    {
        var tires = Tabs.Tuning.DropDownTabs.Tires.T;
        
        tires.Dispatcher.Invoke(() =>
        {  
            TireRearLeftValue.Value = (float)tires.TireRearLeftBox.Value!;    
            TireRearRightValue.Value = (float)tires.TireRearRightBox.Value!;    
        });
    });
    
    private static readonly MenuOption FrontTiresPull = new ("Pull values", OptionType.Button, () =>
    {
        var tires = Tabs.Tuning.DropDownTabs.Tires.T;
        
        tires.Dispatcher.Invoke(() =>
        {
            TireFrontLeftValue.Value = (float)tires.TireFrontLeftBox.Value!;    
            TireFrontRightValue.Value = (float)tires.TireFrontRightBox.Value!;    
        });
    });

    private static void TireFrontLeftValueChanged(object s, EventArgs e)
    {
        var tires = Tabs.Tuning.DropDownTabs.Tires.T;
        tires.Dispatcher.Invoke(() =>
        {
            tires.TireFrontLeftBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void TireFrontRightValueChanged(object s, EventArgs e)
    {
        var tires = Tabs.Tuning.DropDownTabs.Tires.T;
        tires.Dispatcher.Invoke(() =>
        {
            tires.TireFrontRightBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void TireRearLeftValueChanged(object s, EventArgs e)
    {
        var tires = Tabs.Tuning.DropDownTabs.Tires.T;
        tires.Dispatcher.Invoke(() =>
        {
            tires.TireRearLeftBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void TireRearRightValueChanged(object s, EventArgs e)
    {
        var tires = Tabs.Tuning.DropDownTabs.Tires.T;
        tires.Dispatcher.Invoke(() =>
        {
            tires.TireRearRightBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    public static readonly List<MenuOption> TiresOptions = new()
    {
        new MenuOption("Front Tires", OptionType.SubHeader),
        TireFrontLeftValue,
        TireFrontRightValue,
        FrontTiresPull,
        new MenuOption("Rear Tires", OptionType.SubHeader),
        TireRearLeftValue,
        TireRearRightValue,
        RearTiresPull
    };
    
    public static void InitiateSubMenu() 
    {
        TireFrontLeftValue.ValueChangedHandler += TireFrontLeftValueChanged;
        TireFrontRightValue.ValueChangedHandler += TireFrontRightValueChanged;
        TireRearLeftValue.ValueChangedHandler += TireRearLeftValueChanged;
        TireRearRightValue.ValueChangedHandler += TireRearRightValueChanged;
    }
}