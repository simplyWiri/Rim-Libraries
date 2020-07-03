using RimWorld;
using Verse;
using Verse.AI;

namespace Libraries.Jobs
{
    public class JoyGiver_ReadBook : JoyGiver
    {
        public override Job TryGiveJob(Pawn pawn)
        {
            var book = pawn.Map.GetComponent<MapComponent_Library>().RandomBookForRecreation(pawn);
            if (book == null) return null;

            Job tempJob = JobMaker.MakeJob(def.jobDef, book);
            tempJob.count = 1;
            return tempJob;
        }
    }
}