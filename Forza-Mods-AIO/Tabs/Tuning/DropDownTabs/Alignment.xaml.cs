using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs
{
    public partial class Alignment : Page
    {
        public static Alignment Al;
        public Alignment()
        {
            InitializeComponent();
            Al = this;
        }

        private void CamberNegBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.Mw.M.WriteMemory(TuningAddresses.CamberNeg, (float)CamberNegBox.Value); } catch { }
            try { MainWindow.Mw.M.WriteMemory(TuningAddresses.CamberNegStatic, (float)CamberNegBox.Value); } catch { }
        }

        private void CamberPosBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.Mw.M.WriteMemory(TuningAddresses.CamberPos, (float)CamberPosBox.Value); } catch { }
            try { MainWindow.Mw.M.WriteMemory(TuningAddresses.CamberPosStatic, (float)CamberPosBox.Value); } catch { }
        }

        private void ToeNegBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.Mw.M.WriteMemory(TuningAddresses.ToeNeg, (float)ToeNegBox.Value); } catch { }
            try { MainWindow.Mw.M.WriteMemory(TuningAddresses.ToeNegStatic, (float)ToeNegBox.Value); } catch { }
        }

        private void ToePosBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.Mw.M.WriteMemory(TuningAddresses.ToePos, (float)ToePosBox.Value); } catch { }
            try { MainWindow.Mw.M.WriteMemory(TuningAddresses.ToePosStatic, (float)ToePosBox.Value); } catch { }
        }
    }
}
