using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using BaseNutcrackerMusicFix.Patches;

namespace BaseNutcrackerMusicFix
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class BaseNutcrackerMusicFix : BaseUnityPlugin
    {
        private const string modGUID = "HondaOdyssey.BaseNutcrackerMusicFix";
        private const string modName = "Base Nutcracker Music Fix";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static BaseNutcrackerMusicFix Instance;

        internal ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Patching NutcrackerAI...");

            harmony.PatchAll(typeof(BaseNutcrackerMusicFix));
            harmony.PatchAll(typeof(NutcrackerAIPatch));

            mls.LogInfo("Patched Nutcracker AI");
        }
    }
}
