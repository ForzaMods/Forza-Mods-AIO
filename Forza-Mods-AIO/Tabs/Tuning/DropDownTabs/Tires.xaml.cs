using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

public partial class Tires
{
    public static Tires t;
    public static float TireFrontLeftDivider = 1f, TireFrontRightDivider = 1f, TireRearLeftDivider = 1f, TireRearRightDivider = 1f;

    public Tires()
    {
        InitializeComponent();
        t = this;
    }

    private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        string Address = null;

        foreach (var Field in typeof(Tuning_Addresses).GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(string)))
        {
            if (Field.Name != sender.GetType().GetProperty("Name")!.GetValue(sender)!.ToString()!.Remove(sender.GetType()!.GetProperty("Name")!.GetValue(sender)!.ToString()!.Length - 3)) continue;
            Address = Field.GetValue(Field) as string;
        }

        foreach (FrameworkElement Element in t.GetChildren())
        {
            if (Element.GetType() != typeof(ComboBox) && Element.Name != ((NumericUpDown)sender).Name.Replace("Box", "_Psi_Bar_Box")) continue;
            var Multiply = ((ComboBox)Element).SelectedIndex is 0 or -1 ? 1 : 14.5f;
            MainWindow.mw.m.WriteMemory(Address, (float)(((NumericUpDown)sender)!.Value! * Multiply));
        }
    }

    private void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        bool PSI = ((ComboBox)sender).SelectedIndex is 0 or -1;
        
        foreach (var Divider in typeof(Tires).GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(float)))
        {
            if (Divider.Name != sender.GetType().GetProperty("Name")!.GetValue(sender)!.ToString()!.Remove(sender.GetType()!.GetProperty("Name")!.GetValue(sender)!.ToString()!.Length - 12) + "Divider") continue;
            Divider.SetValue(Divider, PSI ? 1f : 14.5f);
        }

        foreach (FrameworkElement Element in t.GetChildren())
        {
            var ElementName = Element.GetType().GetProperty("Name")!.GetValue(Element)!.ToString();
            var SenderName = sender.GetType().GetProperty("Name")!.GetValue(Element)!.ToString();
            if (ElementName != SenderName!.Remove(SenderName.Length - 12) + "Box") continue;
            Element.GetType()!.GetProperty("Interval")!.SetValue(Element, PSI ? 1 : 0.1);
        }
    }
}