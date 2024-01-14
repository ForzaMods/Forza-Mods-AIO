using System;
using System.Windows;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

public partial class Alignment
{
    public bool CodeChange;
    public static Alignment Al { get; private set; } = null!;
    public Alignment()
    {
        InitializeComponent();
        Al = this;
        DataContext = this;
    }

    private void CamberNegBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached || CamberNegBox == null || CodeChange)
        {
            return;
        }   
        
        Mw.M.WriteMemory(TuningAddresses.CamberNeg, Convert.ToSingle(CamberNegBox.Value));
        Mw.M.WriteMemory(TuningAddresses.CamberNegStatic, Convert.ToSingle(CamberNegBox.Value));
    }

    private void CamberPosBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached || CamberPosBox == null || CodeChange)
        {
            return;
        }
        
        Mw.M.WriteMemory(TuningAddresses.CamberPos, Convert.ToSingle(CamberPosBox.Value));
        Mw.M.WriteMemory(TuningAddresses.CamberPosStatic, Convert.ToSingle(CamberPosBox.Value));
    }

    private void ToeNegBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached || ToeNegBox == null || CodeChange)
        {
            return;
        }
        Mw.M.WriteMemory(TuningAddresses.ToeNeg, Convert.ToSingle(ToeNegBox.Value));
        Mw.M.WriteMemory(TuningAddresses.ToeNegStatic, Convert.ToSingle(ToeNegBox.Value));
    }

    private void ToePosBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached || ToePosBox == null || CodeChange)
        {
            return;
        }
        Mw.M.WriteMemory(TuningAddresses.ToePos, Convert.ToSingle(ToePosBox.Value));
        Mw.M.WriteMemory(TuningAddresses.ToePosStatic, Convert.ToSingle(ToePosBox.Value));
    }
}