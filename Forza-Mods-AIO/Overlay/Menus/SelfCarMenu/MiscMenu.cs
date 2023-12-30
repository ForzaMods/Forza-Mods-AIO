using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.MiscellaneousPage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu;

public abstract class MiscMenu
{
    private static readonly ToggleOption UnlimitedBuildBudgetToggle = new("Unlimited Build Budget", false);
    private static readonly ToggleOption UnbreakableSkillScoreToggle = new("Unbreakable Skill Score", false);

    public static readonly List<MenuOption> MiscMenuOptions = new()
    {
        new SubHeaderOption("Misc Toggles"),
        UnlimitedBuildBudgetToggle,
        UnbreakableSkillScoreToggle
    };

    public static void InitiateSubMenu()
    {
        UnlimitedBuildBudgetToggle.Toggled += UnlimitedBuildBudgetToggleChanged;
        UnbreakableSkillScoreToggle.Toggled += UnbreakableSkillScoreToggleChanged;
    }

    private static void UnlimitedBuildBudgetToggleChanged(object s, EventArgs e)
    {
        MiscPage.RemoveBuildCapSwitch.IsOn = UnlimitedBuildBudgetToggle.IsOn;
    }

    private static void UnbreakableSkillScoreToggleChanged(object s, EventArgs e)
    {
        MiscPage.SkillToggle.IsOn = UnbreakableSkillScoreToggle.IsOn;
    }
}