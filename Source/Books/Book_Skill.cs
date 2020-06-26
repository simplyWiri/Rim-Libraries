﻿using RimWorld;
using Verse;

namespace Libraries.Books
{
    public class Book_Skill : BookArt
    {
        private SkillDef skillDef;
        public SkillDef SkillDef => skillDef;

        public float CurrentMultiplier()
        {
            //switch(this.CompQuality.Quality)
            //{
            //    case QualityCategory.Awful: return .5f;
            //    case QualityCategory.Poor: return .6f;
            //    case QualityCategory.Normal: return .65f;
            //    case QualityCategory.Good: return .75f;
            //    case QualityCategory.Excellent: return 1f;
            //    case QualityCategory.Masterwork: return 2f;
            //    case QualityCategory.Legendary: return 3f;
            //}
            //Log.Error("[RimLibrary] Unrecognised quality type, returning 0");
            return 1;
        }

        public Book_Skill() : base()
        {
            skillDef = SkillDefOf.Artistic;
            Find.CurrentMap?.GetComponent<MapComponent_Library>().books.Add(this);
        }
    }
}