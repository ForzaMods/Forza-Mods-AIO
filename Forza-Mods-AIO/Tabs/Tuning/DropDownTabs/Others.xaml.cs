using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

public partial class Others
{
    public static Others O { get; private set; } = null!;
    public Others()
    {
        InitializeComponent();
        O = this;
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
}