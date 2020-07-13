using Libraries.Books;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace Libraries.Jobs
{
    public class WorkGiver_ReadSkillBook : WorkGiver_Scanner
    {
        public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
        {
            return pawn.Map.GetComponent<MapComponent_Library>().books.Where(book => book is Book_Skill && pawn.CanReserve(book));
        }

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            return JobMaker.MakeJob(LibraryJobDefOf.RL_ReadSkillBook, t);
        }
    }
}