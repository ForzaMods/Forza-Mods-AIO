using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Others;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Others.SubMenu;

public abstract class Rims
{
    private static readonly FloatOption RimSizeFrontValue = new("Front", 0f);
    private static readonly FloatOption RimSizeRearValue = new("Rear", 0f);
    private static readonly FloatOption RimRadiusFrontValue = new("Front", 0f);
    private static readonly FloatOption RimRadiusRearValue = new("Front", 0f);
    
    private static readonly ButtonOption RimsPull = new("Pull values",  () =>
    {
        RimSizeFrontValue.Value = (float)O.RimSizeFrontBox.Value!;
        RimRadiusFrontValue.Value = (float)O.RimRadiusFrontBox.Value!;
        RimSizeRearValue.Value = (float)O.RimSizeRearBox.Value!;
        RimRadiusRearValue.Value = (float)O.RimRadiusRearBox.Value!;
    });
    
    public static readonly List<MenuOption> RimsOptions = new()
    {
        new SubHeaderOption("Rim size"),
        RimSizeFrontValue,
        RimSizeRearValue,
        new SubHeaderOption("Rim radius"),
        RimRadiusFrontValue,
        RimRadiusRearValue,
        RimsPull
    };
    
    public static void InitiateSubMenu()
    {
        RimSizeFrontValue.ValueChanged += RimSizeFrontValueChanged;
        RimRadiusFrontValue.ValueChanged += RimRadiusFrontValueChanged;
        RimSizeRearValue.ValueChanged += RimSizeRearValueChanged;
        RimRadiusRearValue.ValueChanged += RimRadiusRearValueChanged;
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