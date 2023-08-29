using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs
{
    public partial class Springs : Page
    {
        float FrontPreviousValue = 0;
        float FrontPreviousRestrictionValue = 0;
        float RearPreviousValue = 0;
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
                FrontPreviousValue = (float)FrontRideHeightMinBox.Value;
                FrontPreviousRestrictionValue = MainWindow.mw.m.ReadFloat(Tuning_Addresses.FrontRestriction);
                FrontRideHeightMinBox.IsEnabled = false;

                try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.FrontRideHeightMin, "float", 0.03.ToString()); } catch { }
                try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.FrontRestriction, "float", 0.01.ToString()); } catch { }
            }
            if (!FrontRestriction.IsOn)
            {
                FrontRideHeightMinBox.IsEnabled = true;
                FrontRideHeightMinBox.Value = FrontPreviousValue;

                try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.FrontRideHeightMin, "float", FrontPreviousValue.ToString()); } catch { }
                try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.FrontRestriction, "float", FrontPreviousRestrictionValue.ToString()); } catch { }
            }
        }

        private void RearRestriction_Toggled(object sender, RoutedEventArgs e)
        {
            if (RearRestriction.IsOn)
            {
                RearPreviousValue = (float)RearRideHeightMinBox.Value;
                RearPreviousRestrictionValue = MainWindow.mw.m.ReadFloat(Tuning_Addresses.RearRestriction);
                RearRideHeightMinBox.IsEnabled = false;

                try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.RearRideHeightMin, "float", 0.03.ToString()); } catch { }
                try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.RearRestriction, "float", 0.01.ToString()); } catch { }
            }
            if (!RearRestriction.IsOn)
            {
                RearRideHeightMinBox.IsEnabled = true;
                RearRideHeightMinBox.Value = RearPreviousValue;

                try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.RearRideHeightMin, "float", RearPreviousValue.ToString()); } catch { }
                try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.RearRestriction, "float", RearPreviousRestrictionValue.ToString()); } catch { }
            }
        }
    }
}
