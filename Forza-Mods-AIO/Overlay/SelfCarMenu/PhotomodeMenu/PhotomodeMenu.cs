using System;
using System.Collections.Generic;
using System.Windows.Threading;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.PhotomodeMenu;

public abstract class PhotomodeMenu
{
    private static readonly Overlay.MenuOption SamplesValue = new("Samples", "Int", 0);
    private static readonly Overlay.MenuOption ShutterSpeedValue = new("Shutter Speed", "Float", 0f);
    private static readonly Overlay.MenuOption TimeSliceValue = new("Time Slice", "Float", 0f);
    private static readonly Overlay.MenuOption ApertureScaleValue = new("Aperture Scale", "Float", 0f);
    private static readonly Overlay.MenuOption CarInFocusValue = new("Car In Focus", "Float", 0f);

    private static readonly Overlay.MenuOption TurnSpeedValue = new("Turn Speed", "Float", 0f);
    private static readonly Overlay.MenuOption SamplesMultiplierValue = new("Samples Multiplier", "Float", 0f);
    private static readonly Overlay.MenuOption MovementSpeedValue = new("Movement Speed", "Float", 0f);

    private static readonly Overlay.MenuOption NoClipToggle = new("No Clip", "Bool", false);
    private static readonly Overlay.MenuOption UnlimitedAltitudeToggle = new("Unlimited Altitude", "Bool", false);
    private static readonly Overlay.MenuOption IncreasedZoomToggle = new("Increased Zoom", "Bool", false);
    
    private static readonly Overlay.MenuOption PullValues = new("Pull All Values", "Button", (() =>
    {
        var PhotoPage = PhotomodePage.PhotoPage;
        
        PhotoPage.Dispatcher.Invoke(delegate ()
        {
            SamplesValue.Value = PhotoPage.SamplesBox.Value;
            ShutterSpeedValue.Value = PhotoPage.ShutterSpeedBox.Value;
            TimeSliceValue.Value = PhotoPage.TimeSliceBox.Value;
            ApertureScaleValue.Value = PhotoPage.ApertureScaleBox.Value;
            CarInFocusValue.Value = PhotoPage.CarInFocusBox.Value;
            TurnSpeedValue.Value = PhotoPage.TurnSpeed.Value;
            SamplesMultiplierValue.Value = PhotoPage.SamplesMultiplierSlider.Value;
            MovementSpeedValue.Value = PhotoPage.SpeedSlider.Value;
        });
    }));

    public static readonly List<Overlay.MenuOption> PhotomodeOptions = new()
    {
        new Overlay.MenuOption("Photomode Values", "MenuButton"),
        new Overlay.MenuOption("Photomode Toggles", "MenuButton")
    };

    public static readonly List<Overlay.MenuOption> PhotomodeValues = new()
    {
        new("Numerics", "SubHeader"),
        SamplesValue,
        ShutterSpeedValue,
        TimeSliceValue,
        ApertureScaleValue,
        CarInFocusValue,
        new("Sliders", "SubHeader"),
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

    private static void Values_OnValueChanged(object s, EventArgs e)
    {
        switch (s.GetType().GetProperty("name").GetValue(s))
        {
            case "Samples":
            {
                PhotomodePage.PhotoPage.Dispatcher.Invoke(() => PhotomodePage.PhotoPage.SamplesBox.Value = (int)SamplesValue.Value);
                break;
            }
            case "Shutter Speed":
            {
                PhotomodePage.PhotoPage.Dispatcher.Invoke(() => PhotomodePage.PhotoPage.ShutterSpeedBox.Value = (float)ShutterSpeedValue.Value);
                break;
            }
            case "Time Slice":
            {
                PhotomodePage.PhotoPage.Dispatcher.Invoke(() => PhotomodePage.PhotoPage.TimeSliceBox.Value = (float)TimeSliceValue.Value);
                break;
            }
            case "Aperture Scale":
            {
                PhotomodePage.PhotoPage.Dispatcher.Invoke(() => PhotomodePage.PhotoPage.ApertureScaleBox.Value = (float)ApertureScaleValue.Value);
                break;
            }
            case "Car In Focus":
            {
                PhotomodePage.PhotoPage.Dispatcher.Invoke(() => PhotomodePage.PhotoPage.CarInFocusBox.Value = (float)CarInFocusValue.Value);
                break;
            }
            case "Turn Speed":
            {
                if ((float)s.GetType().GetProperty("Value").GetValue(s) > PhotomodePage.PhotoPage.TurnSpeed.Maximum)
                    SamplesMultiplierValue.Value = (float)PhotomodePage.PhotoPage.TurnSpeed.Maximum;
                else if ((float)s.GetType().GetProperty("Value").GetValue(s) < PhotomodePage.PhotoPage.TurnSpeed.Minimum)
                    SamplesMultiplierValue.Value = (float)PhotomodePage.PhotoPage.TurnSpeed.Minimum;
                else
                    PhotomodePage.PhotoPage.Dispatcher.Invoke(() => PhotomodePage.PhotoPage.TurnSpeed.Value = (float)TurnSpeedValue.Value);
                break;
            }
            case "Samples Multiplier":
            {
                if ((float)s.GetType().GetProperty("Value").GetValue(s) > PhotomodePage.PhotoPage.SamplesMultiplierSlider.Maximum)
                    SamplesMultiplierValue.Value = (float)PhotomodePage.PhotoPage.SamplesMultiplierSlider.Maximum;
                else if ((float)s.GetType().GetProperty("Value").GetValue(s) < PhotomodePage.PhotoPage.SamplesMultiplierSlider.Minimum)
                    SamplesMultiplierValue.Value = (float)PhotomodePage.PhotoPage.SamplesMultiplierSlider.Minimum;
                else
                    PhotomodePage.PhotoPage.Dispatcher.Invoke(() => PhotomodePage.PhotoPage.SamplesMultiplierSlider.Value = (float)SamplesMultiplierValue.Value);
                break;
            }
            case "Movement Speed":
            {
                if ((float)s.GetType().GetProperty("Value").GetValue(s) > PhotomodePage.PhotoPage.SpeedSlider.Maximum)
                    MovementSpeedValue.Value = (float)PhotomodePage.PhotoPage.SpeedSlider.Maximum;
                else if ((float)s.GetType().GetProperty("Value").GetValue(s) < PhotomodePage.PhotoPage.SpeedSlider.Minimum)
                    MovementSpeedValue.Value = (float)PhotomodePage.PhotoPage.SpeedSlider.Minimum;
                else
                    PhotomodePage.PhotoPage.Dispatcher.Invoke(() => PhotomodePage.PhotoPage.SpeedSlider.Value = (float)MovementSpeedValue.Value);
                break;
            }
        }
    }
    
    private static void Toggles_OnToggled(object s, EventArgs e)
    {
        switch (s.GetType().GetProperty("name").GetValue(s))
        {
            case "No Clip":
            {
                PhotomodePage.PhotoPage.Dispatcher.Invoke(() => PhotomodePage.PhotoPage.NoClip.IsOn = (bool)NoClipToggle.Value);
                break;
            }
            case "Unlimited Altitude":
            {
                PhotomodePage.PhotoPage.Dispatcher.Invoke(() => PhotomodePage.PhotoPage.NoheightRestriction.IsOn = (bool)UnlimitedAltitudeToggle.Value);
                break;
            }
            case "Increased Zoom":
            {
                PhotomodePage.PhotoPage.Dispatcher.Invoke(() => PhotomodePage.PhotoPage.IncreasedZoom.IsOn = (bool)IncreasedZoomToggle.Value);
                break;
            }
        }
    }
}