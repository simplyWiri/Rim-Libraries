using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Libraries
{
    public static class Room_Extensions
    {
        public static Dictionary<int, IEnumerable<Thing>> CachedValues = new Dictionary<int, IEnumerable<Thing>>();
        public static Dictionary<int, float> cachedTimes = new Dictionary<int, float>();
        public static IEnumerable<Thing> SittableBuildings(this Room room)
        {
            if (room == null) return null;

            var t = Time.time;

            if(CachedValues.TryGetValue(room.ID, out var list))
            {
                if(t - cachedTimes[room.ID] < 10)
                {
                    return list;
                } else
                {
                    getSeats(out var seats);

                    CachedValues[room.ID] = seats;
                    cachedTimes[room.ID] = t;
                    return seats;
                }
            } else
            {
                getSeats(out var seats);

                CachedValues.Add(room.ID, seats);
                cachedTimes.Add(room.ID, Time.time);

                return seats;
            }

            void getSeats(out IEnumerable<Thing> seats)
            {
                var listThings = room.ContainedAndAdjacentThings;
                seats = listThings.Where(t => t.def?.building?.isSittable ?? false).OrderByDescending(t => t.def.GetStatValueAbstract(StatDefOf.Comfort));
            }
        }
    }
}