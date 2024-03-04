namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class CustomizationCheats : CheatsUtilities, ICheatsBase
{
    public void Cleanup()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        var fields = typeof(CustomizationCheats).GetFields().Where(f => f.FieldType == typeof(nuint));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }
}