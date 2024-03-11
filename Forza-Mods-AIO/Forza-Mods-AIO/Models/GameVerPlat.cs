namespace Forza_Mods_AIO.Models;

public class GameVerPlat(string name, string platform, string update,GameVerPlat.GameType type)
{
    private static GameVerPlat _instance = null!;
    public static GameVerPlat GetInstance()
    {
        if (_instance != null!) return _instance;
        _instance = new GameVerPlat();
        return _instance;
    }
    
    public string Name { get; set; } = name;
    public string Platform { get; set; } = platform;
    public string Update { get; set; } = update;
    public GameType Type { get; set; } = type;

    private GameVerPlat() : this(string.Empty, string.Empty, string.Empty, GameType.None)
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