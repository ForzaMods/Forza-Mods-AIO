using System.Diagnostics;

namespace Forza_Mods_AIO.Resources;

public class GameVerPlat
{
#if RELEASE
    public string Name { get; }
#endif
    public string Plat { get; }
#if RELEASE
    public string Update { get; }
#endif
    public Process Process { get; }
    public GameType Type { get; }
        
    public GameVerPlat(
#if RELEASE
        string name, 
#endif
        string plat, 
#if RELEASE
        string update, 
#endif
        Process process, 
        GameType type)
    {
#if RELEASE
        Name = name;
#endif
        Plat = plat;
        Process = process;
#if RELEASE
        Update = update;
#endif
        Type = type;
    }
    
    public GameVerPlat()
    {
#if RELEASE
        Name = string.Empty;
#endif        
        Plat = string.Empty;
        Process = new Process();
#if RELEASE
        Update = string.Empty;
#endif
        Type = GameType.None;
    }
        
    public enum GameType : ushort
    {
        None = 0,
        Fh4,
        Fh5,
        Fm8
    }
}
