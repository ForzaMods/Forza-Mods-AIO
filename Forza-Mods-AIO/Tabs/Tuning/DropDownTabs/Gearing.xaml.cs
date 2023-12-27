using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

public partial class Gearing
{
    public static Gearing G { get; private set; } = null!;
    public Gearing()
    {
        InitializeComponent();
        G = this;
    }

    private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        ((NumericUpDown)sender).Value = Math.Round(Convert.ToDouble(((NumericUpDown)sender).Value), 3);

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

        var value = ((NumericUpDown)sender).Value;
        
        if (address == 0 || value == null)
        {
            return;
        }
    
        Mw.M.WriteMemory(address, Convert.ToSingle(value));
    }
}