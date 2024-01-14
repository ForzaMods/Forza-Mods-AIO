using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

public partial class Steering
{
    public static Steering St { get; private set; } = null!;

    public Steering()
    {
        InitializeComponent();
        St = this;
    }

    private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        TuningAddresses.ChangeValue(sender);
    }
}