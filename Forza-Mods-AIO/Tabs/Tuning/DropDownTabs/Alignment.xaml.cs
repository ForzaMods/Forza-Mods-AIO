using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs
{
    public partial class Alignment : Page
    {
        public static Alignment al;
        public Alignment()
        {
            InitializeComponent();
            al = this;
        }

        private void CamberNegBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.CamberNeg, "float", CamberNegBox.Value.ToString()); } catch { }
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.CamberNegStatic, "float", CamberNegBox.Value.ToString()); } catch { }
        }

        private void CamberPosBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.CamberPos, "float", CamberPosBox.Value.ToString()); } catch { }
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.CamberPosStatic, "float", CamberPosBox.Value.ToString()); } catch { }
        }

        private void ToeNegBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.ToeNeg, "float", ToeNegBox.Value.ToString()); } catch { }
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.ToeNegStatic, "float", ToeNegBox.Value.ToString()); } catch { }
        }

        private void ToePosBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.ToePos, "float", ToePosBox.Value.ToString()); } catch { }
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.ToePosStatic, "float", ToePosBox.Value.ToString()); } catch { }
        }
    }
}
