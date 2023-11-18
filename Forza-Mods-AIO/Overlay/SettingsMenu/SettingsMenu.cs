using System;
using System.Collections.Generic;
using System.Windows.Media;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SettingsMenu
{
    public class SettingsMenu
    {
        // Header Option
        static MenuOption _headerImage = new("Header", OptionType.Int, 1);

        #region Background options

        private static readonly MenuOption MainBackgroundR = new("Background R", OptionType.Int, 0);
        private static readonly MenuOption MainBackgroundG = new("Background G", OptionType.Int, 0);
        private static readonly MenuOption MainBackgroundB = new("Background B", OptionType.Int, 0);
        private static readonly MenuOption MainBackgroundA = new("Background Alpha", OptionType.Int, 120);

        private static readonly MenuOption DescriptionBackgroundR = new("Background R", OptionType.Int, 0);
        private static readonly MenuOption DescriptionBackgroundG = new("Background G", OptionType.Int, 0);
        private static readonly MenuOption DescriptionBackgroundB = new("Background B", OptionType.Int, 0);
        private static readonly MenuOption DescriptionBackgroundA = new("Background Alpha", OptionType.Int, 120);

        #endregion

        #region Border options

        private static readonly MenuOption MainBorderR = new("Border R", OptionType.Int, 0);
        private static readonly MenuOption MainBorderG = new("Border G", OptionType.Int, 0);
        private static readonly MenuOption MainBorderB = new("Border B", OptionType.Int, 0);
        private static readonly MenuOption MainBorderA = new("Border Alpha", OptionType.Int, 255);

        private static readonly MenuOption DescriptionBorderR = new("Border R", OptionType.Int, 0);
        private static readonly MenuOption DescriptionBorderG = new("Border G", OptionType.Int, 0);
        private static readonly MenuOption DescriptionBorderB = new("Border B", OptionType.Int, 0);
        private static readonly MenuOption DescriptionBorderA = new("Border Alpha", OptionType.Int, 255);
        public static readonly MenuOption LoadSettings = new("Load Settings", OptionType.Button, delegate { Oh.LoadSettings(); }, isEnabled: false);


        #endregion

        // Subscribes menu options to event handlers

        #region Eventhandlers and similar stuff
        public void InitiateSubMenu()
        {
            _headerImage.ValueChangedHandler += HeaderValueChanged;

            MainBackgroundR.ValueChangedHandler += ColourValueChanged;
            MainBackgroundG.ValueChangedHandler += ColourValueChanged;
            MainBackgroundB.ValueChangedHandler += ColourValueChanged;
            MainBackgroundA.ValueChangedHandler += ColourValueChanged;

            DescriptionBackgroundR.ValueChangedHandler += ColourValueChanged;
            DescriptionBackgroundG.ValueChangedHandler += ColourValueChanged;
            DescriptionBackgroundB.ValueChangedHandler += ColourValueChanged;
            DescriptionBackgroundA.ValueChangedHandler += ColourValueChanged;

            MainBorderR.ValueChangedHandler += ColourValueChanged;
            MainBorderG.ValueChangedHandler += ColourValueChanged;
            MainBorderB.ValueChangedHandler += ColourValueChanged;
            MainBorderA.ValueChangedHandler += ColourValueChanged;

            DescriptionBorderR.ValueChangedHandler += ColourValueChanged;
            DescriptionBorderG.ValueChangedHandler += ColourValueChanged;
            DescriptionBorderB.ValueChangedHandler += ColourValueChanged;
            DescriptionBorderA.ValueChangedHandler += ColourValueChanged;

            Overlay.o.Dispatcher.Invoke(() =>
            {
                Oh.MainBackColour = new SolidColorBrush(Color.FromArgb(120, 0, 0, 0));
                Oh.DescriptionBackColour = new SolidColorBrush(Color.FromArgb(120, 0, 0, 0));
            });
        }

        // Event handlers
        void ColourValueChanged(object s, EventArgs e)
        {
            if ((int)s.GetType().GetProperty("Value").GetValue(s) < 0)
                s.GetType().GetProperty("Value").SetValue(s, 255);
            else if ((int)s.GetType().GetProperty("Value").GetValue(s) > 255)
                s.GetType().GetProperty("Value").SetValue(s, 0);
            Overlay.o.Dispatcher.Invoke(() =>
            {
                if (Oh.CurrentMenu.Contains("MainArea"))
                {
                    Oh.MainBackColour = new SolidColorBrush(Color.FromArgb(
                        Convert.ToByte(MainBackgroundA.Value),
                        Convert.ToByte(MainBackgroundR.Value),
                        Convert.ToByte(MainBackgroundG.Value),
                        Convert.ToByte(MainBackgroundB.Value)));
                    Oh.MainBorderColour = new SolidColorBrush(Color.FromArgb(
                        Convert.ToByte(MainBorderA.Value),
                        Convert.ToByte(MainBorderR.Value),
                        Convert.ToByte(MainBorderG.Value),
                        Convert.ToByte(MainBorderB.Value)));
                }
                else
                {
                    Oh.DescriptionBackColour = new SolidColorBrush(Color.FromArgb(
                        Convert.ToByte(DescriptionBackgroundA.Value),
                        Convert.ToByte(DescriptionBackgroundR.Value),
                        Convert.ToByte(DescriptionBackgroundG.Value),
                        Convert.ToByte(DescriptionBackgroundB.Value)));
                    Oh.DescriptionBorderColour = new SolidColorBrush(Color.FromArgb(
                        Convert.ToByte(DescriptionBorderA.Value),
                        Convert.ToByte(DescriptionBorderR.Value),
                        Convert.ToByte(DescriptionBorderG.Value),
                        Convert.ToByte(DescriptionBorderB.Value)));
                }
            });
        }

        private static void HeaderValueChanged(object s, EventArgs e)
        {
            var headerCount = Oh.Headers.Count;
            
            if ((int)s.GetType().GetProperty("Value")?.GetValue(s)! < 1)
            {
                s.GetType().GetProperty("Value")?.SetValue(s, headerCount);
            }
            else if ((int)s.GetType().GetProperty("Value")?.GetValue(s)! > headerCount)
            {
                s.GetType().GetProperty("Value")?.SetValue(s, 1);
            }
            
            if (headerCount == 0)
            {
                return;
            }

            Oh.HeaderIndex = (int)s.GetType().GetProperty("Value")?.GetValue(s)! - 1;
        }
        
        
        #endregion
        
        // Menu list for this section
        public static readonly List<MenuOption> SettingsOptions = new()
        {
            _headerImage,
            new("Refresh Headers", OptionType.Button, delegate { Oh.CacheHeaders(); }),
            new("Main area", OptionType.MenuButton),
            new("Description area", OptionType.MenuButton),
            new("Save Settings", OptionType.Button, delegate { Oh.SaveSettings(); LoadSettings.IsEnabled = true; }, isEnabled: false),
            LoadSettings
        };

        // Submenu lists
        public static readonly List<MenuOption> MainAreaOptions = new()
        {
            new ("Background", OptionType.SubHeader),
            MainBackgroundR,
            MainBackgroundG,
            MainBackgroundB,
            MainBackgroundA,
            new ("Border", OptionType.SubHeader),
            MainBorderR,
            MainBorderG,
            MainBorderB,
            MainBorderA
        };
        public static readonly List<MenuOption> DescriptionAreaOptions = new()
        {
            new ("Background", OptionType.SubHeader),
            DescriptionBackgroundR,
            DescriptionBackgroundG,
            DescriptionBackgroundB,
            DescriptionBackgroundA,
            new ("Border", OptionType.SubHeader),
            DescriptionBorderR,
            DescriptionBorderG,
            DescriptionBorderB,
            DescriptionBorderA
        };
    }
}
