using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Libraries.Rooms
{
    public class RoomStatWorker_Study : RoomStatWorker
    {
        public override float GetScore(Room room)
        {

            // when the idea of 'noise' is implemented, we will add this here 

            return room.GetStat(RoomsDefOf.RL_Grandeur);
        }
    }
}
