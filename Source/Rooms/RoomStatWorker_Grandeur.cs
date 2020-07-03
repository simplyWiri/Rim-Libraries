using Libraries.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Libraries.Rooms
{
    public class RoomStatWorker_Grandeur : RoomStatWorker
    {
        public override float GetScore(Room room)
        {
            /* Our library is going to be judged, quite hard, we care about these things:
             * + The sum total of scores of our books, effected by their quality
             * + Each artifact in the library, and the relevant pedestal it is in
             * + Each studybench in the library
             * + Each Column
             * + Each Brazier
             * 
             * - How dusty are the shelves
             * - How many books are in the room, yet not on shelves
             */

            var list = room.ContainedAndAdjacentThings;
            int roomSize = room.CellCount;
            float score = 0f;
            int numBooks = 0;
            int numBooksOnFloor = 0;
            int bookcases = 0;
            int nearFullBookShelves = 0; // this is only a positive factor!
            int dustyBookcases = 0;

            foreach(var thing in list)
            {
                if(thing is Book)
                {
                    _ = thing.ParentHolder == null ? numBooksOnFloor++ : numBooks++;
                    continue;
                }
                if(thing is Building_Bookcase)
                {
                    var shelf = thing as Building_Bookcase;
                    var inner = shelf.TryGetInnerInteractableThingOwner();
                    if(inner.Count / (float)inner.maxStacks > .66)
                    {
                        nearFullBookShelves++;
                    }
                    bookcases++;

                    if(shelf.Dusty()) // dusty, bookshelves!
                    {
                        dustyBookcases++;
                    }
                }
            }

            return score;
        }

    }
}
