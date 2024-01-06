using System.Windows;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;
using static System.Convert;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class PhotomodePage
{
    public static PhotomodePage PhotoPage { get; private set; } = null!;

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
        if (!Mw.Attached)
        {
            return;
        }

        PhotoCamEntity.NoClip = NoClip.IsOn;
    }

    /// <summary>
    ///     Increases zoom to 90x
    ///     How does it work? Replaces the MinFov 2.25 flag with 0 
    /// </summary>

    private void IncreasedZoom_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        PhotoCamEntity.IncreasedZoom = IncreasedZoom.IsOn;
    }

    /// <summary>
    ///     No max height
    ///     How does it work? Replaces the MaxHeight 4 flag with 9999
    /// </summary> 
    private void NoHeightRestriction_Toggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        PhotoCamEntity.RemoveMaxHeight = NoheightRestriction.IsOn;
    }
    
    #endregion

    #region Mem writes
    
    private void SpeedSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        PhotoCamEntity.MovementSpeed = (float)SpeedSlider.Value;
    }

    private void SamplesMultiplierSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (!Mw.Attached)
        {
            return;
        }
           
        PhotoCamEntity.SamplesMultiplier = (float)SamplesMultiplierSlider.Value;
    }

    private void TurnSpeed_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        PhotoCamEntity.TurnAndZoomSpeed = (float)TurnSpeed.Value;
    }

    private void SamplesBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        PhotoCamEntity.Samples = ToInt32(SamplesBox.Value); 
    }

    private void ShutterSpeedBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        PhotoCamEntity.ShutterSpeed = ToSingle(ShutterSpeedBox.Value); 
    }

    private void ApertureScaleBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        PhotoCamEntity.ApertureScale = ToSingle(ApertureScaleBox.Value); 
    }

    private void CarInFocusBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        PhotoCamEntity.CarInFocus = ToSingle(CarInFocusBox.Value); 
    }

    private void TimeSliceBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        PhotoCamEntity.TimeSlice = ToSingle(TimeSliceBox.Value);
    }
    
    #endregion
}