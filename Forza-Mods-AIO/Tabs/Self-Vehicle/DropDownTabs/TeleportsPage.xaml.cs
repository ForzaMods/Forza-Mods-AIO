using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs
{
    public partial class TeleportsPage : Page
    {
        public static TeleportsPage t;

        public TeleportsPage()
        {
            InitializeComponent();
            t = this;
        }

        private void TeleportBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            var selectedOption = selectedItem.Content.ToString();

            float x = 0;
            float y = 0;
            float z = 0;

            switch (selectedOption)
            {
                #region FH4
                case "Adventure Park":
                    x = (float)2267.335449;
                    y = (float)304.2393494;
                    z = (float)-2611.638428;
                    break;
                case "Ambleside":
                    x = (float)-5112.047363;
                    y = (float)154.1546478;
                    z = (float)-3534.503906;
                    break;
                case "Beach":
                    x = (float)4874.382812;
                    y = (float)124.9019775;
                    z = (float)-1392.215454;
                    break;
                case "Broadway":
                    x = (float)-237.2871857;
                    y = (float)239.5045471;
                    z = (float)-5816.858398;
                    break;
                case "Dam":
                    x = (float)-854.6953125;
                    y = (float)209.1066284;
                    z = (float)-2031.137329;
                    break;
                case "Edinburgh":
                    x = (float)2045.383179;
                    y = (float)204.0559845;
                    z = (float)2511.078613;
                    break;
                case "Festival":
                    x = (float)-2753.350098;
                    y = (float)349.7218018;
                    z = (float)-4357.629883;
                    break;
                case "Greendale Airstrip":
                    x = (float)3409.570068;
                    y = (float)159.2418976;
                    z = (float)661.2498779;
                    break;
                case "Lake Island":
                    x = (float)-4001.890869;
                    y = (float)175.7261353;
                    z = (float)-196.6170197;
                    break;
                case "Mortimer Gardens":
                    x = (float)-4314.36377;
                    y = (float)153.261261;
                    z = (float)1804.139282;
                    break;
                case "Quarry":
                    x = (float)-1569.987305;
                    y = (float)206.0023804;
                    z = (float)-2843.05249;
                    break;
                case "Railyard":
                    x = (float)-935.0923462;
                    y = (float)161.055069;
                    z = (float)1745.383667;
                    break;
                case "Start of Motorway":
                    x = (float)2657.887451;
                    y = (float)270.7128906;
                    z = (float)-4353.087402;
                    break;
                case "Top of Mountain":
                    x = (float)-2285.739746;
                    y = (float)364.6417236;
                    z = (float)2576.946533;
                    break;
                #endregion
                #region FH5
                case "Top Of Volcano":
                    x = (float)-5594.330078;
                    y = (float)1023.229919;
                    z = (float)2392.037109;
                    break;
                case "Stadium":
                    x = (float)-762.8079834;
                    y = (float)169.0338593;
                    z = (float)1615.112183;
                    break;
                case "Guanajuato (Main City)":
                    x = (float)355.9811096;
                    y = (float)258.8370056;
                    z = (float)3135.321533;
                    break;
                case "Bridge":
                    x = (float)-5820.825684;
                    y = (float)122.3475876;
                    z = (float)-2550.383545;
                    break;
                case "Golf Course":
                    x = (float)-8316.630859;
                    y = (float)125.8156357;
                    z = (float)-1150.103271;
                    break;
                case "Dunes":
                    x = (float)-8615.027344;
                    y = (float)143.9117279;
                    z = (float)1966.912109;
                    break;
                case "Motorway":
                    x = (float)2855.958252;
                    y = (float)195.1608429;
                    z = (float)1465.902954;
                    break;
                case "Airstrip":
                    x = (float)-3891.084717;
                    y = (float)174.4389496;
                    z = (float)-3841.428467;
                    break;
                case "Mulege":
                    x = (float)-4174.963867;
                    y = (float)122.9130783;
                    z = (float)-2227.120605;
                    break;
                case "Temple":
                    x = (float)3643.609375;
                    y = (float)230.227066;
                    z = (float)-2646.405029;
                    break;
                case "River":
                    x = (float)923.4258423;
                    y = (float)246.7331696;
                    z = (float)-2980.020264;
                    break;
                case "Dirt Circuit":
                    x = (float)-8344.927734;
                    y = (float)200.0671387;
                    z = (float)3197.32959;
                    break;
                case "Pllaya Azul":
                    x = (float)5550.070801;
                    y = (float)105.1047897;
                    z = (float)497.8027649;
                    break;
                case "Temple Drag":
                    x = (float)751.5328979;
                    y = (float)190.3298645;
                    z = (float)-110.3424072;
                    break;
                    #endregion
            }
            
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.xAddr, "float", x.ToString());
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.yAddr, "float", y.ToString());
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.zAddr, "float", z.ToString());
        }

        private void AutoTpToWaypoint_Toggled(object sender, RoutedEventArgs e)
        {
            if (AutoTpToWaypoint.IsOn)
            {

            }

            else
            {

            }
        }

        private void BypassOutOfBounds_Toggled(object sender, RoutedEventArgs e)
        {
            if (BypassOutOfBounds.IsOn)
            {

            }
            else
            {



            }
        }
    }
}
