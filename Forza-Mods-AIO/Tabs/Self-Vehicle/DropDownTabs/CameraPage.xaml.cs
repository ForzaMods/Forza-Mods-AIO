using System;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Windows;
using Forza_Mods_AIO.Resources;
using MahApps.Metro.Controls;
using static System.Convert;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class CameraPage
{
    public static CameraPage Camera { get; private set; } = null!;

    public static readonly Detour CameraDetour = new();
    private const string CameraBytes = "0F 10 01 81 79 26 EC 41 00 00 75 02 EB 21 81 79 26 16 43 00 00 75 02 EB 16 81 79 26 96 42 67 7B 75 02 EB 14 81 79 26 54 44 4D A1 75 12 EB 09 0F 10 05 10 00 00 00 EB 07 0F 10 05 0B 00 00 00 B0 01";
    
    public CameraPage()
    {
        InitializeComponent();
        Camera = this;
    }

    private void FovLimiters_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        UIntPtr address = 0;
        var type = sender.GetType();
        var name = type.GetProperty("Name")?.GetValue(sender)?.ToString()?.Replace("Num", "");

        var fields = typeof(SelfVehicleAddresses).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var field in fields.Where(f => f.FieldType == typeof(UIntPtr)))
        {
            if (field.Name != name)
            {
                continue;
            }

            address = (UIntPtr)(field.GetValue(field) ?? 0);
        }

        if (address == 0) return;
        Mw.M.WriteMemory(address, ToSingle(type.GetProperty("Value")?.GetValue(sender)));
    }

    public void UpdateValues()
    {
        if (!Mw.Attached)
        {
            return;
        }
        
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

    private void CameraSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        if (Mw.Gvp.Type != GameVerPlat.GameType.Fh5 && toggleSwitch.IsOn)
        {
            Detour.FailedHandler(toggleSwitch, CameraSwitch_OnToggled, true);
            return;
        }

        const string original = "0F10 01 B0 01";
        if (!CameraDetour.Setup(toggleSwitch, CameraHookAddr, original, CameraBytes, 5, true))
        {
            Detour.FailedHandler(toggleSwitch, CameraSwitch_OnToggled);
            CameraDetour.Clear();
            return;
        }

        var value = new Vector3(ToSingle(OffsetXNum.Value), ToSingle(OffsetYNum.Value), ToSingle(OffsetZNum.Value));
        var clean = new Vector3(0);

        CameraDetour.UpdateVariable(FovSwitch.IsOn ? ToSingle(FovNum.Value) : 0f);
        CameraDetour.UpdateVariable(OffsetSwitch.IsOn ? value : clean, 4);
    }

    private void FovSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        FovNum.Value = Math.Round(FovSlider.Value, 4);
    }

    private void FovNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (FovSwitch == null)
        {
            return;
        }
        
        FovSlider.Value = (double)FovNum!.Value!;
        
        if (!Mw.Attached)
        {
            return;
        }
        
        CameraDetour.UpdateVariable(ToSingle(sender.GetType().GetProperty("Value")?.GetValue(sender)) / 10f);
    }

    private void OffsetNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (OffsetZNum == null || sender is not NumericUpDown numericUpDown)
        {
            return;
        }

        numericUpDown.Value = Math.Round(ToDouble(numericUpDown.Value), 3);
        
        var value = new Vector3(ToSingle(OffsetXNum.Value), ToSingle(OffsetYNum.Value), ToSingle(OffsetZNum.Value));
        CameraDetour.UpdateVariable(value, 4);
    }
}