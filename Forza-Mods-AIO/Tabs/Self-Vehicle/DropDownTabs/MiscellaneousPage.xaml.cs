using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Resources;
using MahApps.Metro.Controls;
using static System.Convert;
using static System.Math;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class MiscellaneousPage
{
    public static MiscellaneousPage MiscPage { get; private set; } = null!;
    public static readonly Detour Build1Detour = new(), Build2Detour = new(), ScaleDetour = new(), SellDetour = new();
    public static readonly Detour SkillTreeDetour = new(), ScoreDetour = new();
    public static readonly Detour SkillCostDetour = new(), DriftDetour = new(), TimeScaleDetour = new();
    public static readonly Detour UnbSkillDetour = new();
    public bool WasSkillDetoured;
    
    private const string Build1Fh4 = "F3 0F 11 B3 DC 03 00 00 C7 83 DC 03 00 00 00 00 00 00";
    private const string Build1Fh5 = "F3 0F 11 83 4C 04 00 00 C7 83 4C 04 00 00 00 00 00 00";
    private const string Build2Fh4 = "F3 0F 11 43 30 C7 43 30 00 00 00 00";
    private const string Build2Fh5 = "F3 0F 11 43 44 C7 43 44 00 00 00 00";
    private const string SkillDetour = "48 89 1D 05 00 00 00";
    private const string ScaleFh5 = "F3 0F 10 35 05 00 00 00";
    private const string ScaleFh4 = "F3 0F 59 05 05 00 00 00";
    private const string SellFh5 = "44 8B 35 05 00 00 00";
    private const string SellFh4 = "8B 3D 05 00 00 00";
    private const string SkillTree = "50 48 8B 05 0F 00 00 00 48 89 43 48 58 F3 0F 10 73 48";
    private const string SkillCost = "8B 05 05 00 00 00";
    private const string ScoreFh4 = "8B 78 04 0F AF 3D 08 00 00 00 48 85 DB";
    private const string ScoreFh5 = "0F AF 3D 05 00 00 00";
    private const string Drift = "F3 0F 59 3D 0E 00 00 00 F3 0F 58 F7 0F 28 7C 24 20";
    
    private const string TimeScale = "F3 0F 59 3D 11 00 00 00 F3 0F 5C C7 F3 0F 11 87 0C 04 00 00";
        
    public MiscellaneousPage()
    {
        InitializeComponent();
        MiscPage = this;
    }

    private void RemoveBuildCapSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        var isFh5 = Mw.Gvp.Type == GameVerPlat.GameType.Fh5;
        var build1 = isFh5 ? Build1Fh5 : Build1Fh4;
        var build2 = isFh5 ? Build2Fh5 : Build2Fh4;
        const string build1OrigFh5 = "F3 0F11 83 4C040000";
        const string build1OrigFh4 = "F3 0F11 B3 DC030000";
        const string build2OrigFh5 = "F3 0F 11 43 44";
        const string build2OrigFh4 = "F3 0F 11 43 30";

        var orig1 = isFh5 ? build1OrigFh5 : build1OrigFh4;
        var orig2 = isFh5 ? build2OrigFh5 : build2OrigFh4;
        
        if (!Build1Detour.Setup(sender, BuildAddr1, orig1, build1, 8) || 
            !Build2Detour.Setup(sender, BuildAddr2, orig2, build2, 5))
        {
            Build1Detour.Clear();
            Build2Detour.Clear();
            return;
        }
        
        Build1Detour.Toggle();
        Build2Detour.Toggle();
    }

    private async void Skill_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (!WasSkillDetoured)
        {
            await GetSkillAddresses(sender);
            WasSkillDetoured = true;
            return;
        }

        Mw.M.WriteMemory(WorldCollisionThreshold, SkillToggle.IsOn ? 9999999999f : 12f);
        Mw.M.WriteMemory(CarCollisionThreshold,SkillToggle.IsOn ? 9999999999f : 12f);
        Mw.M.WriteMemory(SmashAbleCollisionTolerance,SkillToggle.IsOn ? 9999999999f : 22f);
    }

    private async Task GetSkillAddresses(object sender)
    {
        Dispatcher.Invoke(() => SkillToggle.IsEnabled = false);
        
        const string orig = "48 8B 10 48 8B C8";
        if (!UnbSkillDetour.Setup(null, UnbSkillHook, orig, SkillDetour, 6, true, 0, true))
        {
            UnbSkillDetour.Clear();
            return;
        }

        var taskCompletionSource = new TaskCompletionSource<bool>();
        Task.Run(() =>
        {
            while ((WorldCollisionThreshold = UnbSkillDetour.ReadVariable<UIntPtr>()) == 0)
            {
                Task.Delay(25).Wait();
            }

            WorldCollisionThreshold += 0x2C;
        
            CarCollisionThreshold = WorldCollisionThreshold + 4;
            SmashAbleCollisionTolerance = WorldCollisionThreshold + 8;
        
            Mw.M.WriteMemory(WorldCollisionThreshold, 9999999999f);
            Mw.M.WriteMemory(CarCollisionThreshold,9999999999f);
            Mw.M.WriteMemory(SmashAbleCollisionTolerance,9999999999f);
            
            UnbSkillDetour.Destroy();
            UnbSkillDetour.Clear();
            taskCompletionSource.SetResult(true);
        });

        await taskCompletionSource.Task;
        Dispatcher.Invoke(() => SkillToggle.IsEnabled = true);
    }

    private void SellFactorSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        var isFh5 = Mw.Gvp.Type == GameVerPlat.GameType.Fh5;
        var sell = isFh5 ? SellFh5 : SellFh4;
        var count = isFh5 ? 7 : 6;
        
        const string origFh5 = "44 8B B3 80000000";
        const string origFh4 = "8B B8 D04B0000";
        var orig = isFh5 ? origFh5 : origFh4;
        
        if (!SellDetour.Setup(SellFactorSwitch, SellFactorAddr, orig, sell, count, true, 0, true))
        {
            SellDetour.Clear();
            return;
        }

        if (isFh5)
        {
            SellDetour.UpdateVariable(ToInt32(SellFactorNum.Value) * 50);
        }
        else
        {
            SellDetour.UpdateVariable(ToSingle(SellFactorNum.Value) * 50);
        }
        
        SellDetour.Toggle();
    }

    private void SellFactorNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (Mw.Gvp.Type == GameVerPlat.GameType.Fh5)
        {
            SellDetour.UpdateVariable(ToInt32(SellFactorNum.Value) * 50);
            return;
        }
        
        SellDetour.UpdateVariable(ToSingle(SellFactorNum.Value) * 50);
    }

    private void ScaleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        var isFh5 = Mw.Gvp.Type == GameVerPlat.GameType.Fh5;
        var scale = isFh5 ? ScaleFh5 : ScaleFh4;
        const string origFh5 = "F3 0F10 73 10";
        const string origFh4 = "F3 0F59 40 10";

        var orig = !isFh5 ? origFh4 : origFh5;
        
        if (!ScaleDetour.Setup(ScaleSwitch, ScaleAddr, orig, scale, 5, true, 0, true))
        {
            ScaleDetour.Clear();
            return;
        }

        ScaleDetour.UpdateVariable(ToSingle(ScaleNum.Value));
        ScaleDetour.Toggle();
    }

    private void ScaleNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        ScaleDetour.UpdateVariable(ToSingle(e.NewValue));
    }
    
    private void SkillTreeEditToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (Mw.Gvp.Type == GameVerPlat.GameType.Fh4 && SkillTreeEditToggle.IsOn)
        {
            Detour.FailedHandler(sender, SkillTreeEditToggle_OnToggled, true);
            return;
        }

        const string orig = "F3 0F10 73 48";
        if (!SkillTreeDetour.Setup(sender, SkillTreeAddr, orig, SkillTree, 5, true))
        {
            SkillTreeDetour.Clear();
            return;
        }
        
        SkillTreeDetour.UpdateVariable(ToSingle(SkillTreeNum.Value));
        SkillTreeDetour.Toggle();
    }

    private void SkillTreeNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        SkillTreeDetour.UpdateVariable(ToSingle(SkillTreeNum.Value));
    }


    private void ScoreSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        ScoreSlider.Value = Round(ScoreSlider.Value);
        ScoreNum.Value = ScoreSlider.Value;
    }

    private void ScoreToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        var fh4 = Mw.Gvp.Type == GameVerPlat.GameType.Fh4;
        var bytes = fh4 ? ScoreFh4 : ScoreFh5;
        var replace = fh4 ? 6 : 7;
        var save = !fh4;

        const string origFh5 = "8B 78 08 48 8B 4D 60";
        const string origFh4 = "8B 78 04 48 85 DB";

        var orig = fh4 ? origFh4 : origFh5;
        
        if (!ScoreDetour.Setup(sender, SkillScoreAddr, orig,bytes, replace, true, 0, save))
        {
            ScoreDetour.Clear();
            return;
        }
        
        ScoreDetour.UpdateVariable(ToInt32(ScoreNum.Value));
        ScoreDetour.Toggle();
    }

    private void ScoreNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (ScoreToggle == null)
        {
            return;
        }
        
        ScoreSlider.Value = ToDouble(e.NewValue);

        if (!Mw.Attached)
        {
            return;
        }
        
        ScoreDetour.UpdateVariable(ToInt32(e.NewValue));
    }

    private void SkillCostToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (Mw.Gvp.Type == GameVerPlat.GameType.Fh4 && SkillCostToggle.IsOn)
        {
            Detour.FailedHandler(sender, SkillCostToggle_OnToggled, true);
            return;
        }

        const string orig = "8B C3 48 8B 5C 24 30";
        if (!SkillCostDetour.Setup(sender, SkillCostAddr, orig, SkillCost, 7, true, 0, true))
        {
            SkillCostDetour.Clear();
            return;
        }
        
        SkillCostDetour.UpdateVariable(ToInt32(SkillCostNum.Value));
        SkillCostDetour.Toggle();
    }

    private void SkillCostNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        SkillCostDetour.UpdateVariable(ToInt32(e.NewValue));
    }

    private void DriftNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached || sender is not NumericUpDown numericUpDown || DriftSlider == null)
        {
            return;
        }
        
        DriftSlider.Value = ToDouble(numericUpDown.Value);
        DriftDetour.UpdateVariable(ToSingle(numericUpDown.Value));
    }

    private void DriftToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        if (Mw.Gvp.Type == GameVerPlat.GameType.Fh4 && DriftToggle.IsOn)
        {
            Detour.FailedHandler(sender, DriftToggle_OnToggled, true);
            return;
        }

        const string originalBytes = "F3 0F58 F7 0F28 7C 24 20";
        if (!DriftDetour.Setup(DriftToggle, DriftScoreAddr, originalBytes, Drift, 9, true))
        {
            DriftDetour.Clear();
            return;
        }
        
        DriftDetour.Toggle();
    }

    private void TimeScaleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        if (Mw.Gvp.Type == GameVerPlat.GameType.Fh4 && TimeScaleSwitch.IsOn)
        {
            return;
        }

        const string orig = "F3 0F5C C7 F3 0F11 87 0C040000";
        if (!TimeScaleDetour.Setup(TimeScaleSwitch, TimeScaleAddr, orig, TimeScale, 12, true))
        {
            TimeScaleDetour.Clear();
            return;
        }
        
        TimeScaleDetour.UpdateVariable(ToSingle(TimeScaleNum.Value));
        TimeScaleDetour.Toggle();
    }

    private void TimeScaleNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (TimeScaleSwitch == null || TimeScaleSlider == null)
        {
            return;
        }
        
        TimeScaleSlider.Value = ToDouble(e.NewValue);
        
        if (!Mw.Attached)
        {
            return;
        }

        TimeScaleDetour.UpdateVariable(ToSingle(e.NewValue));
    }

    private void TimeScaleSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (TimeScaleSwitch == null)
        {
            return;
        }

        TimeScaleNum.Value = ToDouble(Round(e.NewValue, 4));
    }

    private void DriftSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (!Mw.Attached || sender is not Slider slider || DriftNum == null)
        {
            return;
        }

        DriftNum.Value = Round(slider.Value, 3);
        DriftDetour.UpdateVariable(ToSingle(slider.Value));
    }
}