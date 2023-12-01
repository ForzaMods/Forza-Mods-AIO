using System;
using System.Collections.Generic;
using System.Windows;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.PhotomodePage;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.TeleportsPage;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle;

/// <summary>
///     Interaction logic for Self_Vehicle.xaml
/// </summary>
public partial class SelfVehicle
{
    public static SelfVehicle? Sv;
    public readonly UiManager UiManager;

    public SelfVehicle()
    {
        InitializeComponent();
        Sv = this;
        UiManager = new UiManager(this, AobProgressBar, Sizes, IsClicked);
        UiManager.ToggleUiElements(false);
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
        { "UnlocksButton", 235 },
        { "PhotomodeButton", 285 },
        { "StatsButton", 70 },
        { "TeleportsButton", 70 },
        { "EnvironmentButton", 235 },
        { "CustomizationButton", 70 },
        { "MiscellaneousButton", 290 },
        { "FovButton", 347.5 }
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
        { "FovButton", false }
    };

    private static void HandleOpenButton(string name)
    {
        switch (name)
        {
            case "TeleportsButton" when MainWindow.Mw.Gvp.Name == "Forza Horizon 5":
            {
                if (T.TeleportBox.Items.Contains("Guanajuato (Main City)"))
                {
                    return;
                }

                T.TeleportBox.Items.Clear();
                T.TeleportBox.Items.Add("Waypoint");
                T.TeleportBox.Items.Add("Airstrip");
                T.TeleportBox.Items.Add("Bridge");
                T.TeleportBox.Items.Add("Dirt Circuit");
                T.TeleportBox.Items.Add("Dunes");
                T.TeleportBox.Items.Add("Golf Course");
                T.TeleportBox.Items.Add("Guanajuato (Main City)");
                T.TeleportBox.Items.Add("Motorway");
                T.TeleportBox.Items.Add("Mulege");
                T.TeleportBox.Items.Add("Playa Azul");
                T.TeleportBox.Items.Add("River");
                T.TeleportBox.Items.Add("Stadium");
                T.TeleportBox.Items.Add("Temple");
                T.TeleportBox.Items.Add("Temple Drag");
                T.TeleportBox.Items.Add("Top Of Volcano");
                break;
            }
            case "TeleportsButton" when MainWindow.Mw.Gvp.Name == "Forza Horizon 4":
            {
                if (T.TeleportBox.Items.Contains("Edinburgh"))
                {
                    return;
                }

                T.TeleportBox.Items.Clear();
                T.TeleportBox.Items.Add("Waypoint");
                T.TeleportBox.Items.Add("Adventure Park");
                T.TeleportBox.Items.Add("Ambleside");
                T.TeleportBox.Items.Add("Beach");
                T.TeleportBox.Items.Add("Broadway");
                T.TeleportBox.Items.Add("Dam");
                T.TeleportBox.Items.Add("Edinburgh");
                T.TeleportBox.Items.Add("Festival");
                T.TeleportBox.Items.Add("Greendale Airstrip");
                T.TeleportBox.Items.Add("Lake Island");
                T.TeleportBox.Items.Add("Mortimer Gardens");
                T.TeleportBox.Items.Add("Quarry");
                T.TeleportBox.Items.Add("Railyard");
                T.TeleportBox.Items.Add("Start of Motorway");
                T.TeleportBox.Items.Add("Top of Mountain");
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