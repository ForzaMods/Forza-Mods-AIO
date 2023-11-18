using System;
using System.Collections.Generic;
using System.Windows;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle;

/// <summary>
///     Interaction logic for Self_Vehicle.xaml
/// </summary>
public partial class SelfVehicle
{
    public static SelfVehicle? Sv;

    public SelfVehicle()
    {
        InitializeComponent();
        Sv = this;
        UpdateUi.UpdateUI(false, this);
    }
    
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (!UpdateUi.AnimCompleted) return;
        var senderName = sender.GetType().GetProperty("Name")!.GetValue(sender)!.ToString()!;
        UpdateUi.Animate(sender, IsClicked[senderName], Sizes, IsClicked, this);
        IsClicked[senderName] = !IsClicked[senderName];

        if (!IsClicked[senderName]) return;
        
        switch (senderName)
        {
            case "TeleportsButton" when MainWindow.Mw.Gvp.Name == "Forza Horizon 5":
            {
                if (DropDownTabs.TeleportsPage.T.TeleportBox.Items.Contains("Guanajuato (Main City)"))
                {
                    return;
                }

                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Clear();
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Waypoint");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Airstrip");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Bridge");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Dirt Circuit");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Dunes");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Golf Course");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Guanajuato (Main City)");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Motorway");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Mulege");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Playa Azul");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("River");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Stadium");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Temple");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Temple Drag");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Top Of Volcano");
                break;
            }
            case "TeleportsButton" when MainWindow.Mw.Gvp.Name == "Forza Horizon 4":
            {
                if (DropDownTabs.TeleportsPage.T.TeleportBox.Items.Contains("Edinburgh"))
                {
                    return;
                }

                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Clear();
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Waypoint");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Adventure Park");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Ambleside");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Beach");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Broadway");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Dam");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Edinburgh");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Festival");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Greendale Airstrip");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Lake Island");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Mortimer Gardens");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Quarry");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Railyard");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Start of Motorway");
                DropDownTabs.TeleportsPage.T.TeleportBox.Items.Add("Top of Mountain");
                break;
            }
            case "PhotomodeButton":
            {
                DropDownTabs.PhotomodePage.PhotoPage.CarInFocusBox.Value = PhotoCamEntity.CarInFocus;
                DropDownTabs.PhotomodePage.PhotoPage.SamplesBox.Value = PhotoCamEntity.Samples;
                DropDownTabs.PhotomodePage.PhotoPage.TimeSliceBox.Value = Math.Round(PhotoCamEntity.TimeSlice, 5);
                DropDownTabs.PhotomodePage.PhotoPage.ShutterSpeedBox.Value = PhotoCamEntity.ShutterSpeed;
                DropDownTabs.PhotomodePage.PhotoPage.ApertureScaleBox.Value = PhotoCamEntity.ApertureScale;
                break;
            }
        }
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
        { "MiscellaneousButton", 70 },
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
}