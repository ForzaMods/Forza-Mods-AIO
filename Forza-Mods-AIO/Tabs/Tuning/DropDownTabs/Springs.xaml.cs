using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs
{
    public partial class Springs : Page
    {
        float _frontPreviousRestrictionValue = 0;
        float _rearPreviousRestrictionValue = 0;
        public static Springs Sp;

        public Springs()
        {
            InitializeComponent();
            Sp = this;
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

        private void FrontRestriction_Toggled(object sender, RoutedEventArgs e)
        {
            if (FrontRestriction.IsOn)
            {
                _frontPreviousRestrictionValue = MainWindow.Mw.M.ReadMemory<float>(TuningAddresses.FrontRestriction);
                try { MainWindow.Mw.M.WriteMemory(TuningAddresses.FrontRestriction, (float)0.01); } catch { }
            }
            else if (!FrontRestriction.IsOn)
            {
                try { MainWindow.Mw.M.WriteMemory(TuningAddresses.FrontRestriction, _frontPreviousRestrictionValue); } catch { }
            }
        }

        private void RearRestriction_Toggled(object sender, RoutedEventArgs e)
        {
            if (RearRestriction.IsOn)
            {
                _rearPreviousRestrictionValue = MainWindow.Mw.M.ReadMemory<float>(TuningAddresses.RearRestriction);
                try { MainWindow.Mw.M.WriteMemory(TuningAddresses.RearRestriction, (float)0.01); } catch { }
            }
            else if (!RearRestriction.IsOn)
            {
                try { MainWindow.Mw.M.WriteMemory(TuningAddresses.RearRestriction, _rearPreviousRestrictionValue); } catch { }
            }
        }
    }
}
