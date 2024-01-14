using System.Windows;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

public partial class Aero
{
    public static Aero Ae { get; private set; } = null!;
    public Aero()
    {
        InitializeComponent();
        Ae = this;
        DataContext = this;
    }
    
    private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        TuningAddresses.ChangeValue(sender);
    }
}