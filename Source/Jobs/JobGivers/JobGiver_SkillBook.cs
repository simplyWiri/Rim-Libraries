using Verse;
using Verse.AI;

namespace Libraries.Jobs
{
    public class JobGiver_ReadSkillBook : ThinkNode_JobGiver
    {
        public override Job TryGiveJob(Pawn pawn)
        {
            Job job = JobMaker.MakeJob(LibraryJobDefOf.RL_ReadSkillBook, pawn);

            return job;
        }
    }
}