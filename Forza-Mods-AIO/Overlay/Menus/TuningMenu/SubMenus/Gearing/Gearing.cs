using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Gearing;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Gearing;

public abstract class Gearing
{
    private static readonly FloatOption FinalDriveValue = new("Final Drive", 0f);
    private static readonly FloatOption ReverseGearValue = new("Reverse Gear", 0f);
    private static readonly FloatOption FirstGearValue = new("First Gear", 0f);
    private static readonly FloatOption SecondGearValue = new("Second Gear", 0f);
    private static readonly FloatOption ThirdGearValue = new("Third Gear", 0f);
    private static readonly FloatOption FourthGearValue = new("Fourth Gear", 0f);
    private static readonly FloatOption FifthGearValue = new("Fifth Gear", 0f);
    private static readonly FloatOption SixthGearValue = new("Sixth Gear", 0f);
    private static readonly FloatOption SeventhGearValue = new("Seventh Gear", 0f);
    private static readonly FloatOption EighthGearValue = new("Eighth Gear", 0f);
    private static readonly FloatOption NinthGearValue = new("Ninth Gear", 0f);
    private static readonly FloatOption TenthGearValue = new("Tenth Gear", 0f);

    private static readonly ButtonOption GearingPull = new("Pull values", () =>
    {
        FinalDriveValue.Value = Convert.ToSingle(G.FinalDriveBox.Value);
        ReverseGearValue.Value = Convert.ToSingle(G.ReverseGearBox.Value);
        FirstGearValue.Value = Convert.ToSingle(G.FirstGearBox.Value);
        SecondGearValue.Value = Convert.ToSingle(G.SecondGearBox.Value);
        ThirdGearValue.Value = Convert.ToSingle(G.ThirdGearBox.Value);
        FourthGearValue.Value = Convert.ToSingle(G.FourthGearBox.Value);
        FifthGearValue.Value = Convert.ToSingle(G.FifthGearBox.Value);
        SixthGearValue.Value = Convert.ToSingle(G.SixthGearBox.Value);
        SeventhGearValue.Value = Convert.ToSingle(G.SeventhGearBox.Value);
        EighthGearValue.Value = Convert.ToSingle(G.EighthGearBox.Value);
        NinthGearValue.Value = Convert.ToSingle(G.NinthGearBox.Value);
        TenthGearValue.Value = Convert.ToSingle(G.TenthGearBox.Value);
    });

    private static void FinalDriveValueChanged(object s, EventArgs e)
    {
        var gearing = G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.FinalDriveBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void ReverseGearValueChanged(object s, EventArgs e)
    {
        var gearing = G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.FinalDriveBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void FirstGearValueChanged(object s, EventArgs e)
    {
        var gearing = G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.FinalDriveBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void SecondGearValueChanged(object s, EventArgs e)
    {
        var gearing = G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.SecondGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void ThirdGearValueChanged(object s, EventArgs e)
    {
        var gearing = G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.ThirdGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void FourthGearValueChanged(object s, EventArgs e)
    {
        var gearing = G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.FourthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void FifthGearValueChanged(object s, EventArgs e)
    {
        var gearing = G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.FifthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void SixthGearValueChanged(object s, EventArgs e)
    {
        var gearing = G;
        
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.SixthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void SeventhGearValueChanged(object s, EventArgs e)
    {
        var gearing = G;
        
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.SeventhGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void EighthGearValueChanged(object s, EventArgs e)
    {
        var gearing = G;

        gearing.Dispatcher.Invoke(() =>
        {
            gearing.EighthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void NinthGearValueChanged(object s, EventArgs e)
    {
        var gearing = G;
        
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.NinthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void TenthGearValueChanged(object s, EventArgs e)
    {
        var gearing = G;
        
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.TenthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    public static readonly List<MenuOption> GearingOptions = new()
    {
        FinalDriveValue,
        ReverseGearValue,
        FirstGearValue,
        SecondGearValue,
        ThirdGearValue,
        FourthGearValue,
        FifthGearValue,
        SixthGearValue,
        SeventhGearValue,
        EighthGearValue,
        NinthGearValue,
        TenthGearValue,
        GearingPull
    };
    
    public static void InitiateSubMenu()
    {
        FinalDriveValue.ValueChanged += FinalDriveValueChanged;
        ReverseGearValue.ValueChanged += ReverseGearValueChanged;
        FirstGearValue.ValueChanged += FirstGearValueChanged;
        SecondGearValue.ValueChanged += SecondGearValueChanged;
        ThirdGearValue.ValueChanged += ThirdGearValueChanged;
        FourthGearValue.ValueChanged += FourthGearValueChanged;
        FifthGearValue.ValueChanged += FifthGearValueChanged;
        SixthGearValue.ValueChanged += SixthGearValueChanged;
        SeventhGearValue.ValueChanged += SeventhGearValueChanged;
        EighthGearValue.ValueChanged += EighthGearValueChanged;
        NinthGearValue.ValueChanged += NinthGearValueChanged;
        TenthGearValue.ValueChanged += TenthGearValueChanged;
    }
}