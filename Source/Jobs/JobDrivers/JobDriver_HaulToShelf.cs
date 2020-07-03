using Libraries.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace Libraries.Jobs
{
    public class JobDriver_HaulToShelf : JobDriver
    {
        private Book BookTarget => job.GetTarget(TargetIndex.A).Thing as Book;
        private Building_InternalStorage ShelfTarget => job.GetTarget(TargetIndex.B).Thing as Building_InternalStorage;

        public override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedNullOrForbidden(TargetIndex.A);
            this.FailOnDestroyedNullOrForbidden(TargetIndex.B);
            this.FailOn(() => !ShelfTarget.Accepts(BookTarget));


            // goto book
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch).FailOnSomeonePhysicallyInteracting(TargetIndex.A);
            // pickup book
            pawn.CurJob.count = 1;
            yield return Toils_Haul.StartCarryThing(TargetIndex.A, false, false, false);
            // carry to shelf
            yield return Toils_Haul.CarryHauledThingToContainer();
            // wait for a bit for dramatic effect
            yield return Toils_General.Wait(150).WithProgressBarToilDelay(TargetIndex.B, false, -0.5f);
            // move our book(s) from our pawns inventory to our Shelf
            yield return new Toil
            {
                initAction = delegate
                {
                    if (pawn.carryTracker.CarriedThing == null) return;
                   
                    if (ShelfTarget.Accepts(BookTarget))
                    {
                        if (BookTarget.holdingOwner != null)
                        {
                            BookTarget.holdingOwner.TryTransferToContainer(BookTarget, ShelfTarget.TryGetInnerInteractableThingOwner(), BookTarget.stackCount, true);
                        }
                        else
                        {
                            ShelfTarget.TryGetInnerInteractableThingOwner().TryAdd(BookTarget, true);
                        }
                        // should actually update our shelf graphic here
                        pawn.carryTracker.innerContainer.Remove(BookTarget);
                    }
                }
            };
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(BookTarget, job, errorOnFailed: errorOnFailed) && pawn.Reserve(ShelfTarget, job, errorOnFailed: errorOnFailed);
        }
    }
}
