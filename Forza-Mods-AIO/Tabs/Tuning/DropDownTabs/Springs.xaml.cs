using System;
using System.CodeDom;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

public partial class Springs
{
    private float _frontPreviousRestrictionValue;
    private float _rearPreviousRestrictionValue;
    public bool RideHeightCodeChange;

    public static Springs Sp { get; private set; } = null!;

    public Springs()
    {
        InitializeComponent();
        Sp = this;
    }

    private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        TuningAddresses.ChangeValue(sender);
    }
    
    private void RideHeightChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached || RideHeightCodeChange)
        {
            return;
        }
     
        var senderName = sender.GetType().GetProperty("Name")!.GetValue(sender)!.ToString()!;
        var address = GetAddress(senderName);

        ((NumericUpDown)sender).Value = Math.Round(Convert.ToDouble(((NumericUpDown)sender).Value),3);
        var value = ((NumericUpDown)sender).Value;
        
        if (address == 0 || value == null)
        {
            return;
        }

        var comboBox = Sp.GetChildren(Sp).Cast<FrameworkElement>()
            .Where(element => element.GetType() == typeof(ComboBox) || element.Name == senderName.Replace("Box", "UnitBox"))
            .Cast<ComboBox>().FirstOrDefault();

        if (comboBox == null!)
        {
            return;
        }

        var index = comboBox.SelectedIndex;
        var convertedValue = ConvertRideHeightToGameValue(index, Convert.ToSingle(value));
        var writeValue = Convert.ToSingle(convertedValue);
        Mw.M.WriteMemory(address, writeValue);
    }

    private static UIntPtr GetAddress(string senderName)
    {
        UIntPtr address = 0;
        var fields = typeof(TuningAddresses)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(UIntPtr));
        
        foreach (var field in fields)
        {
            if (field.Name != senderName.Remove(senderName.Length - 3))
            {
                continue;
            }

            address = (UIntPtr)(field.GetValue(field) ?? 0);
        }

        return address;
    }
    
    public static double ConvertGameValueToUnit(ComboBox comboBox, UIntPtr addy)
    {
        var value = Mw.M.ReadMemory<float>(addy);
        
        return comboBox.SelectedIndex switch
        {
            0 => Math.Round(RideHeightToCentimeters(value), 3),
            1 => Math.Round(RideHeightToInches(value), 3),
            _ => 0
        };
    }
    
    private static double ConvertRideHeightToGameValue(int comboBoxIndex, double value)
    {
        return comboBoxIndex switch
        {
            0 => ConvertToOriginalValue(value),
            1 => ConvertToOriginalValue(ConvertToCentimetersFromInches(value)),
            _ => 0
        };
    }
    
    private static double ConvertToCentimetersFromInches(double inchesValue)
    {
        return inchesValue * 2.54;
    }

    private static double ConvertToOriginalValue(double valueInCentimeters)
    {
        return valueInCentimeters / 100;
    }
    
    private static double RideHeightToInches(double rideHeightValue)
    {
        return RideHeightToCentimeters(rideHeightValue) / 2.54;
    }
    
    private static double RideHeightToCentimeters(double rideHeightValue)
    {
        return rideHeightValue * 100;
    }

    private void FrontRestriction_Toggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || FrontRestriction == null)
        {
            return;
        }

        switch (FrontRestriction.IsOn)
        {
            case true:
            {
                _frontPreviousRestrictionValue = Mw.M.ReadMemory<float>(TuningAddresses.FrontRestriction);
                try { Mw.M.WriteMemory(TuningAddresses.FrontRestriction, 0.01f); } catch { }

                break;
            }
            case false:
            {
                try { Mw.M.WriteMemory(TuningAddresses.FrontRestriction, _frontPreviousRestrictionValue); } catch { }
                break;
            }
        }
    }

    private void RearRestriction_Toggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || RearRestriction == null)
        {
            return;
        }

        switch (RearRestriction.IsOn)
        {
            case true:
            {
                _rearPreviousRestrictionValue = Mw.M.ReadMemory<float>(TuningAddresses.RearRestriction);
                try { Mw.M.WriteMemory(TuningAddresses.RearRestriction, (float)0.01); } catch { }

                break;
            }
            case false:
            {
                try { Mw.M.WriteMemory(TuningAddresses.RearRestriction, _rearPreviousRestrictionValue); } catch { }

                break;
            }
        }
    }
}