using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs
{
    /// <summary>
    /// Interaction logic for Alignment.xaml
    /// </summary>
    public partial class Alignment : Page
    {
        public Alignment()
        {
            InitializeComponent();
        }

        private void CamberNegBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.CamberNeg, "float", CamberNegBox.Value.ToString()); } catch { }
            try { MainWindow.mw.m.WriteMemory(Addresses.CamberNegStatic, "float", CamberNegBox.Value.ToString()); } catch { }
        }

        private void CamberPosBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.CamberPos, "float", CamberPosBox.Value.ToString()); } catch { }
            try { MainWindow.mw.m.WriteMemory(Addresses.CamberPosStatic, "float", CamberPosBox.Value.ToString()); } catch { }
        }

        private void ToeNegBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.ToeNeg, "float", ToeNegBox.Value.ToString()); } catch { }
            try { MainWindow.mw.m.WriteMemory(Addresses.ToeNegStatic, "float", ToeNegBox.Value.ToString()); } catch { }
        }

        private void ToePosBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.ToePos, "float", ToePosBox.Value.ToString()); } catch { }
            try { MainWindow.mw.m.WriteMemory(Addresses.ToePosStatic, "float", ToePosBox.Value.ToString()); } catch { }
        }
    }
}
