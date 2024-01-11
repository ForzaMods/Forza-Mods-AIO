using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class FeatureBase
{
    protected static bool IsProcessValid()
    {
        if (!MainWindow.Mw.Attached)
        {
            return false;
        }
            
        if (MainWindow.Mw.M.MProc == null)
        {
            return false;
        }

        return MainWindow.Mw.Gvp.Type != GameVerPlat.GameType.None;
    }
}