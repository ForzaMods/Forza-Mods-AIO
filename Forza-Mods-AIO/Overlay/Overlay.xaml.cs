using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Forza_Mods_AIO.Overlay.Menus.SettingsMenu;
using Forza_Mods_AIO.Overlay.Options;

namespace Forza_Mods_AIO.Overlay;

/// <summary>
///     Interaction logic for Overlay.xaml
/// </summary>
public partial class Overlay
{
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
                { "AutoshowFiltersOptions" , Menus.AutoShowMenu.SubMenus.AutoshowFilters.AutoShowFiltersOptions},
                { "GarageModificationsOptions" , Menus.AutoShowMenu.SubMenus.GarageModifications.GarageModificationsOptions},
                { "OthersModificationsOptions" , Menus.AutoShowMenu.SubMenus.OthersModifications.OthersModificationsOptions},
            { "SelfVehicleOptions" , Menus.SelfCarMenu.SelfCarMenu.SelfCarsOptions},
                { "HandlingOptions" , Menus.SelfCarMenu.HandlingMenu.HandlingMenu.HandlingOptions},
                    { "VelocityOptions" , Menus.SelfCarMenu.HandlingMenu.VelocityMenu.VelocityOptions},
                    { "ModifiersOptions" , Menus.SelfCarMenu.HandlingMenu.ModifiersMenu.ModifiersOptions},
                    { "WheelSpeedOptions", Menus.SelfCarMenu.HandlingMenu.WheelspeedMenu.WheelSpeedOptions },
                    { "TurnAssistOptions", Menus.SelfCarMenu.HandlingMenu.TurnAssistMenu.TurnAssistOptions },
                    { "FlyhackOptions", Menus.SelfCarMenu.HandlingMenu.FlyhackMenu.FlyhackOptions },
                    { "HandlingTogglesOptions", Menus.SelfCarMenu.HandlingMenu.HandlingTogglesMenu.HandlingTogglesOptions }, 
                { "CustomizationOptions", Menus.SelfCarMenu.CustomizationMenu.CustomizationOptions },
                { "UnlocksOptions" , Menus.SelfCarMenu.UnlocksMenu.CurrencyMenuOptions },
                { "PhotomodeOptions" , Menus.SelfCarMenu.PhotomodeMenu.PhotomodeOptions},
                    { "PhotomodeValuesOptions" , Menus.SelfCarMenu.PhotomodeMenu.PhotomodeValues },
                    { "PhotomodeTogglesOptions" , Menus.SelfCarMenu.PhotomodeMenu.PhotomodeToggles },
                { "MiscellaneousOptions", Menus.SelfCarMenu.MiscMenu.MiscMenuOptions },
                { "FOVOptions" , Menus.SelfCarMenu.FovMenu.FovMenu.FovOptions },
                    { "FovLockOptions" , Menus.SelfCarMenu.FovMenu.FovLock.FovLockOptions },
                    { "FovLimitersOptions" , Menus.SelfCarMenu.FovMenu.FovLimiters.FovLimiterOptions },
                        { "ChaseLimitersOptions" , Menus.SelfCarMenu.FovMenu.FovLimiters.ChaseLimitersOptions },
                        { "DriverLimitersOptions" , Menus.SelfCarMenu.FovMenu.FovLimiters.DriverLimitersOptions },
                        { "HoodLimitersOptions" , Menus.SelfCarMenu.FovMenu.FovLimiters.HoodLimitersOptions },
                        { "BumperLimitersOptions" , Menus.SelfCarMenu.FovMenu.FovLimiters.BumperLimitersOptions },
                { "BackfireOptions" , Menus.SelfCarMenu.BackfireMenu.BackfireMenuOptions },
            { "TuningOptions" , Menus.TuningMenu.Tuning.TuningOptions },
                { "AeroOptions" , Menus.TuningMenu.SubMenus.Aero.Aero.AeroOptions },
                { "AlignmentOptions" , Menus.TuningMenu.SubMenus.Alignment.Alignment.AlignmentOptions },
                { "DampingOptions" , Menus.TuningMenu.SubMenus.Damping.Damping.DampingOptions },         
                    { "AntirollBarsDampingOptions" , Menus.TuningMenu.SubMenus.Damping.SubMenus.AntirollBarsDamping.AntirollBarsDampingOptions },  
                    { "ReboundStiffnessOptions" , Menus.TuningMenu.SubMenus.Damping.SubMenus.ReboundStiffness.ReboundStiffnessOptions },  
                    { "BumpStiffnessOptions" , Menus.TuningMenu.SubMenus.Damping.SubMenus.BumpStiffness.BumpStiffnessOptions },  
                { "GearingOptions" , Menus.TuningMenu.SubMenus.Gearing.Gearing.GearingOptions },  
                { "OthersOptions" , Menus.TuningMenu.SubMenus.Others.Others.OthersOptions },  
                    { "WheelbaseOptions" , Menus.TuningMenu.SubMenus.Others.SubMenu.Wheelbase.WheelbaseOptions },  
                    { "RimsOptions" , Menus.TuningMenu.SubMenus.Others.SubMenu.Rims.RimsOptions },  
                { "SpringsOptions" , Menus.TuningMenu.SubMenus.Springs.Springs.SpringsOptions },  
                    { "SpringsValuesOptions" , Menus.TuningMenu.SubMenus.Springs.SubMenus.SpringsValues.SpringsSubMenuOptions },  
                    { "RideHeightOptions" , Menus.TuningMenu.SubMenus.Springs.SubMenus.RideHeight.RideHeightOptions }, 
                { "SteeringOptions" , Menus.TuningMenu.SubMenus.Steering.Steering.SteeringOptions },  
                { "TiresOptions" , Menus.TuningMenu.SubMenus.Tires.Tires.TiresOptions },  
            { "SettingsOptions" , SettingsMenu.SettingsOptions},
                { "MainAreaOptions" , SettingsMenu.MainAreaOptions},
                { "DescriptionAreaOptions" , SettingsMenu.DescriptionAreaOptions},
                { "PositionOptions", SettingsMenu.PositionOptions },
                { "FontSettingsOptions", SettingsMenu.FontOptions }
    };
    
    public static readonly MenuButtonOption AutoshowGarageOption = new("Autoshow/Garage", "Mods for the Autoshow such as free cars, all cars etc", false);
    public static readonly MenuButtonOption SelfVehicleOption = new("Self/Vehicle","Mods for yourself such as speedhack, flyhack etc", false);
    public static readonly MenuButtonOption TuningOption = new("Tuning","Mods such as extended tuning limits ect", false);
    
    private static readonly List<MenuOption> MainOptions = new()
    {
        AutoshowGarageOption,
        SelfVehicleOption,
        TuningOption,
        new MenuButtonOption("Settings")
    };

    private readonly SettingsMenu _sm = new();
    #endregion
    #region Main
    public Overlay()
    {
        InitializeComponent();
        O = this;
        DataContext = this;
        InitializeAllSubMenus();
    }

    private void InitializeAllSubMenus()
    {
        _sm.InitiateSubMenu();
        Menus.SelfCarMenu.SelfCarMenu.InitiateSubMenu();
        AutoShowMenu.AutoShowMenu.InitiateSubMenu();
        Menus.TuningMenu.Tuning.InitiateSubMenu();
    }
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        OverlayHandling.EnableBlur();
    }
    
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
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
            //Task.Run(() => Oh.ControllerKeyHandler(_cts.Token));
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