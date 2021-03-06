﻿using Libraries.Books;
using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Libraries.Jobs
{
    public class JobDriver_ReadSkillBook : JobDriver
    {
        private Book_Skill BookTarget => job.GetTarget(TargetIndex.A).Thing as Book_Skill;
        private int defaultReadTicks => 1500;
        private int curReadTicks = 0;

        public override IEnumerable<Toil> MakeNewToils()
        {
            foreach (var t in Toils_Book.ReadBook(pawn, BookTarget))
            {
                yield return t;
            }

            Toil toil = new Toil();
            var learnRate = 0.07f * BookTarget.CurrentMultiplier * Libraries.Settings.SkillBookSkillGainMultiplier;
            toil.tickAction = () =>
            {
                pawn.skills.Learn(BookTarget.SkillDef, learnRate);

                curReadTicks++;
                if (curReadTicks > defaultReadTicks)
                    ReadyForNextToil();
            };

            toil.defaultCompleteMode = ToilCompleteMode.Never;
            toil.activeSkill = () => SkillDefOf.Intellectual;
            toil.WithProgressBar(TargetIndex.A, () => curReadTicks / (float)defaultReadTicks);
            toil.WithEffect(Library_SubEffecterDefOf.RL_BookEffecter, TargetIndex.A);

            yield return toil;

            yield return Toils_Book.DropBook(pawn);
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(BookTarget, job, errorOnFailed: errorOnFailed);
        }
    }
}