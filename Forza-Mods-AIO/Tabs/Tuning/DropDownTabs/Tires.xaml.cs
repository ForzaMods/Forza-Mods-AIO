using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs
{
    public partial class Tires : Page
    {
        public static Tires t;

        public static float TireFrontLeftDivider = 1f;
        public static float TireFrontRightDivider = 1f;
        public static float TireRearLeftDivider = 1f;
        public static float TireRearRightDivider = 1f;

        public Tires()
        {
            InitializeComponent();
            t = this;
        }

        private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try
            {
                // Got this looping method from here and slightly modified it: https://stackoverflow.com/a/51424624
                foreach (FieldInfo Address in typeof(Tuning_Addresses).GetFields(BindingFlags.Public | BindingFlags.Static).Where(field => field.FieldType == typeof(string)))
                    if (Address.Name == sender.GetType().GetProperty("Name").GetValue(sender).ToString().Remove(sender.GetType().GetProperty("Name").GetValue(sender).ToString().Length - 3))
                        foreach (FrameworkElement Element in t.GetChildren())
                            if (Element.GetType() == typeof(ComboBox) && Element.Name == ((NumericUpDown)sender).Name.Replace("Box", "_Psi_Bar_Box"))
                                if (((ComboBox)Element).SelectedIndex is 0 or -1)
                                    MainWindow.mw.m.WriteMemory(Address.GetValue(null) as string, (float)((MahApps.Metro.Controls.NumericUpDown)sender).Value);
                                else if (((ComboBox)Element).SelectedIndex is 1)
                                    MainWindow.mw.m.WriteMemory(Address.GetValue(null) as string, (float)(((NumericUpDown)sender).Value * 14.5));
            }
            catch { }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (FieldInfo Divider in typeof(Tires).GetFields(BindingFlags.Public | BindingFlags.Static).Where(field => field.FieldType == typeof(float)))
                if (Divider.Name == sender.GetType().GetProperty("Name").GetValue(sender).ToString().Remove(sender.GetType().GetProperty("Name").GetValue(sender).ToString().Length - 12) + "Divider")
                    if (((ComboBox)sender).SelectedIndex is 0 or -1)
                        Divider.SetValue(Divider, 1f);
                    else if (((ComboBox)sender).SelectedIndex is 1)
                        Divider.SetValue(Divider, 14.5f);

            foreach (FrameworkElement Element in t.GetChildren())
                if (Element.GetType().GetProperty("Name").GetValue(Element).ToString() == sender.GetType().GetProperty("Name").GetValue(sender).ToString().Remove(sender.GetType().GetProperty("Name").GetValue(sender).ToString().Length - 12) + "Box")
                    if (((ComboBox)sender).SelectedIndex is 0 or -1)
                        Element.GetType().GetProperty("Interval").SetValue(Element, 1);
                    else if (((ComboBox)sender).SelectedIndex is 1)
                        Element.GetType().GetProperty("Interval").SetValue(Element, 0.1);
        }
    }
}