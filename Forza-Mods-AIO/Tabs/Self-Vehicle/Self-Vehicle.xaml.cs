using System;
using System.Collections.Generic;
using System.Windows;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.PhotomodePage;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.TeleportsPage;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle;

/// <summary>
///     Interaction logic for Self_Vehicle.xaml
/// </summary>
public partial class SelfVehicle
{
    public static SelfVehicle Sv { get; private set; } = null!;
    public readonly UiManager UiManager;

    public SelfVehicle()
    {
        InitializeComponent();
        Sv = this;
        UiManager = new UiManager(this, AobProgressBar, Sizes, IsClicked);
        UiManager.ToggleUiElements(false);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        SelfVehicleAddresses.Scan();
        ScanButton.IsEnabled = false;
    }
    
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (!UiManager.ToggleDropDown(sender))
        {
            return;
        }
        
        var senderName = sender.GetType().GetProperty("Name")!.GetValue(sender)!.ToString()!;
        HandleOpenButton(senderName);
    }

    private static readonly Dictionary<string, double> Sizes = new()
    {
        { "HandlingButton", 465 }, // Button name for page, height of page
        { "UnlocksButton", 180 },
        { "PhotomodeButton", 285 },
        { "StatsButton", 70 },
        { "TeleportsButton", 70 },
        { "EnvironmentButton", 235 },
        { "CustomizationButton", 180 },
        { "MiscellaneousButton", 340 },
        { "FovButton", 347.5 },
        { "BackFireButton", 125 }
    };

    private static readonly Dictionary<string, bool> IsClicked = new()
    {
        { "HandlingButton", false },
        { "UnlocksButton", false },
        { "PhotomodeButton", false },
        { "StatsButton", false },
        { "TeleportsButton", false },
        { "EnvironmentButton", false },
        { "CustomizationButton", false },
        { "MiscellaneousButton", false },
        { "FovButton", false },
        { "BackFireButton", false }
    };

    private static void HandleOpenButton(string name)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        switch (name)
        {
            case "TeleportsButton" when Mw.Gvp.Name == "Forza Horizon 5":
            {
                if (Teleports.TeleportBox.Items.Contains("Guanajuato (Main City)"))
                {
                    return;
                }

                Teleports.TeleportBox.Items.Clear();
                Teleports.TeleportBox.Items.Add("Waypoint");
                Teleports.TeleportBox.Items.Add("Airstrip");
                Teleports.TeleportBox.Items.Add("Bridge");
                Teleports.TeleportBox.Items.Add("Dirt Circuit");
                Teleports.TeleportBox.Items.Add("Dunes");
                Teleports.TeleportBox.Items.Add("Golf Course");
                Teleports.TeleportBox.Items.Add("Guanajuato (Main City)");
                Teleports.TeleportBox.Items.Add("Motorway");
                Teleports.TeleportBox.Items.Add("Mulege");
                Teleports.TeleportBox.Items.Add("Playa Azul");
                Teleports.TeleportBox.Items.Add("River");
                Teleports.TeleportBox.Items.Add("Stadium");
                Teleports.TeleportBox.Items.Add("Temple");
                Teleports.TeleportBox.Items.Add("Temple Drag");
                Teleports.TeleportBox.Items.Add("Top Of Volcano");
                break;
            }
            case "TeleportsButton" when MainWindow.Mw.Gvp.Name == "Forza Horizon 4":
            {
                if (Teleports.TeleportBox.Items.Contains("Edinburgh"))
                {
                    return;
                }

                Teleports.TeleportBox.Items.Clear();
                Teleports.TeleportBox.Items.Add("Waypoint");
                Teleports.TeleportBox.Items.Add("Adventure Park");
                Teleports.TeleportBox.Items.Add("Ambleside");
                Teleports.TeleportBox.Items.Add("Beach");
                Teleports.TeleportBox.Items.Add("Broadway");
                Teleports.TeleportBox.Items.Add("Dam");
                Teleports.TeleportBox.Items.Add("Edinburgh");
                Teleports.TeleportBox.Items.Add("Festival");
                Teleports.TeleportBox.Items.Add("Greendale Airstrip");
                Teleports.TeleportBox.Items.Add("Lake Island");
                Teleports.TeleportBox.Items.Add("Mortimer Gardens");
                Teleports.TeleportBox.Items.Add("Quarry");
                Teleports.TeleportBox.Items.Add("Railyard");
                Teleports.TeleportBox.Items.Add("Start of Motorway");
                Teleports.TeleportBox.Items.Add("Top of Mountain");
                break;
            }
            case "PhotomodeButton":
            {
                PhotoPage.CarInFocusBox.Value = PhotoCamEntity.CarInFocus;
                PhotoPage.SamplesBox.Value = PhotoCamEntity.Samples;
                PhotoPage.TimeSliceBox.Value = Math.Round(PhotoCamEntity.TimeSlice, 5);
                PhotoPage.ShutterSpeedBox.Value = PhotoCamEntity.ShutterSpeed;
                PhotoPage.ApertureScaleBox.Value = PhotoCamEntity.ApertureScale;
                break;
            }
        }
    }
}