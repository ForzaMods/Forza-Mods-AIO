using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using Forza_Mods_AIO.Resources;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class FovPage
{
    public static FovPage? _FovPage;

    public static readonly Detour FovLockDetour = new();
    private const string FovLockBytes = "0F 10 01 80 79 26 EC 75 0E 80 79 27 41 75 08 80 79 2A 80 75 02 EB 12 80 79 26 16 75 13 80 79 27 43 75 0D 80 79 2A C0 75 07 0F 10 05 07 00 00 00 B0 01";
    
    public FovPage()
    {
        InitializeComponent();
        _FovPage = this;
    }

    private void FovLimiters_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        string? address = null;
        var type = sender.GetType();
        var name = type.GetProperty("Name")?.GetValue(sender)?.ToString()?.Replace("Num", "");

        var fields = typeof(SelfVehicleAddresses).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var field in fields.Where(f => f.FieldType == typeof(string)))
        {
            
            if (field.Name != name)
            {
                continue;
            }

            address = field.GetValue(field) as string;
        }

        try
        {
            Mw.M.WriteMemory(address, Convert.ToSingle(type.GetProperty("Value")?.GetValue(sender)));
        }
        catch
        {
            
        }
    }

    public void UpdateValues()
    {
        ChaseMinNum.Value = Mw.M.ReadMemory<float>(ChaseMin);
        ChaseMaxNum.Value = Mw.M.ReadMemory<float>(ChaseMax);
        FarChaseMinNum.Value = Mw.M.ReadMemory<float>(FarChaseMin);
        FarChaseMaxNum.Value = Mw.M.ReadMemory<float>(FarChaseMax);
        DriverMinNum.Value = Mw.M.ReadMemory<float>(DriverMin);
        DriverMaxNum.Value = Mw.M.ReadMemory<float>(DriverMax);
        HoodMinNum.Value = Mw.M.ReadMemory<float>(HoodMin);
        HoodMaxNum.Value = Mw.M.ReadMemory<float>(HoodMax);
        BumperMinNum.Value = Mw.M.ReadMemory<float>(BumperMin);
        BumperMaxNum.Value = Mw.M.ReadMemory<float>(BumperMax);
    }

    private void FovSwitch_OnToggled(object? sender, RoutedEventArgs e)
    {
        if (Mw.Gvp.Name == "Forza Horizon 4" && FovSwitch.IsOn)
        {
            MessageBox.Show("Fov Lock Isnt implemented for FH4. Use limiters instead");
            FovSwitch.Toggled -= FovSwitch_OnToggled;
            FovSwitch.IsOn = false;
            FovSwitch.Toggled += FovSwitch_OnToggled;
            return;
        }

        if (!FovLockDetour.Setup(sender, (UIntPtr)FovHookAddr, FovLockBytes, 5, true))
        {
            FovSwitch.Toggled -= FovSwitch_OnToggled;
            FovSwitch.IsOn = false;
            FovSwitch.Toggled += FovSwitch_OnToggled;
            FovLockDetour.Clear();
            MessageBox.Show("Failed.");
            return;
        }

        FovLockDetour.Toggle();
    }

    private void FovSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (_FovPage == null) return;
        try
        {
            FovNum.Value = Math.Round(FovSlider.Value, 4);
        }
        catch
        {
            // ignored
        }
    }

    private void FovNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (_FovPage == null) return;
        try
        {
            FovSlider.Value = (double)FovNum!.Value!;
            FovLockDetour.UpdateVariable(Convert.ToSingle(sender.GetType().GetProperty("Value")?.GetValue(sender)) / 10f);
        }
        catch
        {
            // ignored
        }
    }
}