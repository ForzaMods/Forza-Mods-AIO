namespace Forza_Mods_AIO.Resources;

public static class Cheats
{
    public static readonly Dictionary<Type, object> CachedInstances = [];

    public static T GetClass<T>() where T : class
    {
        var classType = typeof(T);
        if (CachedInstances.TryGetValue(classType, out var cachedInstance))
        {
            return (T)cachedInstance;
        }
        var newInstance = Activator.CreateInstance(classType) as T;
        CachedInstances[classType] = newInstance!;
        return newInstance!;
    }
}