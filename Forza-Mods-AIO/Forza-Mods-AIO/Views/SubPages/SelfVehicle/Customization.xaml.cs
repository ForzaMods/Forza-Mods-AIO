using System.Numerics;
using System.Windows;
using System.Windows.Media;
using Forza_Mods_AIO.Models;
using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Views.SubPages.SelfVehicle;

public partial class Customization
{
    #if DEBUG
    private readonly DebugSession _headlightColorDebug = new("Headlight Color", [], []);
    #endif
    
    public Customization()
    {
        DataContext = this;
        
        InitializeComponent();
        #if DEBUG
        DebugSessions.GetInstance().EveryDebugSession.Add(_headlightColorDebug);
        #endif
    }

    private void ColorPickerBase_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
    {
        if (ColorPicker == null)
        {
            return;
        }

        #if DEBUG
        var a = ConvertUiColorToGameValues(e.NewValue.GetValueOrDefault());
        _headlightColorDebug.DebugInfoReports.Add(new DebugInfoReport($"Value: {a.ToString()}"));
        #endif
    }

    private static Vector3 ConvertUiColorToGameValues(Color uiColor)
    {
        var alpha = uiColor.A / 255f;
        var red = uiColor.R / 255f * alpha;
        var green = uiColor.G / 255f * alpha;
        var blue = uiColor.B / 255f * alpha;
        return new Vector3(red, green, blue);        
    }
}