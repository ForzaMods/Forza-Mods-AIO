using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Tires;

public abstract class Tires
{
    private static readonly Overlay.MenuOption TireFrontLeftValue = new ("Left", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption TireFrontRightValue = new ("Right", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption TireRearLeftValue = new ("Left", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption TireRearRightValue = new ("Right", Overlay.MenuOption.OptionType.Float, 0f);

    private static readonly Overlay.MenuOption RearTiresPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, (() =>
    {
        var Tires = Tabs.Tuning.DropDownTabs.Tires.t;
        
        Tires.Dispatcher.Invoke(() =>
        {  
            TireRearLeftValue.Value = (float)Tires.TireRearLeftBox.Value!;    
            TireRearRightValue.Value = (float)Tires.TireRearRightBox.Value!;    
        });
    }));
    
    private static readonly Overlay.MenuOption FrontTiresPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, (() =>
    {
        var Tires = Tabs.Tuning.DropDownTabs.Tires.t;
        
        Tires.Dispatcher.Invoke(() =>
        {
            TireFrontLeftValue.Value = (float)Tires.TireFrontLeftBox.Value!;    
            TireFrontRightValue.Value = (float)Tires.TireFrontRightBox.Value!;    
        });
    }));

    private static void TireFrontLeftValueChanged(object s, EventArgs e)
    {
        var Tires = Tabs.Tuning.DropDownTabs.Tires.t;
        Tires.Dispatcher.Invoke(() =>
        {
            Tires.TireFrontLeftBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void TireFrontRightValueChanged(object s, EventArgs e)
    {
        var Tires = Tabs.Tuning.DropDownTabs.Tires.t;
        Tires.Dispatcher.Invoke(() =>
        {
            Tires.TireFrontRightBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void TireRearLeftValueChanged(object s, EventArgs e)
    {
        var Tires = Tabs.Tuning.DropDownTabs.Tires.t;
        Tires.Dispatcher.Invoke(() =>
        {
            Tires.TireRearLeftBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void TireRearRightValueChanged(object s, EventArgs e)
    {
        var Tires = Tabs.Tuning.DropDownTabs.Tires.t;
        Tires.Dispatcher.Invoke(() =>
        {
            Tires.TireRearRightBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    public static readonly List<Overlay.MenuOption> TiresOptions = new()
    {
        new Overlay.MenuOption("Front Tires", Overlay.MenuOption.OptionType.SubHeader),
        TireFrontLeftValue,
        TireFrontRightValue,
        FrontTiresPull,
        new Overlay.MenuOption("Rear Tires", Overlay.MenuOption.OptionType.SubHeader),
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