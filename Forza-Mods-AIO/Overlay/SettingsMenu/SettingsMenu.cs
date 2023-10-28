using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Forza_Mods_AIO.Overlay.SettingsMenu
{
    public class SettingsMenu
    {
        // Header Option
        static Overlay.MenuOption HeaderImage = new Overlay.MenuOption("Header", Overlay.MenuOption.OptionType.Int, 1);

        #region Background options

        public static Overlay.MenuOption MainBackgroundR = new Overlay.MenuOption("Background R", Overlay.MenuOption.OptionType.Int, 0);
        public static Overlay.MenuOption MainBackgroundG = new Overlay.MenuOption("Background G", Overlay.MenuOption.OptionType.Int, 0);
        public static Overlay.MenuOption MainBackgroundB = new Overlay.MenuOption("Background B", Overlay.MenuOption.OptionType.Int, 0);
        public static Overlay.MenuOption MainBackgroundA = new Overlay.MenuOption("Background Alpha", Overlay.MenuOption.OptionType.Int, 120);
        
        public static Overlay.MenuOption DescriptionBackgroundR = new Overlay.MenuOption("Background R", Overlay.MenuOption.OptionType.Int, 0);
        public static Overlay.MenuOption DescriptionBackgroundG = new Overlay.MenuOption("Background G", Overlay.MenuOption.OptionType.Int, 0);
        public static Overlay.MenuOption DescriptionBackgroundB = new Overlay.MenuOption("Background B", Overlay.MenuOption.OptionType.Int, 0);
        public static Overlay.MenuOption DescriptionBackgroundA = new Overlay.MenuOption("Background Alpha", Overlay.MenuOption.OptionType.Int, 120);

        #endregion

        #region Border options

        public static Overlay.MenuOption MainBorderR = new Overlay.MenuOption("Border R", Overlay.MenuOption.OptionType.Int, 0);
        public static Overlay.MenuOption MainBorderG = new Overlay.MenuOption("Border G", Overlay.MenuOption.OptionType.Int, 0);
        public static Overlay.MenuOption MainBorderB = new Overlay.MenuOption("Border B", Overlay.MenuOption.OptionType.Int, 0);
        public static Overlay.MenuOption MainBorderA = new Overlay.MenuOption("Border Alpha", Overlay.MenuOption.OptionType.Int, 255);

        public static Overlay.MenuOption DescriptionBorderR = new Overlay.MenuOption("Border R", Overlay.MenuOption.OptionType.Int, 0);
        public static Overlay.MenuOption DescriptionBorderG = new Overlay.MenuOption("Border G", Overlay.MenuOption.OptionType.Int, 0);
        public static Overlay.MenuOption DescriptionBorderB = new Overlay.MenuOption("Border B", Overlay.MenuOption.OptionType.Int, 0);
        public static Overlay.MenuOption DescriptionBorderA = new Overlay.MenuOption("Border Alpha", Overlay.MenuOption.OptionType.Int, 255);
        public static Overlay.MenuOption LoadSettings = new("Load Settings", Overlay.MenuOption.OptionType.Button, delegate { Overlay.oh.LoadSettings(); }, isEnabled: false);


        #endregion

        // Subscribes menu options to event handlers

        #region Eventhandlers and similar stuff
        public void InitiateSubMenu()
        {
            HeaderImage.ValueChangedHandler += new EventHandler(HeaderValueChanged);

            MainBackgroundR.ValueChangedHandler += new EventHandler(ColourValueChanged);
            MainBackgroundG.ValueChangedHandler += new EventHandler(ColourValueChanged);
            MainBackgroundB.ValueChangedHandler += new EventHandler(ColourValueChanged);
            MainBackgroundA.ValueChangedHandler += new EventHandler(ColourValueChanged);

            DescriptionBackgroundR.ValueChangedHandler += new EventHandler(ColourValueChanged);
            DescriptionBackgroundG.ValueChangedHandler += new EventHandler(ColourValueChanged);
            DescriptionBackgroundB.ValueChangedHandler += new EventHandler(ColourValueChanged);
            DescriptionBackgroundA.ValueChangedHandler += new EventHandler(ColourValueChanged);

            MainBorderR.ValueChangedHandler += new EventHandler(ColourValueChanged);
            MainBorderG.ValueChangedHandler += new EventHandler(ColourValueChanged);
            MainBorderB.ValueChangedHandler += new EventHandler(ColourValueChanged);
            MainBorderA.ValueChangedHandler += new EventHandler(ColourValueChanged);

            DescriptionBorderR.ValueChangedHandler += new EventHandler(ColourValueChanged);
            DescriptionBorderG.ValueChangedHandler += new EventHandler(ColourValueChanged);
            DescriptionBorderB.ValueChangedHandler += new EventHandler(ColourValueChanged);
            DescriptionBorderA.ValueChangedHandler += new EventHandler(ColourValueChanged);

            Overlay.o.Dispatcher.Invoke(() =>
            {
                Overlay.oh.MainBackColour = new SolidColorBrush(Color.FromArgb(120, 0, 0, 0));
                Overlay.oh.DescriptionBackColour = new SolidColorBrush(Color.FromArgb(120, 0, 0, 0));
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
                if (Overlay.oh.CurrentMenu.Contains("MainArea"))
                {
                    Overlay.oh.MainBackColour = new SolidColorBrush(Color.FromArgb(
                        Convert.ToByte(MainBackgroundA.Value),
                        Convert.ToByte(MainBackgroundR.Value),
                        Convert.ToByte(MainBackgroundG.Value),
                        Convert.ToByte(MainBackgroundB.Value)));
                    Overlay.oh.MainBorderColour = new SolidColorBrush(Color.FromArgb(
                        Convert.ToByte(MainBorderA.Value),
                        Convert.ToByte(MainBorderR.Value),
                        Convert.ToByte(MainBorderG.Value),
                        Convert.ToByte(MainBorderB.Value)));
                }
                else
                {
                    Overlay.oh.DescriptionBackColour = new SolidColorBrush(Color.FromArgb(
                        Convert.ToByte(DescriptionBackgroundA.Value),
                        Convert.ToByte(DescriptionBackgroundR.Value),
                        Convert.ToByte(DescriptionBackgroundG.Value),
                        Convert.ToByte(DescriptionBackgroundB.Value)));
                    Overlay.oh.DescriptionBorderColour = new SolidColorBrush(Color.FromArgb(
                        Convert.ToByte(DescriptionBorderA.Value),
                        Convert.ToByte(DescriptionBorderR.Value),
                        Convert.ToByte(DescriptionBorderG.Value),
                        Convert.ToByte(DescriptionBorderB.Value)));
                }
            });
        }
        void HeaderValueChanged(object s, EventArgs e)
        {
            int HeaderCount = Overlay.oh.Headers.Count;
            //int HeaderCount = Directory.GetFiles(Environment.CurrentDirectory + @"\Headers").Length;
            if ((int)s.GetType().GetProperty("Value").GetValue(s) < 1)
                s.GetType().GetProperty("Value").SetValue(s, HeaderCount);
            if ((int)s.GetType().GetProperty("Value").GetValue(s) > HeaderCount)
                s.GetType().GetProperty("Value").SetValue(s, 1);
            Overlay.oh.HeaderIndex = (int)s.GetType().GetProperty("Value").GetValue(s) - 1;
        }
        
        
        #endregion
        
        // Menu list for this section
        public static readonly List<Overlay.MenuOption> SettingsOptions = new()
        {
            HeaderImage,
            new("Refresh Headers", Overlay.MenuOption.OptionType.Button, delegate { Overlay.oh.CacheHeaders(); }),
            new("Main area", Overlay.MenuOption.OptionType.MenuButton),
            new("Description area", Overlay.MenuOption.OptionType.MenuButton),
            new("Save Settings", Overlay.MenuOption.OptionType.Button, delegate { Overlay.oh.SaveSettings(); LoadSettings.IsEnabled = true; }, isEnabled: false),
            LoadSettings
        };

        // Submenu lists
        public static List<Overlay.MenuOption> MainAreaOptions = new()
        {
            new ("Background", Overlay.MenuOption.OptionType.SubHeader),
            MainBackgroundR,
            MainBackgroundG,
            MainBackgroundB,
            MainBackgroundA,
            new ("Border", Overlay.MenuOption.OptionType.SubHeader),
            MainBorderR,
            MainBorderG,
            MainBorderB,
            MainBorderA
        };
        public static List<Overlay.MenuOption> DescriptionAreaOptions = new()
        {
            new ("Background", Overlay.MenuOption.OptionType.SubHeader),
            DescriptionBackgroundR,
            DescriptionBackgroundG,
            DescriptionBackgroundB,
            DescriptionBackgroundA,
            new ("Border", Overlay.MenuOption.OptionType.SubHeader),
            DescriptionBorderR,
            DescriptionBorderG,
            DescriptionBorderB,
            DescriptionBorderA
        };
    }
}
