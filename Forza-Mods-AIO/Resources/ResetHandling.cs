using System.Windows;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Features;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Resources;

public abstract class ResetHandling
{
    public static void ResetAio()
    {
        ClearDetours();
        ResetUi();
        ResetMisc();
    }

    private static void ResetMisc()
    {
        Overlay.Overlay.AutoshowGarageOption.IsEnabled = false;
        Overlay.Overlay.SelfVehicleOption.IsEnabled = false;
        Overlay.Overlay.TuningOption.IsEnabled = false;
        
        Mw.M._memoryCache.Clear();
        Mw.M._signatureResultCache.Clear();
    }

    private static void ResetUi()
    {
        Mw.Dispatcher.Invoke(() =>
        {
#if RELEASE
            Mw.AttachedLabel.Content = "Launch FH4, FH5 or FM8";
#endif
            Tabs.Tuning.Tuning.T.AobProgressBar.Value = 0;
            Tabs.Self_Vehicle.SelfVehicle.Sv.AobProgressBar.Value = 0;
            Tabs.AutoShowTab.AutoShow.As.AobProgressBar.Value = 0;
            Tabs.AIO_Info.AioInfo.Ai.OverlaySwitch.IsOn = false;
            Tabs.AIO_Info.AioInfo.Ai.OverlaySwitch.IsEnabled = false;
            Mw.AioInfo.IsChecked = true;
            Mw.CategoryButton_Click(Mw.AioInfo, new RoutedEventArgs());

            foreach (var visual in Mw.Window.GetChildren())
            {
                var element = (FrameworkElement)visual;

                if (element.GetType() != typeof(ToggleSwitch))
                {
                    continue;
                }

                ((ToggleSwitch)element).IsOn = false;
            }
            
            Tabs.Tuning.Tuning.T.ScanButton.IsEnabled = true;
            Tabs.Self_Vehicle.SelfVehicle.Sv.ScanButton.IsEnabled = true;
            Tabs.AutoShowTab.AutoShow.As.ScanButton.IsEnabled = true;
            Tabs.Tuning.Tuning.T.UiManager.Reset();
            Tabs.Self_Vehicle.SelfVehicle.Sv.UiManager.Reset();
        });

        Mw.ToggleButtons(false);
        
        Tabs.Tuning.Tuning.T.UiManager.ToggleUiElements(false);
        Tabs.Self_Vehicle.SelfVehicle.Sv.UiManager.ToggleUiElements(false);
        Tabs.AutoShowTab.AutoShow.As.UiManager.ToggleUiElements(false);
    }


    private static void ClearDetours()
    {
        EncryptedValues.EncryptedValuesDetour.Clear();
        EncryptedValues.EncryptedEntries.Clear();
        UnlocksPage.CrEntry = null!;
        UnlocksPage.XpEntry = null!;
        CustomizationPage.GlowingPaintDetour.Clear();
        CustomizationPage.HeadlightDetour.Clear();
        CustomizationPage.CleanlinessDetour.Clear();
        CustomizationPage.ForceLodDetour.Clear();
        CustomizationPage.LodCmpDetour.Clear();
        CameraPage.CameraDetour.Clear();
        EnvironmentPage.TimeDetour.Clear();
        EnvironmentPage.FreezeAiDetour.Clear();
        LocatorEntity.WaypointDetour.Clear();
        CarEntity.BaseDetour.Clear();
        MiscellaneousPage.ScaleDetour.Clear();
        MiscellaneousPage.SellDetour.Clear();
        MiscellaneousPage.Build1Detour.Clear();
        MiscellaneousPage.Build2Detour.Clear();
        MiscellaneousPage.SkillTreeDetour.Clear();
        MiscellaneousPage.ScoreDetour.Clear();
        MiscellaneousPage.SkillCostDetour.Clear();
        MiscellaneousPage.DriftDetour.Clear();
        MiscellaneousPage.TimeScaleDetour.Clear();
        BackFirePage.BackFire.BackfireTimeDetour.Clear();
        BackFirePage.BackFire.BackfireTypeDetour.Clear();
        MiscellaneousPage.MiscPage.WasSkillDetoured = false;
        EnvironmentPage.WasTimeDetoured = false;
        TeleportsPage.WaypointDetoured = false;
        Bypass.Clear();
    }
}