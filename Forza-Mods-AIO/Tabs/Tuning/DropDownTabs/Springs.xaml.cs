using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

public partial class Springs
{
    private float _frontPreviousRestrictionValue;
    private float _rearPreviousRestrictionValue;
    public static Springs Sp { get; private set; } = null!;

    public Springs()
    {
        InitializeComponent();
        Sp = this;
    }

    private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }
    
        UIntPtr address = 0;

        foreach (var field in typeof(TuningAddresses).GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(UIntPtr)))
        {
            var senderName = sender.GetType().GetProperty("Name")!.GetValue(sender)!.ToString()!;
            
            if (field.Name != senderName.Remove(senderName.Length - 3))
            {
                continue;
            }

            address = (UIntPtr)(field.GetValue(field) ?? 0);
        }

        if (address == 0)
        {
            return;
        }
    
        Mw.M.WriteMemory(address, (float)((MahApps.Metro.Controls.NumericUpDown)sender).Value);
    }

    private void FrontRestriction_Toggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        switch (FrontRestriction.IsOn)
        {
            case true:
            {
                _frontPreviousRestrictionValue = Mw.M.ReadMemory<float>(TuningAddresses.FrontRestriction);
                try { Mw.M.WriteMemory(TuningAddresses.FrontRestriction, (float)0.01); } catch { }

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
        if (!Mw.Attached)
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