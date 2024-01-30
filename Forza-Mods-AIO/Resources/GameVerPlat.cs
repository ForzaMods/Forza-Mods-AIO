using System.Diagnostics;

namespace Forza_Mods_AIO.Resources;

public class GameVerPlat
{
    public string Name { get; }
    public string Plat { get; }
#if RELEASE
    public string Update { get; }
#endif
    public Process Process { get; }
    public GameType Type { get; }
        
    public GameVerPlat(
        string name, 
        string plat, 
#if RELEASE
        string update, 
#endif
        Process process, 
        GameType type)
    {
        Name = name;
        Plat = plat;
        Process = process;
#if RELEASE
        Update = update;
#endif
        Type = type;
    }
    
    public GameVerPlat()
    {
        Name = string.Empty;
        Plat = string.Empty;
        Process = new Process();
#if RELEASE
        Update = string.Empty;
#endif
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