using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs
{
    public partial class Springs : Page
    {
        float FrontPreviousRestrictionValue = 0;
        float RearPreviousRestrictionValue = 0;
        public static Springs sp;

        public Springs()
        {
            InitializeComponent();
            sp = this;
        }

        public void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try
            {
                // Got this looping method from here and slightly modified it: https://stackoverflow.com/a/51424624
                foreach (FieldInfo Address in typeof(Tuning_Addresses).GetFields(BindingFlags.Public | BindingFlags.Static).Where(field => field.FieldType == typeof(string)))
                    if (Address.Name == sender.GetType().GetProperty("Name").GetValue(sender).ToString().Remove(sender.GetType().GetProperty("Name").GetValue(sender).ToString().Length - 3))
                        MainWindow.mw.m.WriteMemory(Address.GetValue(null) as string, "float", ((MahApps.Metro.Controls.NumericUpDown)sender).Value.ToString());
            }
            catch { }
        }

        private void FrontRestriction_Toggled(object sender, RoutedEventArgs e)
        {
            if (FrontRestriction.IsOn)
            {
                FrontPreviousRestrictionValue = MainWindow.mw.m.ReadFloat(Tuning_Addresses.FrontRestriction);
                try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.FrontRestriction, "float", 0.01.ToString()); } catch { }
            }
            else if (!FrontRestriction.IsOn)
            {
                try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.FrontRestriction, "float", FrontPreviousRestrictionValue.ToString()); } catch { }
            }
        }

        private void RearRestriction_Toggled(object sender, RoutedEventArgs e)
        {
            if (RearRestriction.IsOn)
            {
                RearPreviousRestrictionValue = MainWindow.mw.m.ReadFloat(Tuning_Addresses.RearRestriction);
                try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.RearRestriction, "float", 0.01.ToString()); } catch { }
            }
            else if (!RearRestriction.IsOn)
            {
                try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.RearRestriction, "float", RearPreviousRestrictionValue.ToString()); } catch { }
            }
        }
    }
}
