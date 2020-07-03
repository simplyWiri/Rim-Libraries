using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Libraries.Jobs
{
    public static class Toils_Book
    {
        public const int DEFAULT_READ_TICKS = 1500;

        public static IEnumerable<Toil> ReadBook(Pawn pawn, Book target)
        {
            var toilOpenBook = Toils_General.Wait(3);

            // reserve our book
            yield return Toils_Reserve.Reserve(TargetIndex.A);
            
            // goto our book
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);

            // pick it up, slowly (ish)
            yield return Toils_General.Wait(6).FailOnForbidden(TargetIndex.A).FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch).WithProgressBarToilDelay(TargetIndex.A);

            // but, actually pick it up
            pawn.CurJob.count = 1;
            yield return Toils_Haul.TakeToInventory(TargetIndex.A, () => 1);

            yield return FindAdjacentReadSurface(pawn, target.GetRoom()).FailOnDestroyedNullOrForbidden(TargetIndex.B);

            yield return Toils_Jump.JumpIfTargetDespawnedOrNull(TargetIndex.B, toilOpenBook);

            // open our book
            yield return toilOpenBook;
        }

        public static Toil DropBook(Pawn p)
        {
            Toil toil = new Toil();
            toil.AddPreInitAction(() =>
           {
               p.inventory.innerContainer.TryDrop(p.CurJob.GetTarget(TargetIndex.A).Thing, p.Position, p.Map, ThingPlaceMode.Near, out Thing _);
           });
            toil.AddEndCondition(() =>
           {
               if (!p.inventory.innerContainer.Contains(p.CurJob.GetTarget(TargetIndex.A).Thing.def))
                   return JobCondition.Succeeded;
               else
                   return JobCondition.Ongoing;
           });
            return toil;
        }

        private static Toil FindAdjacentReadSurface(Pawn p, Room room)
        {
            foreach (var thing in room.SittableBuildings())
            {
                if (p.CanReserve(thing))
                {
                    p.CurJob.targetB = thing;
                    p.Reserve(thing, p.CurJob);
                    p.Map.pawnDestinationReservationManager.Reserve(p, p.CurJob, thing.Position);
                    var toil = Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.OnCell);
                    toil.AddFinishAction(() =>
                    {
                        p.Rotation = thing.Rotation;
                    });
                    return toil;
                }
            }
            p.CurJob.targetB = null;
            return new Toil();
        }
    }
}