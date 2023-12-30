using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.UnlocksPage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu;

public abstract class UnlocksMenu
{
    private static readonly IntOption XpValue = new("Value", 0);
    private static readonly ToggleOption XpToggle = new("Toggle", false, "This feature will add XP based on how many you set in the numeric box.");
    private static readonly ButtonOption XpPull = new("Pull", () =>
    {
        XpValue.Value = Convert.ToInt32(Unlocks.XpNum.Value);
    });
    
    private static readonly IntOption CreditsValue = new("Value", 0);
    private static readonly ToggleOption CreditsToggle = new("Toggle", false, "This feature will set your credits based on how the value you set in the numeric box. You must either spend/get some money, or do a wheelspin for it to take effect.");
    private static readonly ButtonOption CreditsPull = new("Pull", () =>
    {
        CreditsValue.Value = Convert.ToInt32(Unlocks.CreditsNum.Value);
    });
    
    public static readonly List<MenuOption> CurrencyMenuOptions = new()
    {
        new SubHeaderOption("XP on unpause"),
        XpValue,
        XpPull,
        XpToggle,
        new SubHeaderOption("Credits"),
        CreditsValue,
        CreditsPull,
        CreditsToggle
    };

    public static void InitiateSubMenu()
    {
        XpValue.ValueChanged += XpValue_OnChanged;
        XpToggle.Toggled += XpToggle_OnChanged;
        CreditsValue.ValueChanged += CreditsValue_OnChanged;
        CreditsToggle.Toggled += CreditsToggle_OnChanged;
    }

    private static void XpValue_OnChanged(object s, EventArgs e)
    {
        Unlocks.XpNum.Value = XpValue.Value;
    }
    
    private static void XpToggle_OnChanged(object s, EventArgs e)
    {
        Unlocks.XpSwitch.IsOn = XpToggle.IsOn;
    }
    
    private static void CreditsValue_OnChanged(object s, EventArgs e)
    {
        Unlocks.CreditsNum.Value = CreditsValue.Value;
    }
    
    private static void CreditsToggle_OnChanged(object s, EventArgs e)
    {
        Unlocks.CreditsSwitch.IsOn = CreditsToggle.IsOn;
    }
}