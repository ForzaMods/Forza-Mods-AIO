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

namespace Forza_Mods_AIO.Tabs
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class AutoShow : Page
    {
        public static AutoShow AS;
        AutoShowTab.AutoshowVars ASA = new AutoShowTab.AutoshowVars();
        public AutoShow()
        {
            InitializeComponent();
            AS = this;
        }

        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => ASA.Scan());
        }

        private void ToggleAllCars_Toggled(object sender, RoutedEventArgs e)
        {
            if (ToggleAllCars.IsOn)
            {

                ToggleRareCars.IsEnabled = false;
                if (MainWindow.mw.gvp.Name == "Forza Horizon 5")
                {
                    MainWindow.mw.m.WriteMemory(ASA.sql18, "string", "AND CanBuyNewCar(Garage.Id, \"0\")                              ");
                    MainWindow.mw.m.WriteMemory(ASA.sql19, "string", "CanBuyNewCar(Garage.Id, \"0\") AS PurchasableCar,                           ");
                }
                else
                {
                    MainWindow.mw.m.WriteMemory(ASA.sql5, "string", "                            ");

                }

                MainWindow.mw.m.WriteMemory(ASA.sql3, "string", "                  ");
                MainWindow.mw.m.WriteMemory(ASA.sql7, "string", "                                           ");
                MainWindow.mw.m.WriteMemory(ASA.sql1, "string", "    Garage.IsInstalled            AS PurchasableCar,");
                MainWindow.mw.m.WriteMemory(ASA.sql9, "string", "                                    ");
                MainWindow.mw.m.WriteMemory(ASA.sql8, "string", "           1215=");
                MainWindow.mw.m.WriteMemory(ASA.sql4, "string", "      1215=");
                MainWindow.mw.m.WriteMemory(ASA.sql17, "string", "                     ");


            }
            else if (!ToggleAllCars.IsOn)
            {
                ToggleRareCars.IsEnabled = true;
                if (MainWindow.mw.gvp.Name == "Forza Horizon 5")
                    MainWindow.mw.m.WriteMemory(ASA.sql18, "string", "AND CanBuyNewCar(Garage.Id, Garage.NotAvailableInAutoshow)    ");
                else
                {
                    MainWindow.mw.m.WriteMemory(ASA.sql5, "string", "AND NotAvailableInAutoshow=0");
                }
                MainWindow.mw.m.WriteMemory(ASA.sql3, "string", "AND NOT IsBarnFind");
                MainWindow.mw.m.WriteMemory(ASA.sql7, "string", "AND IsCarVisibleAndReleased(Garage.ModelId)");
                MainWindow.mw.m.WriteMemory(ASA.sql1, "string", "NOT Garage.NotAvailableInAutoshow AS PurchasableCar,");
                MainWindow.mw.m.WriteMemory(ASA.sql9, "string", "AND UnobtainableCars.Ordinal IS NULL");
                MainWindow.mw.m.WriteMemory(ASA.sql8, "string", "Garage.ModelId!=");
                MainWindow.mw.m.WriteMemory(ASA.sql4, "string", "Garage.Id!=");
                MainWindow.mw.m.WriteMemory(ASA.sql17, "string", "AND NOT IsMidnightCar");




            }
        }
    }
}
