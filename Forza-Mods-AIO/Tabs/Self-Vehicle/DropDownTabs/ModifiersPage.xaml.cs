using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs
{
    /// <summary>
    /// Interaction logic for SpeedHacksPage.xaml
    /// </summary>

    public partial class ModifiersPage : Page
    {
        public static ModifiersPage mp;
        public ModifiersPage()
        {
            InitializeComponent();
            mp = this;
        }

        public void PullButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType().GetProperty("Name").GetValue(sender).ToString().Contains("Gravity"))
                try { GravityValueNum.Value = MainWindow.mw.m.ReadFloat(Self_Vehicle_Addrs.GravityAddr, round: false); } catch { }
            else
                try { AccelerationValueNum.Value = MainWindow.mw.m.ReadFloat(Self_Vehicle_Addrs.WeirdAddr, round: false); } catch { }
        }

        private void SetSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            float original;
            string Addr;
            string Type;
            if (sender.GetType().GetProperty("Name").GetValue(sender).ToString().Contains("Gravity"))
            {
                Type = "Gravity";
                Addr = Self_Vehicle_Addrs.GravityAddr;
            }
            else
            {
                Type = "Acceleration";
                Addr = Self_Vehicle_Addrs.WeirdAddr;
            }

            if ((bool)sender.GetType().GetProperty("IsOn").GetValue(sender))
            {
                Task.Run(() =>
                {
                    original = MainWindow.mw.m.ReadFloat(Addr, round: false);
                    while (true)
                    {
                        bool Toggled = true;
                        if (Type == "Gravity")
                            ModifiersPage.mp.Dispatcher.Invoke(delegate () { Toggled = (bool)ModifiersPage.mp.GravitySetSwitch.GetType().GetProperty("IsOn").GetValue(ModifiersPage.mp.GravitySetSwitch); });
                        else
                            ModifiersPage.mp.Dispatcher.Invoke(delegate () { Toggled = (bool)ModifiersPage.mp.AccelerationSetSwitch.GetType().GetProperty("IsOn").GetValue(ModifiersPage.mp.AccelerationSetSwitch); });

                        if (!Toggled)
                        {
                            if (Type == "Gravity")
                                ModifiersPage.mp.Dispatcher.Invoke(delegate () { ModifiersPage.mp.GravityValueNum.GetType().GetProperty("Value").SetValue(ModifiersPage.mp.GravityValueNum, Convert.ToDouble(original)); });
                            else
                                ModifiersPage.mp.Dispatcher.Invoke(delegate () { ModifiersPage.mp.AccelerationValueNum.GetType().GetProperty("Value").SetValue(ModifiersPage.mp.AccelerationValueNum, Convert.ToDouble(original)); });
                            MainWindow.mw.m.WriteMemory(Addr, "float", original.ToString());
                            break;
                        }

                        try
                        {
                            float SetValue = 0;
                            if (Type == "Gravity")
                                ModifiersPage.mp.Dispatcher.Invoke(delegate () { SetValue = Convert.ToSingle(ModifiersPage.mp.GravityValueNum.GetType().GetProperty("Value").GetValue(ModifiersPage.mp.GravityValueNum)); });
                            else
                                ModifiersPage.mp.Dispatcher.Invoke(delegate () { SetValue = Convert.ToSingle(ModifiersPage.mp.AccelerationValueNum.GetType().GetProperty("Value").GetValue(ModifiersPage.mp.AccelerationValueNum)); });

                            if (MainWindow.mw.m.ReadFloat(Addr) != SetValue)
                            {
                                MainWindow.mw.m.WriteMemory(Addr, "float", SetValue.ToString());
                            }
                            Thread.Sleep(1);
                        }
                        catch
                        {
                            //Car Changed
                            while (true)
                            {
                                try
                                {
                                    original = MainWindow.mw.m.ReadFloat(Addr);
                                    break;
                                }
                                catch { }
                            }
                        }
                    }
                });
            }
        }
    }
}
