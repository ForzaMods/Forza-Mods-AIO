using System.Windows;
using System.Windows.Controls;

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

        private void FrontSpringsMinBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.SpringFrontMin, "float", FrontSpringsMinBox.Value.ToString()); } catch { }
        }

        private void RearSpringsMinBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.SpringRearMin, "float", RearSpringsMinBox.ToString()); } catch { }
        }

        private void FrontSpringsMaxBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.SpringFrontMax, "float", FrontSpringsMaxBox.Value.ToString()); } catch { }
        }

        private void RearSpringsMaxBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.SpringRearMax, "float", RearSpringsMaxBox.Value.ToString()); } catch { }
        }

        private void FrontRideHeightMinBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.FrontRideHeightMin, "float", FrontRideHeightMinBox.Value.ToString()); } catch { }
        }

        private void FrontRideHeightMaxBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.FrontRideHeightMax, "float", FrontRideHeightMaxBox.Value.ToString()); } catch { }
        }

        private void RearRideHeightMinBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.RearRideHeightMin, "float", RearRideHeightMinBox.Value.ToString()); } catch { }
        }

        private void RearRideHeightMaxBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.RearRideHeightMax, "float", RearRideHeightMaxBox.Value.ToString()); } catch { }
        }

        private void FrontRestriction_Toggled(object sender, RoutedEventArgs e)
        {
            if (FrontRestriction.IsOn)
            {
                FrontPreviousValue = (float)FrontRideHeightMinBox.Value;
                FrontPreviousRestrictionValue = MainWindow.mw.m.ReadFloat(Addresses.FrontRestriction);
                FrontRideHeightMinBox.IsEnabled = false;

                try { MainWindow.mw.m.WriteMemory(Addresses.FrontRideHeightMin, "float", 0.03.ToString()); } catch { }
                try { MainWindow.mw.m.WriteMemory(Addresses.FrontRestriction, "float", 0.01.ToString()); } catch { }
            }
            if (!FrontRestriction.IsOn)
            {
                FrontRideHeightMinBox.IsEnabled = true;
                FrontRideHeightMinBox.Value = FrontPreviousValue;

                try { MainWindow.mw.m.WriteMemory(Addresses.FrontRideHeightMin, "float", FrontPreviousValue.ToString()); } catch { }
                try { MainWindow.mw.m.WriteMemory(Addresses.FrontRestriction, "float", FrontPreviousRestrictionValue.ToString()); } catch { }
            }
        }

        private void RearRestriction_Toggled(object sender, RoutedEventArgs e)
        {
            if (RearRestriction.IsOn)
            {
                RearPreviousValue = (float)RearRideHeightMinBox.Value;
                RearPreviousRestrictionValue = MainWindow.mw.m.ReadFloat(Addresses.RearRestriction);
                RearRideHeightMinBox.IsEnabled = false;

                try { MainWindow.mw.m.WriteMemory(Addresses.RearRideHeightMin, "float", 0.03.ToString()); } catch { }
                try { MainWindow.mw.m.WriteMemory(Addresses.RearRestriction, "float", 0.01.ToString()); } catch { }
            }
            if (!RearRestriction.IsOn)
            {
                RearRideHeightMinBox.IsEnabled = true;
                RearRideHeightMinBox.Value = RearPreviousValue;

                try { MainWindow.mw.m.WriteMemory(Addresses.RearRideHeightMin, "float", RearPreviousValue.ToString()); } catch { }
                try { MainWindow.mw.m.WriteMemory(Addresses.RearRestriction, "float", RearPreviousRestrictionValue.ToString()); } catch { }
            }
        }
    }
}
