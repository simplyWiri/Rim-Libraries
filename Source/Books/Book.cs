using RimWorld;
using Verse;

namespace Libraries
{
    // The different types of books Skill, Research, Tech, Artefact
    // none, Journal, Tome, Scroll, Codex
    public class Book : ThingWithComps
    {
        // this will be overriden as a getter in child classes
        //private Building parent = null;

        private CompQuality compQuality = null;
        public CompQuality CompQuality => compQuality ??= this.TryGetComp<CompQuality>();

        private CompArt compArt = null;
        public CompArt CompArt => compArt ??= this.TryGetComp<CompArt>();
    }
}