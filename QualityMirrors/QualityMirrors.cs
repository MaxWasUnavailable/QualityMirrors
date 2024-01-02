using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace QualityMirrors;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class QualityMirrors : BaseUnityPlugin
{
    private new static readonly ManualLogSource Logger = new(PluginInfo.PLUGIN_NAME);
    internal static QualityMirrors? Instance { get; private set; }
    private static Harmony? Harmony { get; set; }
    private static bool IsPatched { get; set; }

    private void Awake()
    {
        Instance = this;

        Harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        PatchAll();

        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
    }

    public void PatchAll()
    {
        if (IsPatched) return;
        Harmony!.PatchAll();
        IsPatched = true;
    }

    public void UnpatchAll()
    {
        if (!IsPatched) return;
        Harmony.UnpatchAll();
        IsPatched = false;
    }
}