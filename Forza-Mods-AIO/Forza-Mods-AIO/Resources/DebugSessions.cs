using System.Collections.ObjectModel;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using Forza_Mods_AIO.Models;

namespace Forza_Mods_AIO.Resources;

public class DebugSessions
{
    private static DebugSessions _instance = null!;
    public static DebugSessions GetInstance()
    {
        if (_instance != null!) return _instance;
        _instance = new DebugSessions();
        return _instance;
    }

    public readonly ObservableCollection<DebugSession> EveryDebugSession = [Cheats.GetClass<Bypass>().BypassDebug];
}