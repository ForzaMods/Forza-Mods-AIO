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

    private static readonly ButtonOption GearingPull = new("Pull values", PullValues);

    public static void PullValues()
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
    }

    private static void FinalDriveValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }
        
        G.FinalDriveBox.Value = floatOption.Value;
    }
    
    private static void ReverseGearValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        G.FinalDriveBox.Value = floatOption.Value;
    }

    private static void FirstGearValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        G.FinalDriveBox.Value = floatOption.Value;
    }

    private static void SecondGearValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        G.SecondGearBox.Value = floatOption.Value;
    }

    private static void ThirdGearValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        G.ThirdGearBox.Value = floatOption.Value;
    }

    private static void FourthGearValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        G.FourthGearBox.Value = floatOption.Value;
    }

    private static void FifthGearValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        G.FifthGearBox.Value = floatOption.Value;
    }

    private static void SixthGearValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        G.SixthGearBox.Value = floatOption.Value;
    }

    private static void SeventhGearValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        G.SeventhGearBox.Value = floatOption.Value;
    }

    private static void EighthGearValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        G.EighthGearBox.Value = floatOption.Value;
    }

    private static void NinthGearValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        G.NinthGearBox.Value = floatOption.Value;
    }

    private static void TenthGearValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        G.TenthGearBox.Value = floatOption.Value;
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