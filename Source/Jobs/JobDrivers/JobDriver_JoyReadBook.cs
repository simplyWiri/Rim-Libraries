using Libraries.Books;
using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Libraries.Jobs
{
    public class JobDriver_JoyReadBook : JobDriver
    {
        private Book BookTarget => job.GetTarget(TargetIndex.A).Thing as Book;
        private float defaultReadTicks => 1500;
        private float curReadTicks = 0;

        public override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedOrNull(TargetIndex.A);

            foreach (var t in Toils_Book.ReadBook(pawn, BookTarget))
            {
                yield return t;
            }

            var toil = new Toil();
            toil.AddPreInitAction(() =>
            {
                pawn.GainComfortFromCellIfPossible();
                JoyUtility.JoyTickCheckEnd(pawn, JoyTickFullJoyAction.EndJob);
            });

            toil.tickAction = () =>
            {
                Pawn actor = pawn;

                if (BookTarget is Book_Skill)
                {
                    var skillbook = BookTarget as Book_Skill;
                    actor.skills.Learn(skillbook.SkillDef, 0.07f * skillbook.CurrentMultiplier() * Libraries.Settings.SkillBookSkillGainMultiplier);
                }

                curReadTicks++;

                if (curReadTicks > job.def.joyDuration)
                    ReadyForNextToil();
                else
                    JoyUtility.JoyTickCheckEnd(pawn);
            };

            toil.AddFinishAction(() =>
            {
                JoyUtility.TryGainRecRoomThought(pawn);
            });

            toil.defaultCompleteMode = ToilCompleteMode.Never;

            yield return toil;

            yield return Toils_Book.DropBook(pawn);
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(BookTarget, job, errorOnFailed: errorOnFailed);
        }
    }
}