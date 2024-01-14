using System;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;
using MahApps.Metro.Controls;
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
 
    private void NoClip_Toggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        PhotoCamEntity.NoClip = toggleSwitch.IsOn;
    }

    private void IncreasedZoom_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        PhotoCamEntity.IncreasedZoom = toggleSwitch.IsOn;
    }

    private void NoHeightRestriction_Toggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        PhotoCamEntity.RemoveMaxHeight = toggleSwitch.IsOn;
    }

    public void UpdateVariable(bool numPartOnly = false)
    {
        if (NumPartBox != null)
        {
            NumPartBox.Value = NumParameterBox.SelectedIndex switch
            {
                0 => PhotoCamEntity.Samples,
                1 => PhotoCamEntity.ShutterSpeed,
                2 => PhotoCamEntity.ApertureScale,
                3 => PhotoCamEntity.CarInFocus,
                4 => PhotoCamEntity.TimeSlice,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        if (SliderPartNum == null || numPartOnly)
        {
            return;
        }

        SliderPartNum.Value = SliderParameterBox.SelectedIndex switch
        {
            0 => PhotoCamEntity.TurnAndZoomSpeed,
            1 => PhotoCamEntity.SamplesMultiplier,
            2 => PhotoCamEntity.MovementSpeed,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void NumPartBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached || sender is not NumericUpDown numericUpDown)
        {
            return;
        }

        switch (NumParameterBox.SelectedIndex)
        {
            case 0:
                PhotoCamEntity.Samples = ToInt32(numericUpDown.Value);
                break;
            case 1:
                PhotoCamEntity.ShutterSpeed = ToSingle(numericUpDown.Value);
                break;
            case 2:
                PhotoCamEntity.ApertureScale = ToSingle(numericUpDown.Value);
                break;
            case 3:
                PhotoCamEntity.CarInFocus = ToSingle(numericUpDown.Value);
                break;
            case 4:
                PhotoCamEntity.TimeSlice = ToSingle(numericUpDown.Value);
                break;
        }
    }

    private void NumParameterBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateVariable(true);
    }

    private void SliderPartNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (Slider == null)
        {
            return;
        }
        
        Slider.Value = ToDouble(e.NewValue);
    }

    private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (SliderPartNum == null)
        {
            return;
        }

        SliderPartNum.Value = e.NewValue;

        if (!Mw.Attached)
        {
            return;
        }

        switch (SliderParameterBox.SelectedIndex)
        {
            case 0:
                PhotoCamEntity.TurnAndZoomSpeed = ToSingle(e.NewValue);
                break;
            case 1:
                PhotoCamEntity.SamplesMultiplier = ToSingle(e.NewValue);
                break;
            case 2:
                PhotoCamEntity.MovementSpeed = ToSingle(e.NewValue);
                break;
        }
    }

    private void SliderParameterBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateVariable(true);
    }
}