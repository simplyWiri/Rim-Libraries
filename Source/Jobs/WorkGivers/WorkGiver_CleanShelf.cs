using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;
using Libraries.Buildings;

namespace Libraries.Jobs
{
    public class WorkGiver_CleanShelf : WorkGiver_Scanner
    {
        public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn) => pawn.Map.GetComponent<MapComponent_Library>().DustyShelves;

        public override PathEndMode PathEndMode => PathEndMode.ClosestTouch;

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (t is Building_Bookcase)
                return JobMaker.MakeJob(LibraryJobDefOf.RL_CleanShelf, t);

            JobFailReason.Is("No dusty shelves found");
            return null;
        }
    }
}
