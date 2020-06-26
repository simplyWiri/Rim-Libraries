using Libraries.Books;
using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Libraries.Jobs
{
    public class WorkGiver_ReadSkillBook : WorkGiver_Scanner
    {
        public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
        {
            return pawn.Map.listerThings.AllThings.FindAll(x => x is Book_Skill);
        }

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            return JobMaker.MakeJob(LibraryJobDefOf.RL_ReadSkillBook, t);
        }
    }
}