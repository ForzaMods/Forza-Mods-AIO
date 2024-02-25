using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Forza_Mods_AIO.Models;

public sealed class DebugSession : INotifyPropertyChanged
{
    public DebugSession(string name, ObservableCollection<DebugInfoReport> debugInfoReports, ObservableCollection<DebugBreakpoint> debugBreakpoints)
    {
        Name = name;
        DebugInfoReports = debugInfoReports;
        DebugBreakpoints = debugBreakpoints;

        foreach (var breakpoint in DebugBreakpoints)
        {
            breakpoint.PropertyChanged += BreakpointOnPropertyChanged;
        }
    }

    private void BreakpointOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        IsAnyBreakpointHit = DebugBreakpoints.Any(breakpoint => breakpoint.IsHit);
    }

    public string Name { get; }
    public ObservableCollection<DebugInfoReport> DebugInfoReports { get; }
    public ObservableCollection<DebugBreakpoint> DebugBreakpoints { get; }

    private bool _isAnyBreakpointHit;

    public bool IsAnyBreakpointHit
    {
        get => _isAnyBreakpointHit;
        private set => SetField(ref _isAnyBreakpointHit, value);
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}