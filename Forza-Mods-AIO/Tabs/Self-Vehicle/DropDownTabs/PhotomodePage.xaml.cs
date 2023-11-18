using System.Windows;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class PhotomodePage
{
    public static PhotomodePage PhotoPage;

    public PhotomodePage()
    {
        InitializeComponent();
        PhotoPage = this;
    }

    #region Toggles
    
    /// <summary>
    ///     No clip toggle.
    ///     How does it work? Replace the collison 2 flag with 0.
    /// </summary>
    private void NoClip_Toggled(object sender, RoutedEventArgs e)
    {
        try
        {
            PhotoCamEntity.NoClip = NoClip.IsOn;
        }
        catch {}
    }

    /// <summary>
    ///     Increases zoom to 90x
    ///     How does it work? Replaces the MinFov 2.25 flag with 0 
    /// </summary>

    private void IncreasedZoom_OnToggled(object sender, RoutedEventArgs e)
    {
        try
        {
            PhotoCamEntity.IncreasedZoom = IncreasedZoom.IsOn;
        }
        catch {}
    }

    /// <summary>
    ///     No max height
    ///     How does it work? Replaces the maxheight 4 flag with 9999
    /// </summary> 
    private void NoheightRestriction_Toggled(object sender, RoutedEventArgs e)
    {
        try
        {
            PhotoCamEntity.RemoveMaxHeight = NoheightRestriction.IsOn;
        }
        catch {}
    }
    
    #endregion

    #region Mem writes
    
    private void SpeedSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try
        {
            PhotoCamEntity.MovementSpeed = (float)SpeedSlider.Value;
        }
        catch {}
    }

    private void SamplesMultiplierSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try
        {
            PhotoCamEntity.SamplesMultiplier = (float)SamplesMultiplierSlider.Value;
        }
        catch {}
    }

    private void TurnSpeed_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try
        {
            PhotoCamEntity.TurnAndZoomSpeed = (float)TurnSpeed.Value;
        }
        catch{}
    }

    private void SamplesBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try
        {
            PhotoCamEntity.Samples = (int)SamplesBox.Value; 
        }
        catch {}
    }

    private void ShutterSpeedBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try
        {
            PhotoCamEntity.ShutterSpeed = (float)ShutterSpeedBox.Value; 
        }
        catch {}
    }

    private void ApertureScaleBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try
        {
            PhotoCamEntity.ApertureScale = (float)ApertureScaleBox.Value; 
        }
        catch {}
    }

    private void CarInFocusBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try
        {
            PhotoCamEntity.CarInFocus = (float)CarInFocusBox.Value; 
        }
        catch {}
    }

    private void TimeSliceBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try
        {
            PhotoCamEntity.TimeSlice = (float)TimeSliceBox.Value;
        }
        catch {}
    }
    
    #endregion
}