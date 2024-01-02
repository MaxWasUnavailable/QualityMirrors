using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace QualityMirrors.Patches;

[HarmonyPatch(typeof(MirrorReflection))]
internal static class MirrorReflectionPatches
{
    [HarmonyPatch(nameof(MirrorReflection.OnWillRenderObject))]
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> IncreaseMirrorResolutions(IEnumerable<CodeInstruction> instructions)
    {
        return new CodeMatcher(instructions)
            .SearchForward(instruction => instruction.opcode == OpCodes.Ldc_I4 && (int)instruction.operand == 128)
            .SetOperandAndAdvance(512)
            .SearchForward(instruction => instruction.opcode == OpCodes.Ldc_I4 && (int)instruction.operand == 256)
            .SetOperandAndAdvance(1024)
            .SearchForward(instruction => instruction.opcode == OpCodes.Ldc_I4 && (int)instruction.operand == 512)
            .SetOperandAndAdvance(2048)
            .SearchForward(instruction => instruction.opcode == OpCodes.Ldc_I4 && (int)instruction.operand == 1024)
            .SetOperandAndAdvance(4096)
            .InstructionEnumeration();
    }
}