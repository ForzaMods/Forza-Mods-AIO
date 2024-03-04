using System.Numerics;
using System.Windows;
using System.Windows.Media;
using Forza_Mods_AIO.Models;
using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Views.SubPages.SelfVehicle;

public partial class Customization
{
    private static readonly DebugBreakpoint HeadlightColorDebugBreakpoint = new("Test");
    private readonly DebugSession _headlightColorDebug = new("Headlight Color", [], [HeadlightColorDebugBreakpoint]);
    
    public Customization()
    {
        DataContext = this;
        
        InitializeComponent();
        DebugSessions.GetInstance().EveryDebugSession.Add(_headlightColorDebug);
    }

    private async void ColorPickerBase_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
    {
        if (ColorPicker == null)
        {
            return;
        }

        var a = ConvertUiColorToGameValues(e.NewValue.GetValueOrDefault());
        await HeadlightColorDebugBreakpoint.MarkAsHit();
        _headlightColorDebug.DebugInfoReports.Add(new DebugInfoReport($"Value: {a.ToString()}"));
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