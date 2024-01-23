using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKRapIntroMod.patches;
using UnityEngine;

namespace DKRapIntroMod
{
    [BepInPlugin(modGUID, modName, modVersion)]

    public class DKRapCore : BaseUnityPlugin

    {
        private const string modGUID = "ttv.teammenship.dkrapintromod";
        private const string modName = "DK Rap Intro Mod";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static DKRapCore Instance;

        internal ManualLogSource mls;

        internal static List<AudioClip> SoundFX;
        internal static AssetBundle Bundle;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Here, here, here we go!");

            harmony.PatchAll(typeof(DKRapCore));
            harmony.PatchAll(typeof(startofroundpatch));

            mls = Logger;

            SoundFX = new List<AudioClip>();
            string FolderLocation = Instance.Info.Location;
            FolderLocation = FolderLocation.TrimEnd("DKRapIntroMod.dll".ToCharArray());
            Bundle = AssetBundle.LoadFromFile(FolderLocation + "funkykong");
            if (Bundle != null)
            {
                mls.LogInfo("OKAY!");
                SoundFX = Bundle.LoadAllAssets<AudioClip>().ToList();
            }
            else
            {
                mls.LogError("Donkey Kong has been captured by K. Rool! Something has gone terribly wrong!");
            }
        }
    }
}

