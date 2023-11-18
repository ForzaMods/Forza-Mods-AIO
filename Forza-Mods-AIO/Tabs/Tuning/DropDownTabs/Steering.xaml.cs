using System.Linq;
using System.Reflection;
using System.Windows;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

public partial class Steering
{
    public static Steering? St;

    public Steering()
    {
        InitializeComponent();
        St = this;
    }

    private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try
        {
            // Got this looping method from here and slightly modified it: https://stackoverflow.com/a/51424624
            foreach (FieldInfo address in typeof(TuningAddresses).GetFields(BindingFlags.Public | BindingFlags.Static).Where(field => field.FieldType == typeof(string)))
            {
                var senderName = sender.GetType().GetProperty("Name").GetValue(sender).ToString();
                if (address.Name == senderName.Remove(senderName.Length - 3))
                {
                    MainWindow.Mw.M.WriteMemory(address.GetValue(null) as string, (float)((MahApps.Metro.Controls.NumericUpDown)sender).Value);
                }
            }
        }
        catch { }
    }
}