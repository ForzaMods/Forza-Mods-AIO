using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class FovPage
{
    public static FovPage? _fovPage;
    
    public FovPage()
    {
        InitializeComponent();
        _fovPage = this;
    }

    private void FovLimiters_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        string Address = null;

        foreach (var Field in typeof(Self_Vehicle_Addrs).GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(string)))
        {
            if (Field.Name != sender.GetType().GetProperty("Name").GetValue(sender).ToString().Replace("Num", string.Empty)) continue;
            Address = Field.GetValue(Field) as string;
        }

        try { MainWindow.mw.m.WriteMemory(Address, Convert.ToSingle(sender.GetType().GetProperty("Value").GetValue(sender))); }
        catch {}
    }

    public void UpdateValues()
    {
        ChaseMinNum.Value = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.ChaseMin);
        ChaseMaxNum.Value = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.ChaseMax);
        FarChaseMinNum.Value = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.FarChaseMin);
        FarChaseMaxNum.Value = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.FarChaseMax);
        DriverMinNum.Value = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.DriverMin);
        DriverMaxNum.Value = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.DriverMax);
        HoodMinNum.Value = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.HoodMin);
        HoodMaxNum.Value = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.HoodMax);
        BumperMinNum.Value = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.BumperMin);
        BumperMaxNum.Value = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.BumperMax);
    }

    private void FovSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (MainWindow.mw.gvp.Name == "Forza Horizon 4" && FovSwitch.IsOn)
        {
            MessageBox.Show("Fov Lock Isnt implemented for FH4. Use limiters instead");
            FovSwitch.IsOn = false;
            return;
        }
        
        if ((bool)sender.GetType().GetProperty("IsOn").GetValue(sender))
        {
            Self_Vehicle_ASM.Fov();
            return;
        }

        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.FovHookAddr, Self_Vehicle_ASM.FovOrigBytes);
    }

    private void FovSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (_fovPage == null) return;
        try { FovNum.Value = Math.Round(FovSlider.Value, 4); }
        catch {}
    }

    private void FovNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (_fovPage == null) return;
        try 
        {
            FovSlider.Value = (double)FovNum.Value;
            MainWindow.mw.m.WriteMemory(Self_Vehicle_ASM.CodeCave11 + 0x52, Convert.ToSingle(sender.GetType().GetProperty("Value").GetValue(sender)) / 10f);
        }
        catch { }
    }
}