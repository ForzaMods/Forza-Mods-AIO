using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.PhotomodePage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu;

public abstract class PhotomodeMenu
{
    #region Submenu Options

    private static readonly IntOption SamplesValue = new("Samples", 0);
    private static readonly FloatOption ShutterSpeedValue = new("Shutter Speed", 0f);
    private static readonly FloatOption TimeSliceValue = new("Time Slice", 0f);
    private static readonly FloatOption ApertureScaleValue = new("Aperture Scale", 0f);
    private static readonly FloatOption CarInFocusValue = new("Car In Focus", 0f);

    private static readonly FloatOption TurnSpeedValue = new("Turn Speed", 0f, PhotoPage.TurnSpeed.Minimum, PhotoPage.TurnSpeed.Maximum);
    private static readonly FloatOption SamplesMultiplierValue = new("Samples Multiplier", 0f, PhotoPage.SamplesMultiplierSlider.Minimum, PhotoPage.SamplesMultiplierSlider.Maximum);
    private static readonly FloatOption MovementSpeedValue = new("Movement Speed", 0f, PhotoPage.SpeedSlider.Minimum, PhotoPage.SpeedSlider.Maximum);

    private static readonly ToggleOption NoClipToggle = new("No Clip", false);
    private static readonly ToggleOption UnlimitedAltitudeToggle = new("Unlimited Altitude", false);
    private static readonly ToggleOption IncreasedZoomToggle = new("Increased Zoom", false);
    
    private static readonly ButtonOption PullValues = new("Pull All Values",  () =>
    {
        SamplesValue.Value = Convert.ToInt32(PhotoPage.SamplesBox.Value);
        ShutterSpeedValue.Value = Convert.ToSingle(PhotoPage.ShutterSpeedBox.Value);
        TimeSliceValue.Value = Convert.ToSingle(PhotoPage.TimeSliceBox.Value);
        ApertureScaleValue.Value = Convert.ToSingle(PhotoPage.ApertureScaleBox.Value);
        CarInFocusValue.Value = Convert.ToSingle(PhotoPage.CarInFocusBox.Value);
        TurnSpeedValue.Value = Convert.ToSingle(PhotoPage.TurnSpeed.Value);
        SamplesMultiplierValue.Value = Convert.ToSingle(PhotoPage.SamplesMultiplierSlider.Value);
        MovementSpeedValue.Value = Convert.ToSingle(PhotoPage.SpeedSlider.Value);
    });

    #endregion
    
    public static readonly List<MenuOption> PhotomodeOptions = new()
    {
        new MenuButtonOption("Photomode Values"),
        new MenuButtonOption("Photomode Toggles")
    };

    public static readonly List<MenuOption> PhotomodeValues = new()
    {
        new SubHeaderOption("Numerics"),
        SamplesValue,
        ShutterSpeedValue,
        TimeSliceValue,
        ApertureScaleValue,
        CarInFocusValue,
        new SubHeaderOption("Sliders"),
        TurnSpeedValue,
        SamplesMultiplierValue,
        MovementSpeedValue,
        PullValues

    };

    public static readonly List<MenuOption> PhotomodeToggles = new()
    {
        NoClipToggle,
        UnlimitedAltitudeToggle,
        IncreasedZoomToggle
    };

    public static void InitiateSubMenu()
    {
        SamplesValue.ValueChangedEventHandler += Values_OnValueChanged;
        ShutterSpeedValue.ValueChangedEventHandler += Values_OnValueChanged;
        TimeSliceValue.ValueChangedEventHandler += Values_OnValueChanged;
        ApertureScaleValue.ValueChangedEventHandler += Values_OnValueChanged;
        CarInFocusValue.ValueChangedEventHandler += Values_OnValueChanged;
        TurnSpeedValue.ValueChangedEventHandler += Values_OnValueChanged;
        SamplesMultiplierValue.ValueChangedEventHandler += Values_OnValueChanged;
        MovementSpeedValue.ValueChangedEventHandler += Values_OnValueChanged;
        
        NoClipToggle.ToggledEventHandler += Toggles_OnToggled;
        UnlimitedAltitudeToggle.ToggledEventHandler += Toggles_OnToggled;
        IncreasedZoomToggle.ToggledEventHandler += Toggles_OnToggled;
    }

    #region Value Eventhandlers

    private static void Values_OnValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }
        
        switch (floatOption.Name)
        {
            case "Samples":
            {
                PhotoPage.SamplesBox.Value = SamplesValue.Value;
                break;
            }
            case "Shutter Speed":
            {
                PhotoPage.ShutterSpeedBox.Value = ShutterSpeedValue.Value;
                break;
            }
            case "Time Slice":
            {
                PhotoPage.TimeSliceBox.Value = TimeSliceValue.Value;
                break;
            }
            case "Aperture Scale":
            {
                PhotoPage.ApertureScaleBox.Value = ApertureScaleValue.Value;
                break;
            }
            case "Car In Focus":
            {
                PhotoPage.CarInFocusBox.Value = CarInFocusValue.Value;
                break;
            }
            case "Turn Speed":
            {
                PhotoPage.TurnSpeed.Value = TurnSpeedValue.Value;
                break;
            }
            case "Samples Multiplier":
            {
                PhotoPage.SamplesMultiplierSlider.Value = SamplesMultiplierValue.Value;
                break;
            }
            case "Movement Speed":
            {
                PhotoPage.SpeedSlider.Value = MovementSpeedValue.Value;
                break;
            }
        }
    }

    #endregion

    #region Toggle Eventhandlers

    private static void Toggles_OnToggled(object s, EventArgs e)
    {
        if (s is not ToggleOption toggleOption)
        {
            return;
        }
        
        switch (toggleOption.Name)
        {
            case "No Clip":
            {
                PhotoPage.NoClip.IsOn = NoClipToggle.IsOn;
                break;
            }
            case "Unlimited Altitude":
            {
                PhotoPage.NoheightRestriction.IsOn = UnlimitedAltitudeToggle.IsOn;
                break;
            }
            case "Increased Zoom":
            {
                PhotoPage.IncreasedZoom.IsOn = IncreasedZoomToggle.IsOn;
                break;
            }
        }
    }

    #endregion

}