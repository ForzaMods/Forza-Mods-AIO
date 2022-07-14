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

                MainWindow.mw.m.WriteMemory(ASA.sql1, "string", "    Garage.IsInstalled            AS PurchasableCar,");
                MainWindow.mw.m.WriteMemory(ASA.sql3, "string", "                  ");
                MainWindow.mw.m.WriteMemory(ASA.sql4, "string", "      1215=");
                MainWindow.mw.m.WriteMemory(ASA.sql5, "string", "                            ");
                MainWindow.mw.m.WriteMemory(ASA.sql7, "string", "                                           ");
                MainWindow.mw.m.WriteMemory(ASA.sql8, "string", "           1215=");
                MainWindow.mw.m.WriteMemory(ASA.sql9, "string", "                                    ");
                MainWindow.mw.m.WriteMemory(ASA.sql17, "string", "                     ");
                MainWindow.mw.m.WriteMemory(ASA.sql18, "string", "AND CanBuyNewCar(Garage.Id, \"0\")                              ");
                MainWindow.mw.m.WriteMemory(ASA.sql19, "string", "CanBuyNewCar(Garage.Id, \"0\") AS PurchasableCar,                           ");
            }
            else if (!ToggleAllCars.IsOn)
            {
                ToggleRareCars.IsEnabled = true;

                MainWindow.mw.m.WriteMemory(ASA.sql1, "string", "NOT Garage.NotAvailableInAutoshow AS PurchasableCar,");
                MainWindow.mw.m.WriteMemory(ASA.sql3, "string", "AND NOT IsBarnFind");
                MainWindow.mw.m.WriteMemory(ASA.sql4, "string", "Garage.Id!=");
                MainWindow.mw.m.WriteMemory(ASA.sql5, "string", "AND NotAvailableInAutoshow=0");
                MainWindow.mw.m.WriteMemory(ASA.sql7, "string", "AND IsCarVisibleAndReleased(Garage.ModelId)");
                MainWindow.mw.m.WriteMemory(ASA.sql8, "string", "Garage.ModelId!=");
                MainWindow.mw.m.WriteMemory(ASA.sql9, "string", "AND UnobtainableCars.Ordinal IS NULL");
                MainWindow.mw.m.WriteMemory(ASA.sql17, "string", "AND NOT IsMidnightCar");
                MainWindow.mw.m.WriteMemory(ASA.sql18, "string", "AND CanBuyNewCar(Garage.Id, Garage.NotAvailableInAutoshow)    ");
                MainWindow.mw.m.WriteMemory(ASA.sql19, "string", "CanBuyNewCar(Garage.Id, Garage.NotAvailableInAutoshow) AS PurchasableCar,   ");
            }
        }

        private void ToggleRareCars_Toggled(object sender, RoutedEventArgs e)
        {
            if (ToggleRareCars.IsOn)
            {
                ToggleAllCars.IsEnabled = false;

                if (MainWindow.mw.gvp.Name == "ForzaHorizon5")
                {
                    MainWindow.mw.m.WriteMemory(ASA.sql18, "string", "AND CanBuyNewCar(Garage.Id, \"0\")                              ");
                    MainWindow.mw.m.WriteMemory(ASA.sql19, "string", "CanBuyNewCar(Garage.Id, \"0\") AS PurchasableCar,                           ");
                }
            }



            /*if (Box_RareCars.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_RareCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);

                Box_AllCars.Enabled = false;
                if (MainWindow.main.platform == 4 || MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory(sql18, "string", "AND NOT CanBuyNewCar(Garage.Id, Garage.NotAvailableInAutoshow)");
                    MainWindow.m.WriteMemory(sql19, "string", "CanBuyNewCar(Garage.Id, \"0\") AS PurchasableCar,                           ");
                }
                else
                {
                    MainWindow.m.WriteMemory(sql6, "string", "=1                                    ");

                }

                MainWindow.m.WriteMemory(sql3, "string", "                  ");
                MainWindow.m.WriteMemory(sql1, "string", "    Garage.IsInstalled            AS PurchasableCar,");
                MainWindow.m.WriteMemory(sql9, "string", "                                    ");
                MainWindow.m.WriteMemory(sql8, "string", "           1215=");
                MainWindow.m.WriteMemory(sql4, "string", "      1215=");
                if (sql17 != "0")
                    MainWindow.m.WriteMemory(sql17, "string", "                     ");

            }
            else if (!Box_RareCars.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_RareCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_AllCars.Enabled = true;
                if (MainWindow.main.platform == 4 || MainWindow.main.platform == 5)
                    MainWindow.m.WriteMemory(sql18, "string", "AND CanBuyNewCar(Garage.Id, Garage.NotAvailableInAutoshow)    ");
                else
                {
                    MainWindow.m.WriteMemory(sql6, "string", "=0                                    ");
                }

                MainWindow.m.WriteMemory(sql3, "string", "AND NOT IsBarnFind");
                MainWindow.m.WriteMemory(sql1, "string", "NOT Garage.NotAvailableInAutoshow AS PurchasableCar,");
                MainWindow.m.WriteMemory(sql9, "string", "AND UnobtainableCars.Ordinal IS NULL");
                MainWindow.m.WriteMemory(sql8, "string", "Garage.ModelId!=");
                MainWindow.m.WriteMemory(sql4, "string", "Garage.Id!=");
                if (sql17 != "0")
                    MainWindow.m.WriteMemory(sql17, "string", "AND NOT IsMidnightCar");
            }*/
        }
    }
}
