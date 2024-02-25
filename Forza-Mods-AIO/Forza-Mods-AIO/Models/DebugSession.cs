using System.Collections.ObjectModel;

namespace Forza_Mods_AIO.Models;

public class DebugSession(
    string name,
    ObservableCollection<DebugInfoReport> debugInfoReports,
    ObservableCollection<DebugBreakpoint> debugBreakpoints)
{
    public string Name { get; } = name;
    public ObservableCollection<DebugInfoReport> DebugInfoReports { get; } = debugInfoReports;
    public ObservableCollection<DebugBreakpoint> DebugBreakpoints { get; }= debugBreakpoints;
}