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

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs
{
    public partial class TeleportsPage : Page
    {
        public TeleportsPage()
        {
            InitializeComponent();
        }

        private void TeleportBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            var selectedOption = selectedItem.Content.ToString();


        }

        private void TeleportBoxFH5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            var selectedOption = selectedItem.Content.ToString();

            
        }

        private void AutoTpToWaypoint_Toggled(object sender, RoutedEventArgs e)
        {
            if(AutoTpToWaypoint.IsOn)
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
