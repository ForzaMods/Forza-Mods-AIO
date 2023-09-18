using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class CameraPage : Page
{
    public static CameraPage CamPage;

    public CameraPage()
    {
        InitializeComponent();
        CamPage = this;
    }

    private void NoClip_Toggled(object sender, RoutedEventArgs e)
    {
        BoundaryRemoval.IsEnabled = !BoundaryRemoval.IsEnabled;

        if (NoClip.IsOn)
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.NoClipAddr, "int", "0");
        else
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.NoClipAddr, "int", "2");
    }

    private void SpeedSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.MovementSpeed, "float", SpeedSlider.Value.ToString());
    }

    private void SamplesMultiplierSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.SamplesMultiplier, "float",
            SamplesMultiplierSlider.Value.ToString());
    }

    private void TurnSpeed_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.TurnAndZoomSpeed, "float", TurnSpeed.Value.ToString());
    }

    private void SamplesBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.Samples, "int", SamplesBox.Value.ToString());
    }

    private void ShutterSpeedBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.ShutterSpeed, "float", ShutterSpeedBox.Value.ToString());
    }

    private void ApertureScaleBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.ApertureScale, "float", ApertureScaleBox.Value.ToString());
    }

    private void CarInFocusBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.CarInFocus, "float", CarInFocusBox.Value.ToString());
    }

    private void TimeSliceBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.TimeSlice, "float", TimeSliceBox.Value.ToString());
    }

    private void NoheightRestriction_Toggled(object sender, RoutedEventArgs e)
    {
        if (NoheightRestriction.IsOn)
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.MaxHeightAddr, "float", "9999");
        else
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.MaxHeightAddr, "float", "4");
    }

    private void BoundaryRemoval_Toggled(object sender, RoutedEventArgs e)
    {
        NoClip.IsEnabled = !NoClip.IsEnabled;

        if (BoundaryRemoval.IsOn)
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.BoundaryRemovalAddr, "float", "99999");
        else
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.BoundaryRemovalAddr, "float", "100");
    }
}