using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

namespace DKRapIntroMod
{
    [BepInPlugin(modGUID, modName, modVersion)]

    public class DKRapCore : BaseUnityPlugin
    {
        private const string modGUID = "ttv.teammenship.dkrapintromod";
        private const string modName = "DK Rap Intro Mod";
        private const string modVersion = "1.0.1";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static DKRapCore Instance;

        internal static List<AudioClip> SoundFX;
        internal static AssetBundle Bundle;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            harmony.PatchAll(typeof(DKRapCore));
            harmony.PatchAll(typeof(startofroundpatch));
            harmony.PatchAll(typeof(StartPatch));

            SoundFX = new List<AudioClip>();
            string FolderLocation = Instance.Info.Location;
            FolderLocation = FolderLocation.TrimEnd("DKRapIntroMod.dll".ToCharArray());
            Bundle = AssetBundle.LoadFromFile(FolderLocation + "dk_rap");
            if (Bundle != null)
            {
                Debug.Log("[DK] OKAY!");
                SoundFX = Bundle.LoadAllAssets<AudioClip>().ToList();
            }
            else
            {
                Debug.Log("[DK] Donkey Kong has been captured by K. Rool! Something has gone terribly wrong!");
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////

    [HarmonyPatch(typeof(StartOfRound))]
    internal class startofroundpatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static void OverrideAudio(StartOfRound __instance)
        {
            Debug.Log("[DK]: welcome aboard skipper!");
            __instance.shipIntroSpeechSFX = DKRapCore.SoundFX[0];
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////

    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartPatch
    {
        [HarmonyPatch("ResetShip")]
        [HarmonyPostfix]
        static void OverrideAudio(StartOfRound __instance)
        {
            int randomNumber = UnityEngine.Random.Range(0, 6);
            Debug.Log("[DK]: we're doing this again...");
            Debug.Log("[DK]: Funky's lucky number is: " + randomNumber);

            if (randomNumber < 5)
            {
                __instance.shipIntroSpeechSFX = DKRapCore.SoundFX[0];
            }
            else
            {
                __instance.shipIntroSpeechSFX = DKRapCore.SoundFX[1];
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////
}