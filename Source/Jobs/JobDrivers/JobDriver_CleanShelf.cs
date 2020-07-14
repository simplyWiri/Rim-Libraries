using Libraries.Buildings;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;

namespace Libraries.Jobs
{
    public class JobDriver_CleanShelf : JobDriver
    {

        private Building_Bookcase Target => job.GetTarget(TargetIndex.A).Thing as Building_Bookcase;
        private int curTicks = 0;
        private int workTaken = 0;
        public override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedNullOrForbidden(TargetIndex.A);

            // goto shelf
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch).FailOnSomeonePhysicallyInteracting(TargetIndex.A);

            Target.CleanDust(out workTaken);
            var clean = new Toil();
            clean.WithProgressBar(TargetIndex.A, () => curTicks / (float)workTaken);

            clean.tickAction = () =>
            {
                if(curTicks++ >= workTaken)
                {
                    ReadyForNextToil();
                }
            };

            clean.AddFinishAction(() => Target.ClearDust());
            clean.WithEffect(EffecterDefOf.Clean, TargetIndex.A);

            yield return clean;
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(TargetA, job, errorOnFailed: errorOnFailed);
        }
    }
}
