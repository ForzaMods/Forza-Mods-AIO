using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Gearing;

public abstract class Gearing
{
    private static readonly MenuOption FinalDriveValue = new ("Final Drive", 0f);
    private static readonly MenuOption ReverseGearValue = new ("Reverse Gear", 0f);
    private static readonly MenuOption FirstGearValue = new ("First Gear", 0f);
    private static readonly MenuOption SecondGearValue = new ("Second Gear", 0f);
    private static readonly MenuOption ThirdGearValue = new ("Third Gear", 0f);
    private static readonly MenuOption FourthGearValue = new ("Fourth Gear", 0f);
    private static readonly MenuOption FifthGearValue = new ("Fifth Gear", 0f);
    private static readonly MenuOption SixthGearValue = new ("Sixth Gear", 0f);
    private static readonly MenuOption SeventhGearValue = new ("Seventh Gear", 0f);
    private static readonly MenuOption EighthGearValue = new ("Eighth Gear", 0f);
    private static readonly MenuOption NinthGearValue = new ("Ninth Gear", 0f);
    private static readonly MenuOption TenthGearValue = new ("Tenth Gear", 0f);

    private static readonly MenuOption GearingPull = new ("Pull values",  () =>
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;
        
        gearing.Dispatcher.Invoke(() =>
        {
            FinalDriveValue.Value = gearing.FinalDriveBox.IsEnabled ? (float)gearing.FinalDriveBox.Value! : 0f;    
            ReverseGearValue.Value = gearing.ReverseGearBox.IsEnabled ? (float)gearing.ReverseGearBox.Value! : 0f;    
            FirstGearValue.Value = gearing.FirstGearBox.IsEnabled ? (float)gearing.FirstGearBox.Value! : 0f;    
            SecondGearValue.Value = gearing.SecondGearBox.IsEnabled ? (float)gearing.SecondGearBox.Value! : 0f;    
            ThirdGearValue.Value = gearing.ThirdGearBox.IsEnabled ? (float)gearing.ThirdGearBox.Value! : 0f;    
            FourthGearValue.Value = gearing.FourthGearBox.IsEnabled ? (float)gearing.FourthGearBox.Value! : 0f;    
            FifthGearValue.Value = gearing.FifthGearBox.IsEnabled ? (float)gearing.FifthGearBox.Value! : 0f;    
            SixthGearValue.Value = gearing.SixthGearBox.IsEnabled ? (float)gearing.SixthGearBox.Value! : 0f;    
            SeventhGearValue.Value = gearing.SeventhGearBox.IsEnabled ? (float)gearing.SeventhGearBox.Value! : 0f;    
            EighthGearValue.Value = gearing.EighthGearBox.IsEnabled ? (float)gearing.EighthGearBox.Value! : 0f;    
            NinthGearValue.Value = gearing.NinthGearBox.IsEnabled ? (float)gearing.NinthGearBox.Value! : 0f;    
            TenthGearValue.Value = gearing.TenthGearBox.IsEnabled ? (float)gearing.TenthGearBox.Value! : 0f;    
        });
    });

    private static void FinalDriveValueChanged(object s, EventArgs e)
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.FinalDriveBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void ReverseGearValueChanged(object s, EventArgs e)
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.FinalDriveBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void FirstGearValueChanged(object s, EventArgs e)
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.FinalDriveBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void SecondGearValueChanged(object s, EventArgs e)
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.SecondGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void ThirdGearValueChanged(object s, EventArgs e)
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.ThirdGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void FourthGearValueChanged(object s, EventArgs e)
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.FourthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void FifthGearValueChanged(object s, EventArgs e)
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.FifthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void SixthGearValueChanged(object s, EventArgs e)
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;
        
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.SixthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void SeventhGearValueChanged(object s, EventArgs e)
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;
        
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.SeventhGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void EighthGearValueChanged(object s, EventArgs e)
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;

        gearing.Dispatcher.Invoke(() =>
        {
            gearing.EighthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void NinthGearValueChanged(object s, EventArgs e)
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;
        
        gearing.Dispatcher.Invoke(() =>
        {
            gearing.NinthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void TenthGearValueChanged(object s, EventArgs e)
    {
        var gearing = Tabs.Tuning.DropDownTabs.Gearing.G;
        
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
        FinalDriveValue.ValueChangedHandler += FinalDriveValueChanged;
        ReverseGearValue.ValueChangedHandler += ReverseGearValueChanged;
        FirstGearValue.ValueChangedHandler += FirstGearValueChanged;
        SecondGearValue.ValueChangedHandler += SecondGearValueChanged;
        ThirdGearValue.ValueChangedHandler += ThirdGearValueChanged;
        FourthGearValue.ValueChangedHandler += FourthGearValueChanged;
        FifthGearValue.ValueChangedHandler += FifthGearValueChanged;
        SixthGearValue.ValueChangedHandler += SixthGearValueChanged;
        SeventhGearValue.ValueChangedHandler += SeventhGearValueChanged;
        EighthGearValue.ValueChangedHandler += EighthGearValueChanged;
        NinthGearValue.ValueChangedHandler += NinthGearValueChanged;
        TenthGearValue.ValueChangedHandler += TenthGearValueChanged;
    }
}