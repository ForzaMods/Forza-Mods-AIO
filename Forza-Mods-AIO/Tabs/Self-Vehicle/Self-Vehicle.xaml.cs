using Forza_Mods_AIO.Resources;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle
{
    /// <summary>
    /// Interaction logic for Self_Vehicle.xaml
    /// </summary>
    public partial class Self_Vehicle : Page
    {
        public static Self_Vehicle sv;
        Self_Vehicle_Addrs sva = new Self_Vehicle_Addrs();

        public Self_Vehicle()
        {
            InitializeComponent();
            sv = this;
            //UpdateUi.UpdateUI(false, this);
        }
        #region Buttons
        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            await sva.Scan();
            //UpdateUi.UpdateUI(true, this);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateUi.AnimCompleted)
            {
                UpdateUi.Animate(sender, UpdateUi.IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()], this);
                UpdateUi.IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()] = !UpdateUi.IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()];
            }
        }
        #endregion
    }
}
