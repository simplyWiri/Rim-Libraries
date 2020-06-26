using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Libraries
{
    public static class Room_Extensions
    {
        public static Dictionary<Room, Tuple<List<Thing>, List<Thing>>> CachedValues = new Dictionary<Room, Tuple<List<Thing>, List<Thing>>>();

        public static List<Thing> UnReservedSittableBuildings(this Room room)
        {
            if (!CachedValues.ContainsKey(room) || CachedValues[room].Item1.Count != room.ContainedAndAdjacentThings.Count)
            {
                var listThings = room.ContainedAndAdjacentThings;
                List<Thing> listseats = listThings.Where(t => t.def?.building?.isSittable ?? false).OrderByDescending(t => t.def.GetStatValueAbstract(StatDefOf.Comfort)).ToList();

                CachedValues.SetOrAdd(room, new Tuple<List<Thing>, List<Thing>>(listThings, listseats));
            }

            return CachedValues[room].Item2;
        }
    }
}