using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Tabs.Tuning
{
    public partial class Tuning : Page
    {
        public static Tuning TBM;

        public Tuning()
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