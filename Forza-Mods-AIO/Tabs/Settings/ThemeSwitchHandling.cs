using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using System.Windows.Media;
using ControlzEx.Theming;

namespace Forza_Mods_AIO.Tabs.Settings;

public class ThemeSwitchHandling
{
    public static Brush MainColour = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF111111"));
    public static Brush DarkishColour = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF101010"));
    public static Brush DarkColour = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF090909"));
    public static Brush DarkerColour = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF080808"));
    
    public static void ApplyTheme()
    {
        if (MainWindow.mw != null)
        {
            ThemeManager.Current.ClearThemes();
            
            MainWindow.mw.Background.Background = MainColour;
            MainWindow.mw.FrameBorder.Background = MainColour;
            MainWindow.mw.SideBar.Background = DarkishColour;
            MainWindow.mw.TopBar1.Background = DarkColour;
            MainWindow.mw.TopBar2.Background = DarkColour;
            
            MainWindow.mw.CategoryButton_Click(new Object(), new RoutedEventArgs());
            foreach (FrameworkElement Element in MainWindow.mw.Window.GetChildren(true))
            {
                if (Element.GetType() == typeof(System.Windows.Controls.Button))
                {
                    Element.GetType().GetProperty("Background").SetValue(Element, DarkerColour);
                    Element.GetType().GetProperty("BorderBrush").SetValue(Element, DarkerColour);
                }

                if (Element.GetType() == typeof(System.Windows.Controls.Border))
                {
                    Element.GetType().GetProperty("BorderBrush").SetValue(Element, DarkerColour);
                }

                if (Element.GetType() == typeof(NumericUpDown))
                {
                    Element.GetType().GetProperty("Background").SetValue(Element, DarkerColour);
                    Element.GetType().GetProperty("BorderBrush").SetValue(Element, DarkerColour);
                }

                if (Element.GetType() == typeof(System.Windows.Controls.ComboBox))
                {
                    Element.GetType().GetProperty("Background").SetValue(Element, DarkerColour);
                    Element.GetType().GetProperty("BorderBrush").SetValue(Element, DarkerColour);
                }

                if (Element.GetType() == typeof(ListBox))
                {
                    Element.GetType().GetProperty("Background").SetValue(Element, DarkerColour);
                    Element.GetType().GetProperty("BorderBrush").SetValue(Element, DarkerColour);
                }

                if (Element.GetType() == typeof(ListBoxItem))
                {
                    Element.GetType().GetProperty("Background").SetValue(Element, MainColour);
                    Element.GetType().GetProperty("BorderBrush").SetValue(Element, MainColour);
                }

                if (Element.GetType() == typeof(ComboBoxItem))
                {
                    Element.GetType().GetProperty("Background").SetValue(Element, MainColour);
                    Element.GetType().GetProperty("BorderBrush").SetValue(Element, MainColour);
                }

                if (Element.GetType() == typeof(MetroProgressBar))
                {
                    Element.GetType().GetProperty("Background").SetValue(Element, DarkerColour);
                    Element.GetType().GetProperty("BorderBrush").SetValue(Element, DarkerColour);
                }
            }
        }
    }
}