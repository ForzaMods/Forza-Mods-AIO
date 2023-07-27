using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Timer = System.Windows.Forms.Timer;

namespace Forza_Mods_AIO.Overlay
{
    /// <summary>
    /// Interaction logic for Overlay.xaml
    /// </summary>
    public partial class Overlay : Window
    {
        public class MenuOption
        {
            public string Name { get; }
            public string Type { get; }
            public object Value
            {
                get => _value;
                set
                {
                    _value = value;
                    ValueChanged(_value);
                }
            }
            private object _value;
            public string Description { get; }
            // Value changed event handler
            public event EventHandler ValueChangedHandler;
            public void ValueChanged(object value)
            {
                EventHandler handler = ValueChangedHandler;
                if (null != handler) handler(this, EventArgs.Empty);
            }

            //Constructors for different value types
            //float
            public MenuOption(string name, string type, float value, string description = null)
            {
                Name = name;
                Type = type;
                Value = value;
                Description = description;
            }
            //bool
            public MenuOption(string name, string type, bool value, string description = null)
            {
                Name = name;
                Type = type;
                Value = value;
                Description = description;
            }
            //int
            public MenuOption(string name, string type, int value, string description = null)
            {
                Name = name;
                Type = type;
                Value = value;
                Description = description;
            }
            //method
            public MenuOption(string name, string type, Action value, string description = null)
            {
                Name = name;
                Type = type;
                Value = value;
                Description = description;
            }
            //null value
            public MenuOption(string name, string type, string description = null)
            {
                Name = name;
                Type = type;
                Value = null;
                Description = description;
            }

        }
        #region Click Through DLLImports
        //Credits to Oleg Kolosov for the transparency https://stackoverflow.com/a/3367137
        const int WS_EX_TRANSPARENT = 0x00000020;
        const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
        #endregion
        #region Variables
        static CancellationTokenSource cts = null;
        public static Overlay o;
        public static OverlayHandling oh = new OverlayHandling();
        #endregion
        #region Menus
        // Every single menu/submenu has to be put in here to work
        public Dictionary<string, List<MenuOption>> AllMenus = new Dictionary<string, List<MenuOption>>()
        {
            { "MainOptions" , MainOptions },
                { "AutoshowOptions" , AutoShowMenu.AutoShowMenu.AutoShowOptions},
                    { "AutoshowFiltersOptions" , AutoShowMenu.SubMenus.AutoshowFilters.AutoShowFiltersOptions},
                    { "GarageModificationsOptions" , AutoShowMenu.SubMenus.GarageModifications.GarageModificationsOptions},
                    { "OthersModificationsOptions" , AutoShowMenu.SubMenus.OthersModifications.OthersModificationsOptions},
                { "SelfCarsOptions" , SelfCarMenu.SelfCarMenu.SelfCarsOptions},
                    { "HandlingOptions" , SelfCarMenu.HandlingMenu.HandlingMenu.HandlingOptions},
                        { "VelocityOptions" , SelfCarMenu.HandlingMenu.HandlingMenu.VelocityOptions},
                    { "UnlocksOptions" , UnlocksOptions},
                    { "CameraOptions" , CameraOptions},
                { "SettingsOptions" , SettingsMenu.SettingsMenu.SettingsOptions},
                    { "MainAreaOptions" , SettingsMenu.SettingsMenu.MainAreaOptions},
                    { "DescriptionAreaOptions" , SettingsMenu.SettingsMenu.DescriptionAreaOptions}
        };

        // Main menu items, all submenus have their own class in Tabs.Overlay
        public static List<MenuOption> MainOptions = new List<MenuOption>()
        {
            new MenuOption("Autoshow", "MenuButton", "Mods for the Autoshow such as free cars, all cars etc"),
            new MenuOption("Self/Cars", "MenuButton", "Mods for yourself such as speedhack, flyhack etc"),
            new MenuOption("Settings", "MenuButton")
        };
        public static List<MenuOption> UnlocksOptions = new List<MenuOption>()
        {
            new MenuOption("Clothes", "MenuButton"),
            new MenuOption("Horns", "MenuButton"),
            new MenuOption("Emotes", "MenuButton")
        };
        public static List<MenuOption> CameraOptions = new List<MenuOption>()
        {
            new MenuOption("FOV", "MenuButton")
        };

        // Add all sub menu classes here for event handling
        SettingsMenu.SettingsMenu sm = new SettingsMenu.SettingsMenu();
        AutoShowMenu.AutoShowMenu am = new AutoShowMenu.AutoShowMenu();
        #endregion
        #region Main
        public Overlay()
        {
            InitializeComponent();
            o = this;
            DataContext = this;
            InitializeAllSubMenus();
        }

        // This is to make sure all sub menus have their event handlers subscribed
        void InitializeAllSubMenus()
        {
            sm.InitiateSubMenu();
            am.InitalizeEventHandlersForSubMenus();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OverlayHandling.EnableBlur();
            var windowHwnd = new WindowInteropHelper(this).Handle;
            var extendedStyle = GetWindowLong(windowHwnd, GWL_EXSTYLE);
            SetWindowLong(windowHwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }
        public void OverlayToggle(bool Toggle)
        {
            if (Toggle)
            {
                cts = new CancellationTokenSource();
                Task.Run(() => oh.OverlayPosAndScale(cts.Token));
                Task.Run(() => oh.UpdateMenuOptions(cts.Token));
                Task.Run(() => oh.KeyHandler(cts.Token));
                Task.Run(() => oh.ChangeSelection(cts.Token));
            }
            else
            {
                cts.Cancel();
                cts.Dispose();
            }
        }
        #endregion
    }
}
