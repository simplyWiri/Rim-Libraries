using Libraries.Buildings;
using Verse;

namespace Libraries
{
    public class RoomRoleWorker_Library : RoomRoleWorker
    {
        public override float GetScore(Room room)
        {
            float score = 0;
            var containedThings = room.ContainedAndAdjacentThings;

            foreach (var thing in containedThings)
            {
                if (thing is Building_Altar || thing is Building_Bookcase || thing is Building_Pedestal || thing is Building_StudyTable || thing is Building_TypeWriter)
                    score++;
            }

            return 13.5f * score;
        }
    }
}