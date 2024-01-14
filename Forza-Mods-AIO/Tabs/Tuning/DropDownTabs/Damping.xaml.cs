using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

public partial class Damping
{
    public static Damping D { get; private set; } = null!;
    public Damping()
    {
        InitializeComponent();
        D = this;
    }

    private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        TuningAddresses.ChangeValue(sender);
    }
}