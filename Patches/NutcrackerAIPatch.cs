using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using UnityEngine;

namespace BaseNutcrackerMusicFix.Patches
{
    internal static class NutcrackerAIPatch
    {
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(NutcrackerEnemyAI), "AimGun", MethodType.Enumerator)]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeInstruction instructionInsert = Transpilers.EmitDelegate<Action<AudioSource>>(audio => { if (!audio.isPlaying) { audio.loop = true; audio.Play(); } });
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            int indexToWork = -1;

            for (int i = 0; i < code.Count - 1; i++)
            {
                if (code[i].opcode == OpCodes.Ldfld && ((FieldInfo)code[i].operand).Name == "creatureVoice")
                {
                    indexToWork = i;
                    break;
                }
            }
            if (indexToWork == -1)
            {
                throw new Exception("Unable to patch NutcrackerAI");
            }

            code.RemoveRange(indexToWork + 1, 7);
            code.Insert(indexToWork + 1, instructionInsert);

            return code;
        }

    }

}
