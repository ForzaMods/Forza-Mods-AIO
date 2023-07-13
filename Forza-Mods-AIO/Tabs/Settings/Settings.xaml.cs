using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Keys = System.Windows.Forms.Keys;
using System.Windows.Media;

namespace Forza_Mods_AIO.Tabs.Settings
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private bool Clicked = false;
        public static bool Grabbing = false;
        public static Settings S;
        
        public Settings()
        {
            InitializeComponent();
            S = this;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            KeybindsHandling.UpdateKeybindingOnLaunch();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Clicked)
                return;

            Clicked = true;
            Grabbing = false;
            Button SenderButton = (Button)sender;
            SenderButton.Content = "Press some key";
            bool Reading = true;
            
            await Task.Run(() =>
            {
                while (Reading)
                {
                    Thread.Sleep(50);
                    if (!Grabbing)
                        KeybindsHandling.KeyGrabber(SenderButton);

                    Reading = false;
                }
            });

            Clicked = false;
        }

        private void ThemeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            string selectedOption = selectedItem.Content.ToString();
            var converter = new BrushConverter();

            switch (selectedOption)
            {
                case "Default":
                    ThemeSwitchHandling.MainColour = (Brush)converter.ConvertFromString("#FF111111");
                    ThemeSwitchHandling.DarkishColour = (Brush)converter.ConvertFromString("#FF101010");
                    ThemeSwitchHandling.DarkColour = (Brush)converter.ConvertFromString("#FF090909"); 
                    ThemeSwitchHandling.DarkerColour = (Brush)converter.ConvertFromString("#FF080808"); 
                    break;
                
                case "Nord":
                    break;
                
                case "Moss":
                    ThemeSwitchHandling.MainColour = (Brush)converter.ConvertFromString("#FF111111");
                    ThemeSwitchHandling.DarkishColour = (Brush)converter.ConvertFromString("#FF101010");
                    ThemeSwitchHandling.DarkColour = (Brush)converter.ConvertFromString("#FF090909"); 
                    ThemeSwitchHandling.DarkerColour = (Brush)converter.ConvertFromString("#FF080808"); 
                    break;
                
                case "Navy Blue":
                    ThemeSwitchHandling.MainColour = (Brush)converter.ConvertFromString("#0764B4"); 
                    ThemeSwitchHandling.DarkishColour = (Brush)converter.ConvertFromString("#065090"); 
                    ThemeSwitchHandling.DarkColour = (Brush)converter.ConvertFromString("#054378"); 
                    ThemeSwitchHandling.DarkerColour = (Brush)converter.ConvertFromString("#03325A"); 
                    break;
                
                case "Lavender":
                    ThemeSwitchHandling.MainColour = (Brush)converter.ConvertFromString("#774CAC"); 
                    ThemeSwitchHandling.DarkishColour = (Brush)converter.ConvertFromString("#5F3D8A"); 
                    ThemeSwitchHandling.DarkColour = (Brush)converter.ConvertFromString("#4F3373"); 
                    ThemeSwitchHandling.DarkerColour = (Brush)converter.ConvertFromString("#3C2656"); 
                    break;
                
                case "Lilac":
                    ThemeSwitchHandling.MainColour = (Brush)converter.ConvertFromString("#A5809C"); 
                    ThemeSwitchHandling.DarkishColour = (Brush)converter.ConvertFromString("#84667D"); 
                    ThemeSwitchHandling.DarkColour = (Brush)converter.ConvertFromString("#6E5568"); 
                    ThemeSwitchHandling.DarkerColour = (Brush)converter.ConvertFromString("#52404E"); 
                    break;
                
            }
            
            ThemeSwitchHandling.ApplyTheme();
        }
    }
}
