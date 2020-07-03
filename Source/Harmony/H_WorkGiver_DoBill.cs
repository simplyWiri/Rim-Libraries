using HarmonyLib;
using Libraries.Extensions;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace Libraries.Harmony
{
    [HarmonyPatch(typeof(WorkGiver_DoBill), nameof(WorkGiver_DoBill.StartOrResumeBillJob))]
    public static class H_WorkGiver_DoBill
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGen)
        {
            var meth = AccessTools.Method(typeof(H_WorkGiver_DoBill), nameof(H_WorkGiver_DoBill.ValidateBill));
            var instList = instructions.ToList();

            int index = instList.FirstIndexOf(inst => inst.opcode == OpCodes.Isinst);
            index -= 2;

            var label = instList[index].operand;

            var insert = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldarg_2),
                new CodeInstruction(OpCodes.Callvirt, meth),
                new CodeInstruction(OpCodes.Brfalse, label)
            };

            instList.InsertRange(index, insert);

            return instList;
        }

        public static bool ValidateBill(Pawn pawn, Bill bill, IBillGiver giver)
        {
#if DEBUG
            Log.Message($"[Libraries] StartOrResume: pawn {{{ pawn.Name }}} attempting to do the bill {{{ bill.Label }}} from the giver {{{giver.LabelShort}}}");
#endif
            if (!pawn.IsColonist) return true;

            //if (pawn.CanCraftThing(bill.recipe.ProducedThingDef))
            //    return true;

            JobFailReason.Is("Does not have the required knowledge");
            return false;
        }
    }

    [HarmonyPatch(typeof(WorkGiver_DoBill), nameof(WorkGiver_DoBill.TryStartNewDoBillJob))]
    public static class H_WorkGiver_DoBill_TryStartNew
    {
        public static bool Prefix(Pawn pawn, Bill bill, IBillGiver giver)
        {
#if DEBUG
            Log.Message($"[Libraries] TryStartNew: pawn {{{ pawn.Name }}} attempting to do the bill {{{ bill.Label }}} from the giver {{{giver.LabelShort}}}");
#endif
            if (!pawn.IsColonist) return true;

            //if (pawn.CanCraftThing(bill.recipe.ProducedThingDef))
            //    return true;

            JobFailReason.Is("Does not have the required knowledge");
            return false;
        }
    }
}