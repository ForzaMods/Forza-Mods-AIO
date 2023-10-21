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
using Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping.SubMenus;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Gearing;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Others;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Others.SubMenu;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Springs.SubMenus;
using Aero = Forza_Mods_AIO.Overlay.Tuning.SubMenus.Aero.Aero;
using Alignment = Forza_Mods_AIO.Overlay.Tuning.SubMenus.Alignment.Alignment;
using Springs = Forza_Mods_AIO.Overlay.Tuning.SubMenus.Springs;
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
            private void ValueChanged(object value)
            {
                EventHandler handler = ValueChangedHandler;
                handler?.Invoke(this, EventArgs.Empty);
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
                { "AutoshowGarageOptions" , AutoShowMenu.AutoShowMenu.AutoShowOptions},
                    { "AutoshowFiltersOptions" , AutoShowMenu.SubMenus.AutoshowFilters.AutoShowFiltersOptions},
                    { "GarageModificationsOptions" , AutoShowMenu.SubMenus.GarageModifications.GarageModificationsOptions},
                    { "OthersModificationsOptions" , AutoShowMenu.SubMenus.OthersModifications.OthersModificationsOptions},
                { "SelfVehicleOptions" , SelfCarMenu.SelfCarMenu.SelfCarsOptions},
                    { "HandlingOptions" , SelfCarMenu.HandlingMenu.HandlingMenu.HandlingOptions},
                        { "VelocityOptions" , SelfCarMenu.HandlingMenu.HandlingMenu.VelocityOptions},
                        { "ModifiersOptions" , ModifiersMenu.ModifiersOptions},
                        { "WheelSpeedOptions", SelfCarMenu.HandlingMenu.HandlingMenu.WheelSpeedOptions },
                        { "CustomizationOptions", SelfCarMenu.CustomizationMenu.CustomizationMenu.CustomizationOptions },
                    { "UnlocksOptions" , UnlocksOptions},
                    { "CameraOptions" , CameraOptions},
                { "TuningOptions" , Tuning.Tuning.TuningOptions },
                    { "AeroOptions" , Aero.AeroOptions },
                    { "AlignmentOptions" , Alignment.AlignmentOptions },
                    { "DampingOptions" , Damping.DampingOptions },         
                        { "AntirollBarsDampingOptions" , AntirollBarsDamping.AntirollBarsDampingOptions },  
                        { "ReboundStiffnessOptions" , ReboundStiffness.ReboundStiffnessOptions },  
                        { "BumpStiffnessOptions" , BumpStiffness.BumpStiffnessOptions },  
                    { "GearingOptions" , Gearing.GearingOptions },  
                    { "OthersOptions" , Others.OthersOptions },  
                        { "WheelbaseOptions" , Wheelbase.WheelbaseOptions },  
                        { "RimsOptions" , Rims.RimsOptions },  
                    { "SpringsOptions" , Springs.Springs.SpringsOptions },  
                        { "SpringsValuesOptions" , SpringsValues.SpringsSubMenuOptions },  
                        { "RideHeightOptions" , RideHeight.RideHeightOptions }, 
                    { "TiresOptions" , Tuning.SubMenus.Tires.Tires.TiresOptions },  
                { "SettingsOptions" , SettingsMenu.SettingsMenu.SettingsOptions},
                    { "MainAreaOptions" , SettingsMenu.SettingsMenu.MainAreaOptions},
                    { "DescriptionAreaOptions" , SettingsMenu.SettingsMenu.DescriptionAreaOptions}
        };

        // Main menu items, all submenus have their own class in Tabs.Overlay
        public static List<MenuOption> MainOptions = new List<MenuOption>()
        {
            new MenuOption("Autoshow/Garage", "MenuButton", "Mods for the Autoshow such as free cars, all cars etc"),
            new MenuOption("Self/Vehicle", "MenuButton", "Mods for yourself such as speedhack, flyhack etc"),
            new MenuOption("Tuning", "MenuButton", "Mods such as extended tuning limits ect"),
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
        HandlingMenu hm = new();
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
            am.InitiateSubMenu();
            hm.InitiateSubMenu();
            Aero.InitiateSubMenu();
            Alignment.InitiateSubMenu();
            AntirollBarsDamping.InitiateSubMenu();
            ReboundStiffness.InitiateSubMenu();
            BumpStiffness.InitiateSubMenu();
            Gearing.InitiateSubMenu();
            Wheelbase.InitiateSubMenu();
            Rims.InitiateSubMenu();
            SpringsValues.InitiateSubMenu();
            RideHeight.InitiateSubMenu();
            SelfCarMenu.CustomizationMenu.CustomizationMenu.InitiateSubMenu();
            Tuning.SubMenus.Tires.Tires.InitiateSubMenu();
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
