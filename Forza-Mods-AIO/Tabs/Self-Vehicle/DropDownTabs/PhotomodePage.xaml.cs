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
        PhotoCamEntity.NoClip = NoClip.IsOn;
    }

    /// <summary>
    ///     Increases zoom to 90x
    ///     How does it work? Replaces the MinFov 2.25 flag with 0 
    /// </summary>

    private void IncreasedZoom_OnToggled(object sender, RoutedEventArgs e)
    {
        PhotoCamEntity.IncreasedZoom = IncreasedZoom.IsOn;
    }

    /// <summary>
    ///     No max height
    ///     How does it work? Replaces the maxheight 4 flag with 9999
    /// </summary> 
    private void NoheightRestriction_Toggled(object sender, RoutedEventArgs e)
    {
        PhotoCamEntity.RemoveMaxHeight = NoheightRestriction.IsOn;
    }
    
    #endregion

    #region Mem writes
    
    private void SpeedSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        PhotoCamEntity.MovementSpeed = (float)SpeedSlider.Value;
    }

    private void SamplesMultiplierSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        PhotoCamEntity.SamplesMultiplier = (float)SamplesMultiplierSlider.Value;
    }

    private void TurnSpeed_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        PhotoCamEntity.TurnAndZoomSpeed = (float)TurnSpeed.Value;
    }

    private void SamplesBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        PhotoCamEntity.Samples = (int)SamplesBox.Value; 
    }

    private void ShutterSpeedBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        PhotoCamEntity.ShutterSpeed = (float)ShutterSpeedBox.Value; 
    }

    private void ApertureScaleBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        PhotoCamEntity.ApertureScale = (float)ApertureScaleBox.Value; 
    }

    private void CarInFocusBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        PhotoCamEntity.CarInFocus = (float)CarInFocusBox.Value; 
    }

    private void TimeSliceBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        PhotoCamEntity.TimeSlice = (float)TimeSliceBox.Value;
    }
    
    #endregion
}