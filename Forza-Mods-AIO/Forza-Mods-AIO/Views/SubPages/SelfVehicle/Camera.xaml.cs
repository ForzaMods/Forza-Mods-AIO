using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.SelfVehicle;

public partial class Camera
{
    public Camera()
    {
        ViewModel = new CameraViewModel();
        DataContext = this;
        
        InitializeComponent();
    }
    
    public CameraViewModel ViewModel { get; }
    private static CameraCheats CameraCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<CameraCheats>();

    private async void LimitersScanButton_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.AreScanPromptLimiterUiElementsVisible = false;
        ViewModel.AreScanningLimiterUiElementsVisible = true;

        if (!CameraCheatsFh5.WereLimitersScanned)
        {
            await CameraCheatsFh5.CheatLimiters();
        }

        if (!CameraCheatsFh5.WereLimitersScanned)
        {
            ViewModel.AreScanPromptLimiterUiElementsVisible = true;
            ViewModel.AreScanningLimiterUiElementsVisible = false;
            return;
        }

        LimiterMinBox.Value = GetInstance().ReadMemory<float>(CameraCheatsFh5.ChaseAddress);
        LimiterMaxBox.Value = GetInstance().ReadMemory<float>(CameraCheatsFh5.ChaseAddress + 4);

        ViewModel.AreScanningLimiterUiElementsVisible = false;
        ViewModel.AreLimiterUiElementsVisible = true;
    }

    private void LimiterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox || !CameraCheatsFh5.WereLimitersScanned)
        {
            return;
        }

        LimiterMinBox.Value = comboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<float>(CameraCheatsFh5.ChaseAddress),
            1 => GetInstance().ReadMemory<float>(CameraCheatsFh5.ChaseFarAddress),
            2 => GetInstance().ReadMemory<float>(CameraCheatsFh5.DriverAddress),
            3 => GetInstance().ReadMemory<float>(CameraCheatsFh5.HoodAddress),
            4 => GetInstance().ReadMemory<float>(CameraCheatsFh5.BumperAddress),
            _ => 0
        };

        LimiterMaxBox.Value = comboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<float>(CameraCheatsFh5.ChaseAddress + 4),
            1 => GetInstance().ReadMemory<float>(CameraCheatsFh5.ChaseFarAddress + 4),
            2 => GetInstance().ReadMemory<float>(CameraCheatsFh5.DriverAddress + 4),
            3 => GetInstance().ReadMemory<float>(CameraCheatsFh5.HoodAddress + 4),
            4 => GetInstance().ReadMemory<float>(CameraCheatsFh5.BumperAddress + 4),
            _ => 0
        };
    }

    private void LimiterMinBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown || !CameraCheatsFh5.WereLimitersScanned)
        {
            return;
        }

        var newValue = Convert.ToSingle(numericUpDown.Value);
        switch (LimiterComboBox.SelectedIndex)
        {
            case 0:
            {
                GetInstance().WriteMemory(CameraCheatsFh5.ChaseAddress, newValue);
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(CameraCheatsFh5.ChaseFarAddress, newValue);
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(CameraCheatsFh5.DriverAddress, newValue);
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(CameraCheatsFh5.HoodAddress, newValue);
                break;
            }
            case 4:
            {
                GetInstance().WriteMemory(CameraCheatsFh5.BumperAddress, newValue);
                break;
            }
        }
    }

    private void LimiterMaxBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown || !CameraCheatsFh5.WereLimitersScanned)
        {
            return;
        }

        var newValue = Convert.ToSingle(numericUpDown.Value);
        switch (LimiterComboBox.SelectedIndex)
        {
            case 0:
            {
                GetInstance().WriteMemory(CameraCheatsFh5.ChaseAddress + 4, newValue);
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(CameraCheatsFh5.ChaseFarAddress + 4, newValue);
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(CameraCheatsFh5.DriverAddress + 4, newValue);
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(CameraCheatsFh5.HoodAddress + 4, newValue);
                break;
            }
            case 4:
            {
                GetInstance().WriteMemory(CameraCheatsFh5.BumperAddress + 4, newValue);
                break;
            }
        }
    }

    private async void FovLockSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreCameraHookUiElementsEnabled = false;
        if (CameraCheatsFh5.CameraDetourAddress == 0)
        {
            await CameraCheatsFh5.CheatCamera();
        }
        ViewModel.AreCameraHookUiElementsEnabled = true;
        
        if (CameraCheatsFh5.CameraDetourAddress == 0) return;
        GetInstance().WriteMemory(CameraCheatsFh5.CameraDetourAddress + 0x59, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(CameraCheatsFh5.CameraDetourAddress + 0x5A, Convert.ToSingle(FovLockSlider.Value) / 10);
    }

    private void FovLockSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (sender is not Slider slider)
        {
            return;
        }
        
        if (CameraCheatsFh5.CameraDetourAddress == 0) return;
        GetInstance().WriteMemory(CameraCheatsFh5.CameraDetourAddress + 0x5A, Convert.ToSingle(slider.Value) / 10);
    }

    private async void OffsetSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreCameraHookUiElementsEnabled = false;
        if (CameraCheatsFh5.CameraDetourAddress == 0)
        {
            await CameraCheatsFh5.CheatCamera();
        }
        ViewModel.AreCameraHookUiElementsEnabled = true;

        if (CameraCheatsFh5.CameraDetourAddress == 0) return;
        GetInstance().WriteMemory(CameraCheatsFh5.CameraDetourAddress + 0x5E, toggleSwitch.IsOn ? (byte)1 : (byte)0);

        var write = new Vector3(
            Convert.ToSingle(XValueBox.Value),
            Convert.ToSingle(YValueBox.Value),
            Convert.ToSingle(ZValueBox.Value)
        );
        
        GetInstance().WriteMemory(CameraCheatsFh5.CameraDetourAddress + 0x5F, write);
    }

    private void OffsetBoxes_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (CameraCheatsFh5.CameraDetourAddress == 0) return;
        
        var write = new Vector3(
            Convert.ToSingle(XValueBox.Value),
            Convert.ToSingle(YValueBox.Value),
            Convert.ToSingle(ZValueBox.Value)
        );
        
        GetInstance().WriteMemory(CameraCheatsFh5.CameraDetourAddress + 0x5F, write);
    }
}