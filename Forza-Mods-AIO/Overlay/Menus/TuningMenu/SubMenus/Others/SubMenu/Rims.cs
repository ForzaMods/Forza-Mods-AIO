using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Others;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Others.SubMenu;

public abstract class Rims
{
    private static readonly FloatOption RimSizeFrontValue = new("Wheelbase", 0f);
    private static readonly FloatOption RimRadiusFrontValue = new("Front Width", 0f);
    private static readonly FloatOption RimSizeRearValue = new("Rear Width", 0f);
    private static readonly FloatOption RimRadiusRearValue = new("Front Spacer", 0f);
    
    private static readonly ButtonOption RimsPull = new("Pull values",  () =>
    {
        RimSizeFrontValue.Value = (float)O.RimSizeFrontBox.Value!;
        RimRadiusFrontValue.Value = (float)O.RimRadiusFrontBox.Value!;
        RimSizeRearValue.Value = (float)O.RimSizeRearBox.Value!;
        RimRadiusRearValue.Value = (float)O.RimRadiusRearBox.Value!;
    });
    
    public static readonly List<MenuOption> RimsOptions = new()
    {
        RimSizeFrontValue,
        RimRadiusFrontValue,
        RimSizeRearValue,
        RimRadiusRearValue,
        RimsPull
    };
    
    public static void InitiateSubMenu()
    {
        RimSizeFrontValue.ValueChangedEventHandler += RimSizeFrontValueChanged;
        RimRadiusFrontValue.ValueChangedEventHandler += RimRadiusFrontValueChanged;
        RimSizeRearValue.ValueChangedEventHandler += RimSizeRearValueChanged;
        RimRadiusRearValue.ValueChangedEventHandler += RimRadiusRearValueChanged;
    }

    private static void RimSizeFrontValueChanged(object s, EventArgs e)
    {
        var others = O;

        others.Dispatcher.Invoke(() =>
        {
            others.RimSizeFrontBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RimRadiusFrontValueChanged(object s, EventArgs e)
    {
        var others = O;

        others.Dispatcher.Invoke(() =>
        {
            others.RimRadiusFrontBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RimSizeRearValueChanged(object s, EventArgs e)
    {
        var others = O;

        others.Dispatcher.Invoke(() =>
        {
            others.RimSizeRearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RimRadiusRearValueChanged(object s, EventArgs e)
    {
        var others = O;

        others.Dispatcher.Invoke(() =>
        {
            others.RimRadiusRearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}