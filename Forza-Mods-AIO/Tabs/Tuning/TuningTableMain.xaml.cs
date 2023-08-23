using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.TuningTablePort
{
    public partial class TuningTableMain : Page
    {
        public static TuningTableMain TBM;

        public TuningTableMain()
        {
            InitializeComponent();
            TBM = this;
            UpdateUi.UpdateUI(false, this);
        }

        #region Interaction
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