using HarmonyLib;
using System.Reflection;

[HarmonyPatch(typeof(Assembly), nameof(Assembly.GetEntryAssembly))]
class PatchGetEntryAssembly
{
    static bool Prefix(ref Assembly __result)
    {
        try
        {
            __result = GlobalVariables.LoadedAssembly;
            return false;
        }
        catch
        {

        }

        return true;
    }
}

[HarmonyPatch(typeof(Assembly), nameof(Assembly.GetCallingAssembly))]
class PatchGetCallingAssembly
{
    static bool Prefix(ref Assembly __result)
    {
        try
        {
            __result = GlobalVariables.LoadedAssembly;
            return false;
        }
        catch
        {

        }

        return true;
    }
}

[HarmonyPatch(typeof(Assembly), nameof(Assembly.GetExecutingAssembly))]
class PatchGetExecutingAssembly
{
    static bool Prefix(ref Assembly __result)
    {
        try
        {
            __result = GlobalVariables.LoadedAssembly;
            return false;
        }
        catch
        {

        }

        return true;
    }
}