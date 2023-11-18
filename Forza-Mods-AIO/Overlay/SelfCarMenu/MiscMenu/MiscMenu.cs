using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.MiscMenu;

public abstract class MiscMenu
{
    private static readonly MenuOption UnlimitedBuildBudgetToggle = new("Unlimited Build Budget", OptionType.Bool, false);
    private static readonly MenuOption UnbreakableSkillScoreToggle = new("Unbreakable Skill Score", OptionType.Bool, false);

    public static readonly List<MenuOption> MiscMenuOptions = new()
    {
        UnlimitedBuildBudgetToggle,
        UnbreakableSkillScoreToggle
    };

    public static void InitiateSubMenu()
    {
        UnlimitedBuildBudgetToggle.ValueChangedHandler += UnlimitedBuildBudgetToggleChanged;
        UnbreakableSkillScoreToggle.ValueChangedHandler += UnbreakableSkillScoreToggleChanged;
    }

    // Event handlers
    private static void UnlimitedBuildBudgetToggleChanged(object s, EventArgs e)
    {
        MiscellaneousPage.MiscPage.Dispatcher.Invoke(() =>
        {
            MiscellaneousPage.MiscPage.RemoveBuildCapSwitch.IsOn = (bool)UnlimitedBuildBudgetToggle.Value;
        });
    }

    private static void UnbreakableSkillScoreToggleChanged(object s, EventArgs e)
    {
        MiscellaneousPage.MiscPage.Dispatcher.Invoke(() =>
        {
            MiscellaneousPage.MiscPage.SkillToggle.IsOn = (bool)UnbreakableSkillScoreToggle.Value;
        });
    }
}