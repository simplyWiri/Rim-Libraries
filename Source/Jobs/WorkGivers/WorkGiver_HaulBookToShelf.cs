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
        public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn) => pawn.Map.GetComponent<MapComponent_Library>().HaulableBooks;

        public override PathEndMode PathEndMode => PathEndMode.ClosestTouch;

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            Book book = t as Book;
            if (!HaulAIUtility.PawnCanAutomaticallyHaul(pawn, t, forced)) return null;

            var building = ClosestStorage(pawn, t);

            if (building == null)
            {
                JobFailReason.Is("No Storage Found");
                return null;
            }

            return JobMaker.MakeJob(LibraryJobDefOf.RL_HaulBookToShelf, book, building);
        }


        // pretty much vanilla's implementating, bar skinning a few checks which are never relevant
        public Building_InternalStorage ClosestStorage(Pawn p, Thing t)
        {
            float closestDst = float.MaxValue;
            var parms = TraverseParms.For(p, Danger.Deadly, TraverseMode.ByPawn, false);
            Building_InternalStorage closest = null;

            foreach (var thing in p.Map.GetComponent<MapComponent_Library>().AllShelves)
            {
                if (!thing.Spawned) continue;
                if (!(p.CanReserve(thing) && thing.Accepts(t))) continue;

                float sqr = (t.Position - thing.Position).LengthHorizontalSquared;
                if (!(sqr < closestDst)) continue;

                if (p.Map.reachability.CanReach(t.Position, thing, PathEndMode.ClosestTouch, parms))
                    closest = thing;
            }

            return closest;
        }
    }
}

