using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Forza_Mods_AIO.Overlay;

/// <summary>
///     Interaction logic for Overlay.xaml
/// </summary>
public partial class Overlay
{
    public enum OptionType
    {
        Float,
        Int,
        Bool,
        Selection,
        MenuButton,
        Button,
        SubHeader
    }
    
    public class MenuOption
    {
        public string Name { get; }
        public string? Description { get; }
        public OptionType Type { get; }
        
        public object Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged(_value);
            }
        }
        private object _value = null!;
        
        public object? Min { get; }
        public object? Max { get; }
        public object? Interval { get; }
        
        public string[]? Selections { get; }
        
        public bool IsEnabled { get; set; }
        
        // Value changed event handler
        public event EventHandler? ValueChangedHandler;
        
        private void ValueChanged(object value)
        {
            var handler = ValueChangedHandler;
            handler?.Invoke(this, EventArgs.Empty);
        }
            
        /// <summary>
        /// Float based constructor for a menu option
        /// </summary>
        /// <param name="name">The name of the menu option.</param>
        /// <param name="value">The initial float value of the menu option.</param>
        /// <param name="min">The minimum allowed value for the menu option. (Optional)</param>
        /// <param name="max">The maximum allowed value for the menu option. (Optional)</param>
        /// <param name="interval">Specify the interval for the menu option value. (Optional)</param>
        /// <param name="description">A description providing additional information about the menu option. (Optional)</param>
        /// <param name="isEnabled">A bool indicating whether the menu option is enabled. Defaults to true. (Optional)</param>
        public MenuOption(string name, float value, float? min = null, float? max = null, float? interval = null, string? description = null, bool isEnabled = true)
        {
            
            if (min != null && max != null && min.Value >= max.Value)
            {
                throw new ArgumentException("Minimum value must be less than the maximum value.");
            }
            
            Name = name;
            Type = OptionType.Float;
            Value = Convert.ToSingle(value);
            Min = min == null ? null : Convert.ToSingle(min);
            Max = max == null ? null : Convert.ToSingle(max);
            Interval = interval;
            Description = description;
            IsEnabled = isEnabled;
        }
        
        /// <summary>
        /// Bool based constructor for a menu option
        /// </summary>
        /// <param name="name">The name of the menu option.</param>
        /// <param name="value">The initial bool value of the menu option.</param>
        /// <param name="description">A description providing additional information about the menu option. (Optional)</param>
        /// <param name="isEnabled">A bool indicating whether the menu option is enabled. Defaults to true. (Optional)</param>
        public MenuOption(string name, bool value, string? description = null, bool isEnabled = true)
        {
            Name = name;
            Type = OptionType.Bool;
            Value = Convert.ToBoolean(value);
            Description = description;
            IsEnabled = isEnabled;
        }
        
        /// <summary>
        /// Int based constructor for a menu option
        /// </summary>
        /// <param name="name">The name of the menu option.</param>
        /// <param name="value">The initial int value of the menu option.</param>
        /// <param name="min">The minimum allowed value for the menu option. (Optional)</param>
        /// <param name="max">The maximum allowed value for the menu option. (Optional)</param>
        /// <param name="interval">The interval for the value for the menu option. (Optional)</param>
        /// <param name="description">A description providing additional information about the menu option. (Optional)</param>
        /// <param name="isEnabled">A bool indicating whether the menu option is enabled. Defaults to true. (Optional)</param>
        public MenuOption(string name, int value, int? min = null, int? max = null, int? interval = null, string? description = null, bool isEnabled = true)
        {
            if (min != null && max != null && min.Value >= max.Value)
            {
                throw new ArgumentException("Minimum value must be less than the maximum value.");
            }
            
            Name = name;
            Type = OptionType.Int;
            Value = Convert.ToInt32(value);
            Min = min == null ? null : Convert.ToInt32(min);
            Max = max == null ? null : Convert.ToInt32(max);
            Interval = interval;
            Description = description;
            IsEnabled = isEnabled;
        }

        /// <summary>
        /// Button/Action based constructor for a menu option
        /// </summary>
        /// <param name="name">The name of the menu option.</param>
        /// <param name="value">The action that will execute when clicking on the button.</param>
        /// <param name="description">A description providing additional information about the menu option. (Optional)</param>
        /// <param name="isEnabled">A bool indicating whether the menu option is enabled. Defaults to true. (Optional)</param>
        public MenuOption(string name, Action value, string? description = null, bool isEnabled = true)
        {
            Name = name;
            Type = OptionType.Button;
            Value = value;
            Description = description;
            IsEnabled = isEnabled;
        }

        /// <summary>
        /// Subheader/MenuOption based constructor for a menu option
        /// </summary>
        /// <param name="name">The name of the menu option.</param>
        /// <param name="type">The type for the menu option. (needs to be either SubHeader or MenuButton)</param>
        /// <param name="isScannable">A bool indicating whether the menu option contains a progressbar and is able to be scanned. (applies only when the type is MenuButton)</param>
        /// <param name="description">A description providing additional information about the menu option. (Optional)</param>
        /// <param name="isEnabled">A bool indicating whether the menu option is enabled. Defaults to true. (Optional)</param>
        public MenuOption(string name, OptionType type, string? description = null, bool isEnabled = true)
        {
            if (type != OptionType.MenuButton && type != OptionType.SubHeader)
            {
                throw new ArgumentException("The type for this constructor needs to be either MenuButton or SubHeader");
            }
            
            Name = name;
            Type = type;
            Value = null!;
            Description = description;
            IsEnabled = isEnabled;
        }
        
        /// <summary>
        /// Null value based constructor for a menu option
        /// </summary>
        /// <param name="name">The name of the menu option.</param>
        /// <param name="value">The initial index of the menu option.</param>
        /// <param name="selections">An array of strings representing the available selections for the menu option.</param>
        /// <param name="description">A description providing additional information about the menu option. (Optional)</param>
        /// <param name="isEnabled">A bool indicating whether the menu option is enabled. Defaults to true. (Optional)</param>
        public MenuOption(string name, int value, string[] selections, string? description = null, bool isEnabled = true)
        {
            Name = name;
            Type = OptionType.Selection;
            Value = value;
            Selections = selections;
            Description = description;
            IsEnabled = isEnabled;
        }
    }
    
    #region Click Through DLLImports
    //Credits to Oleg Kolosov for the transparency https://stackoverflow.com/a/3367137
    private const int WsExTransparent = 0x00000020;
    private const int GwlExStyle = -20;

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int index);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int index, int newStyle);
    #endregion
    #region Variables
    private static CancellationTokenSource? _cts;
    public static Overlay O { get; private set; } = null!;
    public static readonly OverlayHandling Oh = new();
    #endregion
    #region Menus
    // Every single menu/submenu has to be put in here to work
    public readonly Dictionary<string, List<MenuOption>> AllMenus = new()
    {
        { "MainOptions" , MainOptions },
            { "AutoshowGarageOptions" , AutoShowMenu.AutoShowMenu.AutoShowOptions},
                { "AutoshowFiltersOptions" , AutoShowMenu.SubMenus.AutoshowFilters.AutoShowFiltersOptions},
                { "GarageModificationsOptions" , AutoShowMenu.SubMenus.GarageModifications.GarageModificationsOptions},
                { "OthersModificationsOptions" , AutoShowMenu.SubMenus.OthersModifications.OthersModificationsOptions},
            { "SelfVehicleOptions" , SelfCarMenu.SelfCarMenu.SelfCarsOptions},
                { "HandlingOptions" , SelfCarMenu.HandlingMenu.HandlingMenu.HandlingOptions},
                    { "VelocityOptions" , SelfCarMenu.HandlingMenu.HandlingMenu.VelocityOptions},
                    { "ModifiersOptions" , SelfCarMenu.HandlingMenu.ModifiersMenu.ModifiersOptions},
                    { "WheelSpeedOptions", SelfCarMenu.HandlingMenu.WheelspeedMenu.WheelSpeedOptions },
                    { "TurnAssistOptions", SelfCarMenu.HandlingMenu.TurnAssistMenu.TurnAssistOptions },
                    { "FlyhackOptions", SelfCarMenu.HandlingMenu.FlyhackMenu.FlyhackOptions },
                    { "HandlingTogglesOptions", SelfCarMenu.HandlingMenu.HandlingTogglesMenu.HandlingTogglesOptions }, 
                { "CustomizationOptions", SelfCarMenu.CustomizationMenu.CustomizationMenu.CustomizationOptions },
                { "UnlocksOptions" , SelfCarMenu.UnlocksMenu.CurrencyMenu.CurrencyMenuOptions },
                    { "CurrencyOptions" , SelfCarMenu.UnlocksMenu.CurrencyMenu.CurrencyMenuOptions },
                { "PhotomodeOptions" , SelfCarMenu.PhotomodeMenu.PhotomodeMenu.PhotomodeOptions},
                    { "PhotomodeValuesOptions" , SelfCarMenu.PhotomodeMenu.PhotomodeMenu.PhotomodeValues },
                    { "PhotomodeTogglesOptions" , SelfCarMenu.PhotomodeMenu.PhotomodeMenu.PhotomodeToggles },
                { "MiscellaneousOptions", SelfCarMenu.MiscMenu.MiscMenu.MiscMenuOptions },
                { "FOVOptions" , SelfCarMenu.FovMenu.FovMenu.FovOptions },
                    { "FovLockOptions" , SelfCarMenu.FovMenu.FovLock.FovLockOptions },
                    { "FovLimitersOptions" , SelfCarMenu.FovMenu.FovLimiters.FovLimiterOptions },
                        { "ChaseLimitersOptions" , SelfCarMenu.FovMenu.FovLimiters.ChaseLimitersOptions },
                        { "DriverLimitersOptions" , SelfCarMenu.FovMenu.FovLimiters.DriverLimitersOptions },
                        { "HoodLimitersOptions" , SelfCarMenu.FovMenu.FovLimiters.HoodLimitersOptions },
                        { "BumperLimitersOptions" , SelfCarMenu.FovMenu.FovLimiters.BumperLimitersOptions },
                { "BackfireOptions" , SelfCarMenu.BackfireMenu.BackfireMenuOptions },
            { "TuningOptions" , Tuning.Tuning.TuningOptions },
                { "AeroOptions" , Tuning.SubMenus.Aero.Aero.AeroOptions },
                { "AlignmentOptions" , Tuning.SubMenus.Alignment.Alignment.AlignmentOptions },
                { "DampingOptions" , Tuning.SubMenus.Damping.Damping.DampingOptions },         
                    { "AntirollBarsDampingOptions" , Tuning.SubMenus.Damping.SubMenus.AntirollBarsDamping.AntirollBarsDampingOptions },  
                    { "ReboundStiffnessOptions" , Tuning.SubMenus.Damping.SubMenus.ReboundStiffness.ReboundStiffnessOptions },  
                    { "BumpStiffnessOptions" , Tuning.SubMenus.Damping.SubMenus.BumpStiffness.BumpStiffnessOptions },  
                { "GearingOptions" , Tuning.SubMenus.Gearing.Gearing.GearingOptions },  
                { "OthersOptions" , Tuning.SubMenus.Others.Others.OthersOptions },  
                    { "WheelbaseOptions" , Tuning.SubMenus.Others.SubMenu.Wheelbase.WheelbaseOptions },  
                    { "RimsOptions" , Tuning.SubMenus.Others.SubMenu.Rims.RimsOptions },  
                { "SpringsOptions" , Tuning.SubMenus.Springs.Springs.SpringsOptions },  
                    { "SpringsValuesOptions" , Tuning.SubMenus.Springs.SubMenus.SpringsValues.SpringsSubMenuOptions },  
                    { "RideHeightOptions" , Tuning.SubMenus.Springs.SubMenus.RideHeight.RideHeightOptions }, 
                { "SteeringOptions" , Tuning.SubMenus.Steering.Steering.SteeringOptions },  
                { "TiresOptions" , Tuning.SubMenus.Tires.Tires.TiresOptions },  
            { "SettingsOptions" , SettingsMenu.SettingsMenu.SettingsOptions},
                { "MainAreaOptions" , SettingsMenu.SettingsMenu.MainAreaOptions},
                { "DescriptionAreaOptions" , SettingsMenu.SettingsMenu.DescriptionAreaOptions},
                { "PositionOptions", SettingsMenu.SettingsMenu.PositionOptions },
                { "FontSettingsOptions", SettingsMenu.SettingsMenu.FontOptions }
    };
    
    public static readonly MenuOption AutoshowGarageOption = new("Autoshow/Garage", OptionType.MenuButton, "Mods for the Autoshow such as free cars, all cars etc", false);
    public static readonly MenuOption SelfVehicleOption = new("Self/Vehicle", OptionType.MenuButton,"Mods for yourself such as speedhack, flyhack etc", false);
    public static readonly MenuOption TuningOption = new("Tuning", OptionType.MenuButton,"Mods such as extended tuning limits ect", false);
        
    // Main menu items, all submenus have their own class in Tabs.Overlay
    private static readonly List<MenuOption> MainOptions = new()
    {
        AutoshowGarageOption,
        SelfVehicleOption,
        TuningOption,
        new MenuOption("Settings", OptionType.MenuButton)
    };

    // Add all sub menu classes here for event handling
    public readonly SettingsMenu.SettingsMenu Sm = new();
    #endregion
    #region Main
    public Overlay()
    {
        InitializeComponent();
        O = this;
        DataContext = this;
        InitializeAllSubMenus();
    }

    // This is to make sure all sub menus have their event handlers subscribed
    private void InitializeAllSubMenus()
    {
        Sm.InitiateSubMenu();
        SelfCarMenu.SelfCarMenu.InitiateSubMenu();
        AutoShowMenu.AutoShowMenu.InitiateSubMenu();
        Tuning.Tuning.InitiateSubMenu();
    }
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        OverlayHandling.EnableBlur();
        var windowHandle = new WindowInteropHelper(this).Handle;
        var extendedStyle = GetWindowLong(windowHandle, GwlExStyle);
        _ = SetWindowLong(windowHandle, GwlExStyle, extendedStyle | WsExTransparent);
    }
    public void OverlayToggle(bool toggle)
    {
        if (toggle)
        {
            _cts = new CancellationTokenSource();
            Task.Run(() => Oh.OverlayPosAndScale(_cts.Token));
            Task.Run(() => Oh.UpdateMenuOptions(_cts.Token));
            Task.Run(() => Oh.KeyHandler(_cts.Token));
            Task.Run(() => Oh.ChangeSelection(_cts.Token));
            Task.Run(() => Oh.ChangeValue(_cts.Token));
        }
        else
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
    #endregion
}