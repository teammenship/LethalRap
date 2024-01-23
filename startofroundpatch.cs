
using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKRapIntroMod.patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class startofroundpatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static void OverrideAudio(StartOfRound __instance)
        {
            __instance.shipIntroSpeechSFX = DKRapCore.SoundFX[0];
        }
    }
}
