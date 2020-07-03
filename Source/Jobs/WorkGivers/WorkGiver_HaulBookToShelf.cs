using Libraries.Buildings;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace Libraries.Jobs
{
    public class WorkGiver_HaulBookToShelf : WorkGiver_Scanner
    {
        public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
        {
            return pawn.Map.listerThings.AllThings.FindAll(x => x is Book);
        }

        public override PathEndMode PathEndMode => PathEndMode.ClosestTouch;

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            Book book = t as Book;
            if (!HaulAIUtility.PawnCanAutomaticallyHaul(pawn, t, forced)) return null;

            var building = GenClosest.ClosestThing_Global_Reachable(
                book.Position,
                pawn.Map,
                pawn.Map.listerThings.AllThings.FindAll(x => x is Building_InternalStorage),
                PathEndMode.ClosestTouch,
                TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false),
                9999f,
                (Thing m) => !m.IsForbidden(pawn) && pawn.CanReserveNew(m) && ((Building_InternalStorage)m).Accepts(book));

            if (building == null)
            {
                JobFailReason.Is("No Storage Found");
                return null;
            }

            return JobMaker.MakeJob(LibraryJobDefOf.RL_HaulBookToShelf, book, building);
        }
    }
}

