using HarmonyLib;
using TMPro;
using UnityEngine;

namespace TFS_PhotonNetworkChecker.patches
{
    [HarmonyPatch(typeof(MenuPageMain))]
    internal class MenuPageMainPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void InjectPhotonStatus(MenuPageMain __instance)
        {
            var header = __instance.transform.Find("Header");
            if (header == null)
            {
                Plugin.PluginLogger.LogWarning("[PhotonNetworkChecker] 'Header' not found in MenuPageMain");
                return;
            }

            var buildNameTransform = header.Find("Build Name");
            if (buildNameTransform == null)
            {
                Plugin.PluginLogger.LogWarning("[PhotonNetworkChecker] 'Build Name' not found in Header");
                return;
            }

            // Prevent duplicate injection on re-open
            if (header.Find("Photon Status") != null)
                return;

            var clone = Object.Instantiate(buildNameTransform.gameObject, header);
            clone.name = "Photon Status";

            // Position below Build Name
            var srcRt = buildNameTransform.GetComponent<RectTransform>();
            var rt = clone.GetComponent<RectTransform>();
            if (rt != null && srcRt != null)
                rt.anchoredPosition = srcRt.anchoredPosition + new Vector2(0f, -20f);

            // Remove the vanilla BuildName component so it doesn't overwrite our text
            var bn = clone.GetComponent<BuildName>();
            if (bn != null)
                Object.DestroyImmediate(bn);

            clone.AddComponent<PhotonStatusHUD>();
        }
    }
}
