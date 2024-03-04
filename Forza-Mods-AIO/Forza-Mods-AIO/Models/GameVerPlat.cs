namespace Forza_Mods_AIO.Models;

public class GameVerPlat(string name, string plat, string update,GameVerPlat.GameType type)
{
    private static GameVerPlat _instance = null!;
    public static GameVerPlat GetInstance()
    {
        if (_instance != null!) return _instance;
        _instance = new GameVerPlat();
        return _instance;
    }
    
    public string Name { get; set; } = name;
    public string Plat { get; set; } = plat;
    public string Update { get; set; } = update;
    public GameType Type { get; set; } = type;

    public GameVerPlat() : this(string.Empty, string.Empty, string.Empty, GameType.None)
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