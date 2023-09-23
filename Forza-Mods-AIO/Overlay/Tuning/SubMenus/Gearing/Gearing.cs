using System;
using System.Collections.Generic;
using System.Windows.Threading;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Gearing;

public abstract class Gearing
{
    private static readonly Overlay.MenuOption FinalDriveValue = new ("Final Drive", "Float", 0f);
    private static readonly Overlay.MenuOption ReverseGearValue = new ("Reverse Gear", "Float", 0f);
    private static readonly Overlay.MenuOption FirstGearValue = new ("First Gear", "Float", 0f);
    private static readonly Overlay.MenuOption SecondGearValue = new ("Second Gear", "Float", 0f);
    private static readonly Overlay.MenuOption ThirdGearValue = new ("Third Gear", "Float", 0f);
    private static readonly Overlay.MenuOption FourthGearValue = new ("Fourth Gear", "Float", 0f);
    private static readonly Overlay.MenuOption FifthGearValue = new ("Fifth Gear", "Float", 0f);
    private static readonly Overlay.MenuOption SixthGearValue = new ("Sixth Gear", "Float", 0f);
    private static readonly Overlay.MenuOption SeventhGearValue = new ("Seventh Gear", "Float", 0f);
    private static readonly Overlay.MenuOption EighthGearValue = new ("Eighth Gear", "Float", 0f);
    private static readonly Overlay.MenuOption NinthGearValue = new ("Ninth Gear", "Float", 0f);
    private static readonly Overlay.MenuOption TenthGearValue = new ("Tenth Gear", "Float", 0f);

    private static readonly Overlay.MenuOption GearingPull = new ("Pull values", "Button", new Action(() =>
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;
        
        Gearing.Dispatcher.Invoke(() =>
        {
            FinalDriveValue.Value = Gearing.FinalDriveBox.IsEnabled ? (float)Gearing.FinalDriveBox.Value! : 0f;    
            ReverseGearValue.Value = Gearing.ReverseGearBox.IsEnabled ? (float)Gearing.ReverseGearBox.Value! : 0f;    
            FirstGearValue.Value = Gearing.FirstGearBox.IsEnabled ? (float)Gearing.FirstGearBox.Value! : 0f;    
            SecondGearValue.Value = Gearing.SecondGearBox.IsEnabled ? (float)Gearing.SecondGearBox.Value! : 0f;    
            ThirdGearValue.Value = Gearing.ThirdGearBox.IsEnabled ? (float)Gearing.ThirdGearBox.Value! : 0f;    
            FourthGearValue.Value = Gearing.FourthGearBox.IsEnabled ? (float)Gearing.FourthGearBox.Value! : 0f;    
            FifthGearValue.Value = Gearing.FifthGearBox.IsEnabled ? (float)Gearing.FifthGearBox.Value! : 0f;    
            SixthGearValue.Value = Gearing.SixthGearBox.IsEnabled ? (float)Gearing.SixthGearBox.Value! : 0f;    
            SeventhGearValue.Value = Gearing.SeventhGearBox.IsEnabled ? (float)Gearing.SeventhGearBox.Value! : 0f;    
            EighthGearValue.Value = Gearing.EighthGearBox.IsEnabled ? (float)Gearing.EighthGearBox.Value! : 0f;    
            NinthGearValue.Value = Gearing.NinthGearBox.IsEnabled ? (float)Gearing.NinthGearBox.Value! : 0f;    
            TenthGearValue.Value = Gearing.TenthGearBox.IsEnabled ? (float)Gearing.TenthGearBox.Value! : 0f;    
        });
    }));

    private static void FinalDriveValueChanged(object s, EventArgs e)
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;
        Gearing.Dispatcher.Invoke(() =>
        {
            Gearing.FinalDriveBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void ReverseGearValueChanged(object s, EventArgs e)
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;
        Gearing.Dispatcher.Invoke(() =>
        {
            Gearing.FinalDriveBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void FirstGearValueChanged(object s, EventArgs e)
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;
        Gearing.Dispatcher.Invoke(() =>
        {
            Gearing.FinalDriveBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void SecondGearValueChanged(object s, EventArgs e)
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;
        Gearing.Dispatcher.Invoke(() =>
        {
            Gearing.SecondGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void ThirdGearValueChanged(object s, EventArgs e)
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;
        Gearing.Dispatcher.Invoke(() =>
        {
            Gearing.ThirdGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void FourthGearValueChanged(object s, EventArgs e)
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;
        Gearing.Dispatcher.Invoke(() =>
        {
            Gearing.FourthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void FifthGearValueChanged(object s, EventArgs e)
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;
        Gearing.Dispatcher.Invoke(() =>
        {
            Gearing.FifthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void SixthGearValueChanged(object s, EventArgs e)
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;
        
        Gearing.Dispatcher.Invoke(() =>
        {
            Gearing.SixthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void SeventhGearValueChanged(object s, EventArgs e)
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;
        
        Gearing.Dispatcher.Invoke(() =>
        {
            Gearing.SeventhGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void EighthGearValueChanged(object s, EventArgs e)
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;

        Gearing.Dispatcher.Invoke(() =>
        {
            Gearing.EighthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void NinthGearValueChanged(object s, EventArgs e)
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;
        
        Gearing.Dispatcher.Invoke(() =>
        {
            Gearing.NinthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void TenthGearValueChanged(object s, EventArgs e)
    {
        var Gearing = Tabs.Tuning.DropDownTabs.Gearing.g;
        
        Gearing.Dispatcher.Invoke(() =>
        {
            Gearing.TenthGearBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    public static readonly List<Overlay.MenuOption> GearingOptions = new()
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