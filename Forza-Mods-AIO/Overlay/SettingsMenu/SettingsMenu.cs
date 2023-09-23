using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Threading;

namespace Forza_Mods_AIO.Overlay.SettingsMenu
{
    public class SettingsMenu
    {
        // Header Option
        static Overlay.MenuOption HeaderImage = new Overlay.MenuOption("Header", "Int", 1);

        #region Background options

        static Overlay.MenuOption MainBackgroundR = new Overlay.MenuOption("Background R", "Int", 255);
        static Overlay.MenuOption MainBackgroundG = new Overlay.MenuOption("Background G", "Int", 255);
        static Overlay.MenuOption MainBackgroundB = new Overlay.MenuOption("Background B", "Int", 255);
        static Overlay.MenuOption MainBackgroundA = new Overlay.MenuOption("Background Alpha", "Int", 0);

        static Overlay.MenuOption DescriptionBackgroundR = new Overlay.MenuOption("Background R", "Int", 255);
        static Overlay.MenuOption DescriptionBackgroundG = new Overlay.MenuOption("Background G", "Int", 255);
        static Overlay.MenuOption DescriptionBackgroundB = new Overlay.MenuOption("Background B", "Int", 255);
        static Overlay.MenuOption DescriptionBackgroundA = new Overlay.MenuOption("Background Alpha", "Int", 0);

        #endregion

        #region Border options

        static Overlay.MenuOption MainBorderR = new Overlay.MenuOption("Border R", "Int", 0);
        static Overlay.MenuOption MainBorderG = new Overlay.MenuOption("Border G", "Int", 0);
        static Overlay.MenuOption MainBorderB = new Overlay.MenuOption("Border B", "Int", 0);
        static Overlay.MenuOption MainBorderA = new Overlay.MenuOption("Border Alpha", "Int", 255);

        static Overlay.MenuOption DescriptionBorderR = new Overlay.MenuOption("Border R", "Int", 0);
        static Overlay.MenuOption DescriptionBorderG = new Overlay.MenuOption("Border G", "Int", 0);
        static Overlay.MenuOption DescriptionBorderB = new Overlay.MenuOption("Border B", "Int", 0);
        static Overlay.MenuOption DescriptionBorderA = new Overlay.MenuOption("Border Alpha", "Int", 255);

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
        }

        // Event handlers
        void ColourValueChanged(object s, EventArgs e)
        {
            if ((int)s.GetType().GetProperty("Value").GetValue(s) < 0)
                s.GetType().GetProperty("Value").SetValue(s, 255);
            if ((int)s.GetType().GetProperty("Value").GetValue(s) > 255)
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
        public static List<Overlay.MenuOption> SettingsOptions = new List<Overlay.MenuOption>()
        {
            HeaderImage,
            new Overlay.MenuOption("Refresh Headers", "Button", new Action(delegate { Overlay.oh.CacheHeaders(); })),
            new Overlay.MenuOption("Main area", "MenuButton"),
            new Overlay.MenuOption("Description area", "MenuButton"),
            new Overlay.MenuOption("Save Settings", "Button", SaveSettings)
        };


        private static void SaveSettings()
        {
            
        }

        // Submenu lists
        public static List<Overlay.MenuOption> MainAreaOptions = new List<Overlay.MenuOption>()
        {
            new Overlay.MenuOption("Background", "SubHeader"),
            MainBackgroundR,
            MainBackgroundG,
            MainBackgroundB,
            MainBackgroundA,
            new Overlay.MenuOption("Border", "SubHeader"),
            MainBorderR,
            MainBorderG,
            MainBorderB,
            MainBorderA
        };
        public static List<Overlay.MenuOption> DescriptionAreaOptions = new List<Overlay.MenuOption>()
        {
            new Overlay.MenuOption("Background", "SubHeader"),
            DescriptionBackgroundR,
            DescriptionBackgroundG,
            DescriptionBackgroundB,
            DescriptionBackgroundA,
            new Overlay.MenuOption("Border", "SubHeader"),
            DescriptionBorderR,
            DescriptionBorderG,
            DescriptionBorderB,
            DescriptionBorderA
        };
    }
}
