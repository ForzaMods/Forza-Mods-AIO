using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

public partial class Tires
{
    public static Tires T { get; private set; } = null!;
    public static float TireFrontLeftDivider = 1f, TireFrontRightDivider = 1f, TireRearLeftDivider = 1f, TireRearRightDivider = 1f;

    public Tires()
    {
        InitializeComponent();
        T = this;
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
            
            if (field.Name != senderName.Replace("Box", string.Empty))
            {
                continue;
            }

            address = (UIntPtr)(field.GetValue(field) ?? 0);
        }

        if (address == 0)
        {
            return;
        }

        foreach (var visual in T.GetChildren())
        {
            var element = (FrameworkElement)visual;
            
            if (element.GetType() != typeof(ComboBox) &&
                element.Name != ((NumericUpDown)sender).Name.Replace("Box", "UnitBox"))
            {
                continue;
            }
            
            var multiply = ((ComboBox)element).SelectedIndex is 0 or -1 ? 1 : 14.5f;
            Mw.M.WriteMemory(address, Convert.ToSingle((sender as NumericUpDown).Value * multiply));
        }
    }

    private void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var psi = ((ComboBox)sender).SelectedIndex is 0 or -1;
        
        foreach (var divider in typeof(Tires).GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(float)))
        {
            var senderName = sender.GetType().GetProperty("Name")!.GetValue(sender)!.ToString()!;
            
            if (divider.Name != senderName!.Replace("UnitBox","Divider"))
            {
                continue;
            }
            
            divider.SetValue(divider, psi ? 1f : 14.5f);
        }

        foreach (var visual in T.GetChildren())
        {
            var element = (FrameworkElement)visual;
            var elementName = element.GetType().GetProperty("Name")!.GetValue(element)!.ToString();
            var senderName = sender.GetType().GetProperty("Name")!.GetValue(element)!.ToString();
            
            if (elementName != senderName!.Replace("UnitBox","Box"))
            {
                continue;
            }
            element.GetType()!.GetProperty("Interval")!.SetValue(element, psi ? 1 : 0.1);
        }
    }
}