using System.Diagnostics;

namespace Forza_Mods_AIO.Models;

public class GameVerPlat(string name, string plat, string update, Process process, GameVerPlat.GameType type)
{
    public string Name { get; } = name;
    public string Plat { get; } = plat;
    public string Update { get; } = update;
    public Process Process { get; } = process;
    public GameType Type { get; } = type;

    public GameVerPlat() : this(string.Empty, string.Empty, string.Empty, new Process(), GameType.None)
    {
    }
        
    public enum GameType : ushort
    {
        None = 0,
        Fh4,
        Fh5,
        Fm8
    }
}