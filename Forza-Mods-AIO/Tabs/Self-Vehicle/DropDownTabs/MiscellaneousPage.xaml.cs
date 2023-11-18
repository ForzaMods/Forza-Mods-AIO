using System;
using System.Threading.Tasks;
using System.Windows;
using Forza_Mods_AIO.Resources;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class MiscellaneousPage
{
    public static MiscellaneousPage? MiscPage;
    public static readonly Detour Build1Detour = new(), Build2Detour = new(), UnbSkillDetour = new();
    public static bool WasSkillDetoured;
    
    private const string Build1Fh4 = "F3 0F 11 B3 DC 03 00 00 C7 83 DC 03 00 00 00 00 00 00";
    private const string Build1Fh5 = "F3 0F 11 83 4C 04 00 00 C7 83 4C 04 00 00 00 00 00 00";
    private const string Build2Fh4 = "F3 0F 11 43 44 C7 43 44 00 00 00 00";
    private const string Build2Fh5 = "F3 0F 11 43 30 C7 43 30 00 00 00 00";
    private const string SkillDetour = "48 89 1D 49 00 00 00 48 8B 10 48 8B C8";
    
    public MiscellaneousPage()
    {
        InitializeComponent();
        MiscPage = this;
    }

    private void RemoveBuildCapSwitch_OnToggled(object? sender, RoutedEventArgs e)
    {
        var fh5 = Mw.Gvp.Name.Contains('5');
        var build1 = fh5 ? Build1Fh5 : Build1Fh4;
        var build2 = fh5 ? Build2Fh5 : Build2Fh4;
        if (!Build1Detour.Setup(sender, BuildCapAddrAsm1, build1, 8) || !Build2Detour.Setup(sender, BuildCapAddrAsm2, build2, 8))
        {
            RemoveBuildCapSwitch.Toggled -= RemoveBuildCapSwitch_OnToggled;
            RemoveBuildCapSwitch.IsOn = false;
            RemoveBuildCapSwitch.Toggled += RemoveBuildCapSwitch_OnToggled;
            MessageBox.Show("Failed");
            return;
        }
        
        Build1Detour.Toggle();
        Build2Detour.Toggle();
    }

    private void Skill_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!WasSkillDetoured)
        {
            GetSkillAddresses(sender);
            WasSkillDetoured = true;
        }
        
        if (!SkillToggle.IsOn)
        {
            Mw.M.WriteMemory(WorldCollisionThreshold, 12f);
            Mw.M.WriteMemory(CarCollisionThreshold,12f);
            Mw.M.WriteMemory(SmashableCollisionTolerance,22f);
            return;
        }
        
        Mw.M.WriteMemory(WorldCollisionThreshold, 9999999999f);
        Mw.M.WriteMemory(CarCollisionThreshold,9999999999f);
        Mw.M.WriteMemory(SmashableCollisionTolerance,9999999999f);
    }

    private void GetSkillAddresses(object sender)
    {
        if (!UnbSkillDetour.Setup(sender, UnbSkillHook, SkillDetour, 6, true))
        {
            SkillToggle.Toggled -= Skill_OnToggled;
            SkillToggle.IsOn = false;
            SkillToggle.Toggled += Skill_OnToggled;
            MessageBox.Show("Failed");
            return;
        }

        WorldCollisionThreshold = UnbSkillDetour.ReadVariable<UIntPtr>();
        CarCollisionThreshold = WorldCollisionThreshold + 8;
        SmashableCollisionTolerance = WorldCollisionThreshold + 16;
        UnbSkillDetour.Clear();
        UnbSkillDetour.Destroy();
    }
}