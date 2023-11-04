using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle;

/// <summary>
///     Interaction logic for Self_Vehicle.xaml
/// </summary>
public partial class Self_Vehicle : Page
{
    public static Self_Vehicle sv;

    public Self_Vehicle()
    {
        InitializeComponent();
        sv = this;
        UpdateUi.UpdateUI(false, this);
    }
    
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (!UpdateUi.AnimCompleted) return;
        var SenderName = sender.GetType().GetProperty("Name")!.GetValue(sender)!.ToString()!;
        UpdateUi.Animate(sender, IsClicked[SenderName], Sizes, IsClicked, this);
        IsClicked[SenderName] = !IsClicked[SenderName];

        if (!IsClicked[SenderName]) return;
        
        switch (SenderName)
        {
            case "TeleportsButton" when MainWindow.mw.gvp.Name == "Forza Horizon 5":
            {
                if (DropDownTabs.TeleportsPage.t.TeleportBox.Items.Contains("Guanajuato (Main City)"))
                {
                    return;
                }

                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Clear();
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Waypoint");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Airstrip");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Bridge");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Dirt Circuit");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Dunes");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Golf Course");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Guanajuato (Main City)");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Motorway");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Mulege");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Playa Azul");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("River");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Stadium");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Temple");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Temple Drag");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Top Of Volcano");
                break;
            }
            case "TeleportsButton" when MainWindow.mw.gvp.Name == "Forza Horizon 4":
            {
                if (DropDownTabs.TeleportsPage.t.TeleportBox.Items.Contains("Edinburgh"))
                {
                    return;
                }

                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Clear();
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Waypoint");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Adventure Park");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Ambleside");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Beach");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Broadway");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Dam");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Edinburgh");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Festival");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Greendale Airstrip");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Lake Island");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Mortimer Gardens");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Quarry");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Railyard");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Start of Motorway");
                DropDownTabs.TeleportsPage.t.TeleportBox.Items.Add("Top of Mountain");
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
        { "HandlingButton", 464 }, // Button name for page, height of page
        { "UnlocksButton", 180 },
        { "PhotomodeButton", 285 },
        { "StatsButton", 70 },
        { "TeleportsButton", 70 },
        { "EnvironmentButton", 235 },
        { "CustomizationButton", 70 },
        { "MiscellaneousButton", 70 },
        { "FovButton", 347.5 }
    };

    private static Dictionary<string, bool> IsClicked = new()
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