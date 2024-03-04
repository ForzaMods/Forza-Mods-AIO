namespace Forza_Mods_AIO.Resources;

public abstract class Pages
{
    private static readonly Dictionary<Type, object> CachedInstances = new();

    public static void RemovePage(Type pageType)
    {
        CachedInstances.Remove(pageType);
    }

    public static object GetPage(Type pageType)
    {
        if (CachedInstances.TryGetValue(pageType, out var cachedInstance))
        {
            return cachedInstance;
        }

        var newInstance = Activator.CreateInstance(pageType);
        CachedInstances[pageType] = newInstance!;
        return newInstance!;
    }
}