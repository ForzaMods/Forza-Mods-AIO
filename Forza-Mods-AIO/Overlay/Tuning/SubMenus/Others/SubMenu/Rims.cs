using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Others.SubMenu;

public abstract class Rims
{
    private static readonly MenuOption RimSizeFrontValue = new ("Wheelbase", 0f);
    private static readonly MenuOption RimRadiusFrontValue = new ("Front Width", 0f);
    private static readonly MenuOption RimSizeRearValue = new ("Rear Width", 0f);
    private static readonly MenuOption RimRadiusRearValue = new ("Front Spacer", 0f);
    
    private static readonly MenuOption RimsPull = new ("Pull values",  () =>
    {
        var others = Tabs.Tuning.DropDownTabs.Others.O;
        
        others.Dispatcher.Invoke(() =>
        {
            RimSizeFrontValue.Value = (float)others.RimSizeFrontBox.Value!;
            RimRadiusFrontValue.Value = (float)others.RimRadiusFrontBox.Value!;
            RimSizeRearValue.Value = (float)others.RimSizeRearBox.Value!;
            RimRadiusRearValue.Value = (float)others.RimRadiusRearBox.Value!;
        });
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
        RimSizeFrontValue.ValueChangedHandler += RimSizeFrontValueChanged;
        RimRadiusFrontValue.ValueChangedHandler += RimRadiusFrontValueChanged;
        RimSizeRearValue.ValueChangedHandler += RimSizeRearValueChanged;
        RimRadiusRearValue.ValueChangedHandler += RimRadiusRearValueChanged;
    }

    private static void RimSizeFrontValueChanged(object s, EventArgs e)
    {
        var others = Tabs.Tuning.DropDownTabs.Others.O;

        others.Dispatcher.Invoke(() =>
        {
            others.RimSizeFrontBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RimRadiusFrontValueChanged(object s, EventArgs e)
    {
        var others = Tabs.Tuning.DropDownTabs.Others.O;

        others.Dispatcher.Invoke(() =>
        {
            others.RimRadiusFrontBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RimSizeRearValueChanged(object s, EventArgs e)
    {
        var others = Tabs.Tuning.DropDownTabs.Others.O;

        others.Dispatcher.Invoke(() =>
        {
            others.RimSizeRearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RimRadiusRearValueChanged(object s, EventArgs e)
    {
        var others = Tabs.Tuning.DropDownTabs.Others.O;

        others.Dispatcher.Invoke(() =>
        {
            others.RimRadiusRearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}