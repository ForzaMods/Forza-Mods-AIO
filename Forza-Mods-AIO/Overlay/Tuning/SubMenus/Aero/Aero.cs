using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Aero;

public abstract class Aero
{
    private static readonly MenuOption FrontAeroMinValue = new ("Min Value", OptionType.Float, 0f);
    private static readonly MenuOption FrontAeroMaxValue = new ("Max Value", OptionType.Float, 0f);
    
    private static readonly MenuOption RearAeroMinValue = new ("Min Value", OptionType.Float, 0f);
    private static readonly MenuOption RearAeroMaxValue = new ("Max Value", OptionType.Float, 0f);
    
    private static readonly MenuOption FrontAeroPull = new ("Pull values", OptionType.Button, () =>
    {
        var aero = Tabs.Tuning.DropDownTabs.Aero.Ae;
        
        aero.Dispatcher.Invoke(() =>
        {
            if (aero.FrontAeroMinBox.Value == null || aero.FrontAeroMaxBox.Value == null)
                return;

            FrontAeroMinValue.Value = (float)aero.FrontAeroMinBox.Value;
            FrontAeroMaxValue.Value = (float)aero.FrontAeroMaxBox.Value;
        });
    });
    
    private static readonly MenuOption RearAeroPull = new ("Pull values", OptionType.Button, () =>
    {
        var aero = Tabs.Tuning.DropDownTabs.Aero.Ae;
        
        aero.Dispatcher.Invoke(() =>
        {
            if (aero.RearAeroMinBox.Value == null || aero.RearAeroMaxBox.Value == null)
                return;

            RearAeroMinValue.Value = (float)aero.RearAeroMinBox.Value;
            RearAeroMaxValue.Value = (float)aero.RearAeroMaxBox.Value;
        });
    });

    private static void FrontAeroMinValueChanged(object s, EventArgs e)
    {
        var aero = Tabs.Tuning.DropDownTabs.Aero.Ae;

        aero.Dispatcher.Invoke(() =>
        {
            aero.FrontAeroMinBox.Value = (float)FrontAeroMinValue.Value;
        });
    }
    
    private static void FrontAeroMaxValueChanged(object s, EventArgs e)
    {
        var aero = Tabs.Tuning.DropDownTabs.Aero.Ae;

        aero.Dispatcher.Invoke(() =>
        {
            aero.FrontAeroMaxBox.Value = (float)FrontAeroMaxValue.Value;
        });
    }
    
    private static void RearAeroMinValueChanged(object s, EventArgs e)
    {
        var aero = Tabs.Tuning.DropDownTabs.Aero.Ae;

        aero.Dispatcher.Invoke(() =>
        {
            aero.RearAeroMinBox.Value = (float)RearAeroMinValue.Value;
        });
    }
    
    private static void RearAeroMaxValueChanged(object s, EventArgs e)
    {
        var aero = Tabs.Tuning.DropDownTabs.Aero.Ae;

        aero.Dispatcher.Invoke(() =>
        {
            aero.RearAeroMaxBox.Value = (float)RearAeroMaxValue.Value;
        });
    }
    
    public static readonly List<MenuOption> AeroOptions = new()
    {
        new("Front Aero", OptionType.SubHeader),
        FrontAeroMinValue,
        FrontAeroMaxValue,
        FrontAeroPull,
        new("Rear Aero", OptionType.SubHeader),
        RearAeroMinValue,
        RearAeroMaxValue,
        RearAeroPull
    };

    public static void InitiateSubMenu()
    {
        FrontAeroMinValue.ValueChangedHandler += FrontAeroMinValueChanged;
        FrontAeroMaxValue.ValueChangedHandler += FrontAeroMaxValueChanged;
        RearAeroMinValue.ValueChangedHandler += RearAeroMinValueChanged;
        RearAeroMaxValue.ValueChangedHandler += RearAeroMaxValueChanged;
    }
}