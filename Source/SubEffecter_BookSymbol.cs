﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace Libraries
{
	[DefOf]
	public static class Library_SubEffecterDefOf
    {
		public static EffecterDef RL_BookEffecter;
    }

    class SubEffecter_BookSymbol : SubEffecter
    {
		private Mote interactMote;

		public SubEffecter_BookSymbol(SubEffecterDef def, Effecter parent) : base(def, parent) { }

		public override void SubEffectTick(TargetInfo A, TargetInfo B)
		{
			if (interactMote == null)
			{
				interactMote = (Mote)ThingMaker.MakeThing(def.moteDef);
				interactMote.exactPosition = A.Cell.ToVector3();
				interactMote.Scale = .65f;
				GenSpawn.Spawn(interactMote, A.Cell, A.Map);
			}
		}

		public override void SubCleanup()
		{
			if (interactMote != null && !interactMote.Destroyed)
			{
				interactMote.Destroy();
			}
		}
	}
}
