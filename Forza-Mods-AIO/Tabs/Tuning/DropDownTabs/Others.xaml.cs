using MahApps.Metro.Controls;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs
{
    public partial class Others : Page
    {
        public static Others o;
        public Others()
        {
            InitializeComponent();
            o = this;
        }

        public void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try
            {
                // Got this looping method from here and slightly modified it: https://stackoverflow.com/a/51424624
                foreach (FieldInfo Address in typeof(Tuning_Addresses).GetFields(BindingFlags.Public | BindingFlags.Static).Where(field => field.FieldType == typeof(string)))
                    if (Address.Name == sender.GetType().GetProperty("Name").GetValue(sender).ToString().Remove(sender.GetType().GetProperty("Name").GetValue(sender).ToString().Length - 3))
                            MainWindow.mw.m.WriteMemory(Address.GetValue(null) as string, "float", ((NumericUpDown)sender).Value.ToString());
            }
            catch { }
        }

        private void FreezeToggled(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleSwitch).IsOn)
            {

            }
            else
            {

            }
        }
    }
}
