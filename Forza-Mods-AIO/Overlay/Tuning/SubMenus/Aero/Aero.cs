using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Aero;

public abstract class Aero
{
    private static readonly Overlay.MenuOption FrontAeroMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption FrontAeroMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption RearAeroMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption RearAeroMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption FrontAeroPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, new Action(() =>
    {
        var Aero = Tabs.Tuning.DropDownTabs.Aero.ae;
        
        Aero.Dispatcher.Invoke(() =>
        {
            if (Aero.FrontAeroMinBox.Value == null || Aero.FrontAeroMaxBox.Value == null)
                return;

            FrontAeroMinValue.Value = (float)Aero.FrontAeroMinBox.Value;
            FrontAeroMaxValue.Value = (float)Aero.FrontAeroMaxBox.Value;
        });
    }));
    
    private static readonly Overlay.MenuOption RearAeroPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, new Action(() =>
    {
        var Aero = Tabs.Tuning.DropDownTabs.Aero.ae;
        
        Aero.Dispatcher.Invoke(() =>
        {
            if (Aero.RearAeroMinBox.Value == null || Aero.RearAeroMaxBox.Value == null)
                return;

            RearAeroMinValue.Value = (float)Aero.RearAeroMinBox.Value;
            RearAeroMaxValue.Value = (float)Aero.RearAeroMaxBox.Value;
        });
    }));

    private static void FrontAeroMinValueChanged(object s, EventArgs e)
    {
        var Aero = Tabs.Tuning.DropDownTabs.Aero.ae;

        Aero.Dispatcher.Invoke(() =>
        {
            Aero.FrontAeroMinBox.Value = (float)FrontAeroMinValue.Value;
        });
    }
    
    private static void FrontAeroMaxValueChanged(object s, EventArgs e)
    {
        var Aero = Tabs.Tuning.DropDownTabs.Aero.ae;

        Aero.Dispatcher.Invoke(() =>
        {
            Aero.FrontAeroMaxBox.Value = (float)FrontAeroMaxValue.Value;
        });
    }
    
    private static void RearAeroMinValueChanged(object s, EventArgs e)
    {
        var Aero = Tabs.Tuning.DropDownTabs.Aero.ae;

        Aero.Dispatcher.Invoke(() =>
        {
            Aero.RearAeroMinBox.Value = (float)RearAeroMinValue.Value;
        });
    }
    
    private static void RearAeroMaxValueChanged(object s, EventArgs e)
    {
        var Aero = Tabs.Tuning.DropDownTabs.Aero.ae;

        Aero.Dispatcher.Invoke(() =>
        {
            Aero.RearAeroMaxBox.Value = (float)RearAeroMaxValue.Value;
        });
    }
    
    public static readonly List<Overlay.MenuOption> AeroOptions = new()
    {
        new("Front Aero", Overlay.MenuOption.OptionType.SubHeader),
        FrontAeroMinValue,
        FrontAeroMaxValue,
        FrontAeroPull,
        new("Rear Aero", Overlay.MenuOption.OptionType.SubHeader),
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