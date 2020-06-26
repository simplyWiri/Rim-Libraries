using HarmonyLib;
using RimWorld;
using Verse;

namespace Libraries.Harmony
{
    [HarmonyPatch(typeof(WorkGiver_ConstructFinishFrames), nameof(WorkGiver_ConstructFinishFrames.JobOnThing))]
    public static class H_WorkGiver_ConstructFinishFrames
    {
        /*
         * This method is called when a 'frame' (an empty building) has had all the resources delivered, and the pawn is now attempting to
         * build the actual building, we aim to deny this, if the pawn has not learnt the skill, and if the skill is available to learn
         * we wish to queue up the reading of this book, and requeue this job after!
         */

        public static void Prefix(Pawn pawn, Thing t)
        {
        }
    }
}