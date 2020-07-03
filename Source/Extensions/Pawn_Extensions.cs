using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Libraries.Extensions
{
    public static class Pawn_Extensions
    {
        public static Dictionary<Pawn, HashSet<ResearchProjectDef>> KnownTechnologies = new Dictionary<Pawn, HashSet<ResearchProjectDef>>();
        public static void GenerateDictionaryEntry(Pawn key)
        {
            var researches = DefDatabase<ResearchProjectDef>.AllDefsListForReading;
            var knownTechs = new HashSet<ResearchProjectDef>();
            foreach(var def in key.Faction.def.startingResearchTags)
            {
                foreach(var researchDef in researches)
                {
                    if(researchDef.HasTag(def))
                    {
                        AddRecursivePrerequisities(researchDef, knownTechs);
                    }
                }
            }
            KnownTechnologies.Add(key, knownTechs);
        }

        public static void AddRecursivePrerequisities(ResearchProjectDef def, HashSet<ResearchProjectDef> knownTechs)
        {
            knownTechs.Add(def);
            foreach(var requisite in def.prerequisites)
            {
                AddRecursivePrerequisities(requisite, knownTechs);
            }
        }

        public static bool CanCraftThing(this Pawn pawn, ThingDef thing)
        {
            if (thing.researchPrerequisites.NullOrEmpty()) return true;

            try
            {
                foreach (var requisite in thing?.researchPrerequisites)
                    if (!KnownTechnologies[pawn].Contains(requisite)) return false;

                return true;
            } catch (Exception)
            {
                GenerateDictionaryEntry(pawn);

                foreach (var requisite in thing?.researchPrerequisites)
                    if (!KnownTechnologies[pawn].Contains(requisite)) return false;

                return true;
            }
        }
        public static bool CanBuildThing(this Pawn pawn, Building building)
        {
            if (building.def.researchPrerequisites.NullOrEmpty()) return true;

            try
            {
                foreach (var requisite in building.def?.researchPrerequisites)
                    if (!KnownTechnologies[pawn].Contains(requisite)) return false;

                return true;
            }
            catch (Exception)
            {
                GenerateDictionaryEntry(pawn);

                foreach (var requisite in building.def?.researchPrerequisites)
                    if (!KnownTechnologies[pawn].Contains(requisite)) return false;

                return true;
            }
        }

    }
}
