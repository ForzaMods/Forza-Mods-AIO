using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs
{
    public partial class Gearing : Page
    {
        public static Gearing G;
        public Gearing()
        {
            InitializeComponent();
            G = this;
        }

        public void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try
            {
                // Got this looping method from here and slightly modified it: https://stackoverflow.com/a/51424624
                foreach (FieldInfo address in typeof(TuningAddresses).GetFields(BindingFlags.Public | BindingFlags.Static).Where(field => field.FieldType == typeof(string)))
                    if (address.Name == sender.GetType().GetProperty("Name").GetValue(sender).ToString().Remove(sender.GetType().GetProperty("Name").GetValue(sender).ToString().Length - 3))
                        MainWindow.Mw.M.WriteMemory(address.GetValue(null) as string, (float)((MahApps.Metro.Controls.NumericUpDown)sender).Value);
            }
            catch { }
        }
    }
}
