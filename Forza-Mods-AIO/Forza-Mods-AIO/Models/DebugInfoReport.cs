using System.Globalization;

namespace Forza_Mods_AIO.Models;

public class DebugInfoReport(string info)
{
    public string Info { get; } = info;
    public string ReportedAt { get; } = DateTime.Now.ToString(CultureInfo.InvariantCulture);
}