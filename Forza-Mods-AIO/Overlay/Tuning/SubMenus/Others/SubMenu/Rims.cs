using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Others.SubMenu;

public abstract class Rims
{
    private static readonly Overlay.MenuOption RimSizeFrontValue = new ("Wheelbase", "Float", 0f);
    private static readonly Overlay.MenuOption RimRadiusFrontValue = new ("Front Width", "Float", 0f);
    private static readonly Overlay.MenuOption RimSizeRearValue = new ("Rear Width", "Float", 0f);
    private static readonly Overlay.MenuOption RimRadiusRearValue = new ("Front Spacer", "Float", 0f);
    
    private static readonly Overlay.MenuOption RimsPull = new ("Pull values", "Button", new Action(() =>
    {
        var Others = Tabs.Tuning.DropDownTabs.Others.o;
        
        Others.Dispatcher.Invoke(() =>
        {
            RimSizeFrontValue.Value = (float)Others.RimSizeFrontBox.Value!;
            RimRadiusFrontValue.Value = (float)Others.RimRadiusFrontBox.Value!;
            RimSizeRearValue.Value = (float)Others.RimSizeRearBox.Value!;
            RimRadiusRearValue.Value = (float)Others.RimRadiusRearBox.Value!;
        });
    }));
    
    public static readonly List<Overlay.MenuOption> RimsOptions = new()
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
        var Others = Tabs.Tuning.DropDownTabs.Others.o;

        Others.Dispatcher.Invoke(() =>
        {
            Others.RimSizeFrontBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RimRadiusFrontValueChanged(object s, EventArgs e)
    {
        var Others = Tabs.Tuning.DropDownTabs.Others.o;

        Others.Dispatcher.Invoke(() =>
        {
            Others.RimRadiusFrontBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RimSizeRearValueChanged(object s, EventArgs e)
    {
        var Others = Tabs.Tuning.DropDownTabs.Others.o;

        Others.Dispatcher.Invoke(() =>
        {
            Others.RimSizeRearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RimRadiusRearValueChanged(object s, EventArgs e)
    {
        var Others = Tabs.Tuning.DropDownTabs.Others.o;

        Others.Dispatcher.Invoke(() =>
        {
            Others.RimRadiusRearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}