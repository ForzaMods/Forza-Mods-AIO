using System.Collections.Generic;
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
            if (!UpdateUi.AnimCompleted) return;
            UpdateUi.Animate(sender, IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()], Sizes, IsClicked, this);
            IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()] = !IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()];
        }
        
        private static readonly Dictionary<string, double> Sizes = new Dictionary<string, double>()
        {
            { "TiresButton" , 240 }, // Button name for page, height of page
            { "GearingButton" , 275 },
            { "AlignmentButton" , 120 },
            { "SpringsButton" , 295 },
            { "DampingButton" , 350 },
            { "AeroButton", 120 },
            { "SteeringButton", 275 },
            { "OthersButton", 395 },
        };

        private static Dictionary<string, bool> IsClicked = new Dictionary<string, bool>()
        {
            // Tuning
            {"TiresButton", false },
            {"GearingButton", false },
            {"AlignmentButton", false },
            {"SpringsButton", false },
            {"DampingButton", false },
            {"AeroButton", false },
            {"SteeringButton", false },
            {"OthersButton", false }
        };
        #endregion
    }
}