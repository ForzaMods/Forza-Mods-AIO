using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

public partial class Tires
{
    public static Tires T;
    public static float TireFrontLeftDivider = 1f, TireFrontRightDivider = 1f, TireRearLeftDivider = 1f, TireRearRightDivider = 1f;

    public Tires()
    {
        InitializeComponent();
        T = this;
    }

    private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        string? address = null;

        foreach (var field in typeof(TuningAddresses).GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(string)))
        {
            var senderName = sender.GetType().GetProperty("Name")!.GetValue(sender)!.ToString()!;
            
            if (field.Name != senderName.Remove(senderName.Length - 3))
            {
                continue;
            }
            
            address = field.GetValue(field) as string;
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
            MainWindow.Mw.M.WriteMemory(address, (float)(((NumericUpDown)sender)!.Value! * multiply));
        }
    }

    private void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var psi = ((ComboBox)sender).SelectedIndex is 0 or -1;
        
        foreach (var divider in typeof(Tires).GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(float)))
        {
            var senderName = sender.GetType().GetProperty("Name")!.GetValue(sender)!.ToString()!;
            
            if (divider.Name != senderName.Remove(senderName.Length - 12) + "Divider")
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
            
            if (elementName != senderName!.Remove(senderName.Length - 12) + "Box")
            {
                continue;
            }
            element.GetType()!.GetProperty("Interval")!.SetValue(element, psi ? 1 : 0.1);
        }
    }
}