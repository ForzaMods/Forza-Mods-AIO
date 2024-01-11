using System.Diagnostics;

namespace Forza_Mods_AIO.Resources;

public class GameVerPlat
{
    public string Name { get; }
    public string Plat { get; }
    public string Update { get; }
    public Process Process { get; }
    public GameType Type { get; }
        
    public GameVerPlat(string name, string plat, string update, Process process, GameType type)
    {
        Name = name;
        Plat = plat;
        Process = process;
        Update = update;
        Type = type;
    }
    
    public GameVerPlat()
    {
        Name = string.Empty;
        Plat = string.Empty;
        Process = new Process();
        Update = string.Empty;
        Type = GameType.None;
    }
        
    public enum GameType
    {
        None = 0,
        Fh4,
        Fh5,
        Fm8
    }
}