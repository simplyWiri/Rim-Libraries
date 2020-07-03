using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Libraries
{
    public static class Room_Extensions
    {
        public static Dictionary<int, Tuple<List<Thing>, IEnumerable<Thing>>> CachedValues = new Dictionary<int, Tuple<List<Thing>, IEnumerable<Thing>>>();

        public static IEnumerable<Thing> SittableBuildings(this Room room)
        {
            if (room == null) return null;

            if (!CachedValues.ContainsKey(room.ID) || CachedValues[room.ID].Item1.Count != room.ContainedAndAdjacentThings.Count)
            {
                var listThings = room.ContainedAndAdjacentThings;
                var listseats = listThings.Where(t => t.def?.building?.isSittable ?? false).OrderByDescending(t => t.def.GetStatValueAbstract(StatDefOf.Comfort));

                CachedValues.SetOrAdd(room.ID, new Tuple<List<Thing>, IEnumerable<Thing>>(listThings, listseats));
            }

            return CachedValues[room.ID].Item2;
        }
    }
}