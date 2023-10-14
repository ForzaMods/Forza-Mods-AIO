using System.Windows;
using System.Windows.Controls;

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
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.NoClipAddr, NoClip.IsOn ? 0 : 2);
    }

    /// <summary>
    ///     Increases zoom to 90x
    ///     How does it work? Replaces the MinFov 2.25 flag with 0 
    /// </summary>

    private void IncreasedZoom_OnToggled(object sender, RoutedEventArgs e)
    {
        MainWindow.mw.m.WriteMemory<float>(Self_Vehicle_Addrs.MinFovAddr, IncreasedZoom.IsOn ? 0 : (float)2.25 );
    }

    /// <summary>
    ///     No max height
    ///     How does it work? Replaces the maxheight 4 flag with 9999
    /// </summary> 
    private void NoheightRestriction_Toggled(object sender, RoutedEventArgs e)
    {
        MainWindow.mw.m.WriteMemory<float>(Self_Vehicle_Addrs.MaxHeightAddr, NoheightRestriction.IsOn ? 9999 : 4);
    }
    
    #endregion

    #region Mem writes
    
    private void SpeedSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try { MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.MovementSpeed, (float)SpeedSlider.Value);} catch { }
    }

    private void SamplesMultiplierSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try { MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.SamplesMultiplier, (float)SamplesMultiplierSlider.Value);} catch {}
    }

    private void TurnSpeed_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try { MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.TurnAndZoomSpeed, (float)TurnSpeed.Value); } catch {}
    }

    private void SamplesBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.Samples, (int)SamplesBox.Value);
    }

    private void ShutterSpeedBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.ShutterSpeed, (float)ShutterSpeedBox.Value);
    }

    private void ApertureScaleBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.ApertureScale, (float)ApertureScaleBox.Value);
    }

    private void CarInFocusBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.CarInFocus, (float)CarInFocusBox.Value);
    }

    private void TimeSliceBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.TimeSlice, (float)TimeSliceBox.Value);
    }
    
    #endregion
}