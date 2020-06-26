using RimWorld;
using Verse;
using Verse.AI;

namespace Libraries.Jobs
{
    public class JoyGiver_ReadBook : JoyGiver
    {
        public override Job TryGiveJob(Pawn pawn)
        {
            Job tempJob = new Job(def.jobDef, pawn.Map.GetComponent<MapComponent_Library>().RandomBookForRecreation());
            tempJob.count = 1;
            return tempJob;
        }
    }
}