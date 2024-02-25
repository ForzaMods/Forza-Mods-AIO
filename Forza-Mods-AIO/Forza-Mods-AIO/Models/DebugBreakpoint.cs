namespace Forza_Mods_AIO.Models;

public class DebugBreakpoint(string name)
{
    public string Name { get; } = name;
    public bool IsEnabled { get; }
    public bool IsHit { get; }
}