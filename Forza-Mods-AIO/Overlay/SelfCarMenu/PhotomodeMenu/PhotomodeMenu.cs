using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.PhotomodeMenu;

public abstract class PhotomodeMenu
{
    #region Submenu Options

    private static readonly Overlay.MenuOption SamplesValue = new("Samples", Overlay.MenuOption.OptionType.Int, 0);
    private static readonly Overlay.MenuOption ShutterSpeedValue = new("Shutter Speed", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption TimeSliceValue = new("Time Slice", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption ApertureScaleValue = new("Aperture Scale", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption CarInFocusValue = new("Car In Focus", Overlay.MenuOption.OptionType.Float, 0f);

    private static readonly Overlay.MenuOption TurnSpeedValue = new("Turn Speed", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption SamplesMultiplierValue = new("Samples Multiplier", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption MovementSpeedValue = new("Movement Speed", Overlay.MenuOption.OptionType.Float, 0f);

    private static readonly Overlay.MenuOption NoClipToggle = new("No Clip", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption UnlimitedAltitudeToggle = new("Unlimited Altitude", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption IncreasedZoomToggle = new("Increased Zoom", Overlay.MenuOption.OptionType.Bool, false);
    
    private static readonly Overlay.MenuOption PullValues = new("Pull All Values", Overlay.MenuOption.OptionType.Button, () =>
    {
        var PhotoPage = PhotomodePage.PhotoPage;
        
        PhotoPage.Dispatcher.Invoke(delegate
        {
            SamplesValue.Value = (int)PhotoPage.SamplesBox.Value;
            ShutterSpeedValue.Value = Convert.ToSingle(PhotoPage.ShutterSpeedBox.Value);
            TimeSliceValue.Value = Convert.ToSingle(PhotoPage.TimeSliceBox.Value);
            ApertureScaleValue.Value = Convert.ToSingle(PhotoPage.ApertureScaleBox.Value);
            CarInFocusValue.Value = Convert.ToSingle(PhotoPage.CarInFocusBox.Value);
            TurnSpeedValue.Value = Convert.ToSingle(PhotoPage.TurnSpeed.Value);
            SamplesMultiplierValue.Value = Convert.ToSingle(PhotoPage.SamplesMultiplierSlider.Value);
            MovementSpeedValue.Value = Convert.ToSingle(PhotoPage.SpeedSlider.Value);
        });
    });

    #endregion
    
    public static readonly List<Overlay.MenuOption> PhotomodeOptions = new()
    {
        new Overlay.MenuOption("Photomode Values", Overlay.MenuOption.OptionType.MenuButton),
        new Overlay.MenuOption("Photomode Toggles", Overlay.MenuOption.OptionType.MenuButton)
    };

    public static readonly List<Overlay.MenuOption> PhotomodeValues = new()
    {
        new("Numerics", Overlay.MenuOption.OptionType.SubHeader),
        SamplesValue,
        ShutterSpeedValue,
        TimeSliceValue,
        ApertureScaleValue,
        CarInFocusValue,
        new("Sliders", Overlay.MenuOption.OptionType.SubHeader),
        TurnSpeedValue,
        SamplesMultiplierValue,
        MovementSpeedValue,
        PullValues

    };

    public static readonly List<Overlay.MenuOption> PhotomodeToggles = new()
    {
        NoClipToggle,
        UnlimitedAltitudeToggle,
        IncreasedZoomToggle
    };

    public static void InitiateSubMenu()
    {
        SamplesValue.ValueChangedHandler += Values_OnValueChanged;
        ShutterSpeedValue.ValueChangedHandler += Values_OnValueChanged;
        TimeSliceValue.ValueChangedHandler += Values_OnValueChanged;
        ApertureScaleValue.ValueChangedHandler += Values_OnValueChanged;
        CarInFocusValue.ValueChangedHandler += Values_OnValueChanged;
        TurnSpeedValue.ValueChangedHandler += Values_OnValueChanged;
        SamplesMultiplierValue.ValueChangedHandler += Values_OnValueChanged;
        MovementSpeedValue.ValueChangedHandler += Values_OnValueChanged;
        
        NoClipToggle.ValueChangedHandler += Toggles_OnToggled;
        UnlimitedAltitudeToggle.ValueChangedHandler += Toggles_OnToggled;
        IncreasedZoomToggle.ValueChangedHandler += Toggles_OnToggled;
    }

    #region Value Eventhandlers

    private static void Values_OnValueChanged(object s, EventArgs e)
    {
        PhotomodePage.PhotoPage.Dispatcher.Invoke(() =>
        {
            switch (s.GetType().GetProperty("Name").GetValue(s))
            {
                case "Samples":
                {
                    PhotomodePage.PhotoPage.SamplesBox.Value = (int)SamplesValue.Value;
                    break;
                }
                case "Shutter Speed":
                {
                    PhotomodePage.PhotoPage.ShutterSpeedBox.Value = (float)ShutterSpeedValue.Value;
                    break;
                }
                case "Time Slice":
                {
                    PhotomodePage.PhotoPage.TimeSliceBox.Value = (float)TimeSliceValue.Value;
                    break;
                }
                case "Aperture Scale":
                {
                    PhotomodePage.PhotoPage.ApertureScaleBox.Value = (float)ApertureScaleValue.Value;
                    break;
                }
                case "Car In Focus":
                {
                    PhotomodePage.PhotoPage.CarInFocusBox.Value = (float)CarInFocusValue.Value;
                    break;
                }
                case "Turn Speed":
                {
                    if ((float)s.GetType().GetProperty("Value").GetValue(s) > PhotomodePage.PhotoPage.TurnSpeed.Maximum)
                        SamplesMultiplierValue.Value = (float)PhotomodePage.PhotoPage.TurnSpeed.Maximum;
                    else if ((float)s.GetType().GetProperty("Value").GetValue(s) < PhotomodePage.PhotoPage.TurnSpeed.Minimum)
                        SamplesMultiplierValue.Value = (float)PhotomodePage.PhotoPage.TurnSpeed.Minimum;
                    else
                        PhotomodePage.PhotoPage.TurnSpeed.Value = (float)TurnSpeedValue.Value;
                    break;
                }
                case "Samples Multiplier":
                {
                    if ((float)s.GetType().GetProperty("Value").GetValue(s) > PhotomodePage.PhotoPage.SamplesMultiplierSlider.Maximum)
                        SamplesMultiplierValue.Value = (float)PhotomodePage.PhotoPage.SamplesMultiplierSlider.Maximum;
                    else if ((float)s.GetType().GetProperty("Value").GetValue(s) < PhotomodePage.PhotoPage.SamplesMultiplierSlider.Minimum)
                        SamplesMultiplierValue.Value = (float)PhotomodePage.PhotoPage.SamplesMultiplierSlider.Minimum;
                    else
                        PhotomodePage.PhotoPage.SamplesMultiplierSlider.Value = (float)SamplesMultiplierValue.Value;
                    break;
                }
                case "Movement Speed":
                {
                    if ((float)s.GetType().GetProperty("Value").GetValue(s) > PhotomodePage.PhotoPage.SpeedSlider.Maximum)
                        MovementSpeedValue.Value = (float)PhotomodePage.PhotoPage.SpeedSlider.Maximum;
                    else if ((float)s.GetType().GetProperty("Value").GetValue(s) < PhotomodePage.PhotoPage.SpeedSlider.Minimum)
                        MovementSpeedValue.Value = (float)PhotomodePage.PhotoPage.SpeedSlider.Minimum;
                    else
                        PhotomodePage.PhotoPage.SpeedSlider.Value = (float)MovementSpeedValue.Value;
                    break;
                }
            }
        });
    }

    #endregion

    #region Toggle Eventhandlers

    private static void Toggles_OnToggled(object s, EventArgs e)
    {
        PhotomodePage.PhotoPage.Dispatcher.Invoke(() =>
        {
            switch (s.GetType().GetProperty("Name").GetValue(s))
            {
                case "No Clip":
                {
                    PhotomodePage.PhotoPage.NoClip.IsOn = (bool)NoClipToggle.Value;
                    break;
                }
                case "Unlimited Altitude":
                {
                    PhotomodePage.PhotoPage.NoheightRestriction.IsOn = (bool)UnlimitedAltitudeToggle.Value;
                    break;
                }
                case "Increased Zoom":
                {
                    PhotomodePage.PhotoPage.IncreasedZoom.IsOn = (bool)IncreasedZoomToggle.Value;
                    break;
                }
            }
        });
    }

    #endregion

}