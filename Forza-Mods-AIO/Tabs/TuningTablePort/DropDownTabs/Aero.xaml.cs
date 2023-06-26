using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs
{
    public partial class Aero : Page
    {
        public static Aero ae;
        public Aero()
        {
            InitializeComponent();
            ae = this;
        }

        private void FrontAeroMinBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.FrontAeroMin, "float", FrontAeroMinBox.Value.ToString()); } catch { }
        }

        private void FrontAeroMaxBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.FrontAeroMax, "float", FrontAeroMaxBox.Value.ToString()); } catch { }
        }

        private void RearAeroMinBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.RearAeroMin, "float", RearAeroMinBox.Value.ToString()); } catch { }
        }

        private void RearAeroMaxBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.RearAeroMax, "float", RearAeroMaxBox.Value.ToString()); } catch { }
        }
    }
}
