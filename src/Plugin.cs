using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace TFS_PhotonNetworkChecker
{
    [BepInPlugin("TFS_PhotonNetworkChecker", "TFS_PhotonNetworkChecker", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource PluginLogger;
        private static Harmony harmony;

        private void Awake()
        {
            PluginLogger = base.Logger;
            PluginLogger.LogInfo("Plugin TFS_PhotonNetworkChecker loaded.");

            harmony = new Harmony("TFS_PhotonNetworkChecker");
            harmony.PatchAll();
        }
    }
}
