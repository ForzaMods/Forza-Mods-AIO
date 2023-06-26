using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs
{
    public partial class Damping : Page
    {
        public static Damping d;
        public Damping()
        {
            InitializeComponent();
            d = this;
        }

        private void FrontAntirollBarsMinBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.FrontAntirollMin, "float", FrontAntirollBarsMinBox.Value.ToString()); } catch { }
        }

        private void FrontAntirollBarsMaxBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.FrontAntirollMax, "float", FrontAntirollBarsMaxBox.Value.ToString()); } catch { }
        }

        private void RearAntirollBarsMinBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.RearAntirollMin, "float", RearAntirollBarsMinBox.Value.ToString()); } catch { }
        }

        private void RearAntirollBarsMaxBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.RearAntirollMax, "float", RearAntirollBarsMaxBox.Value.ToString()); } catch { }
        }

        private void FrontReboundStiffnessMinBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.FrontReboundStiffnesMin, "float", FrontReboundStiffnessMinBox.Value.ToString()); } catch { }
        }

        private void FrontReboundStiffnessMaxBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.FrontReboundStiffnessMax, "float", FrontReboundStiffnessMaxBox.Value.ToString()); } catch { }
        }

        private void RearReboundStiffnessMinBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.RearReboundStiffnessMin, "float", RearReboundStiffnessMinBox.Value.ToString()); } catch { }
        }

        private void RearReboundStiffnessMaxBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.RearReboundStiffnessMax, "float", RearReboundStiffnessMaxBox.Value.ToString()); } catch { }
        }

        private void FrontBumpStiffnessMinBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.FrontBumpStiffnessMin, "float", FrontBumpStiffnessMinBox.Value.ToString()); } catch { }
        }

        private void FrontBumpStiffnessMaxBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.FrontBumpStiffnessMax, "float", FrontBumpStiffnessMaxBox.Value.ToString()); } catch { }
        }

        private void RearBumpStiffnessMinBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.RearBumpStiffnessMin, "float", RearBumpStiffnessMinBox.Value.ToString()); } catch { }
        }

        private void RearBumpStiffnessMaxBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.RearBumpStiffnessMax, "float", RearBumpStiffnessMaxBox.Value.ToString()); } catch { }
        }
    }
}
