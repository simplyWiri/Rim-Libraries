using RimWorld;
using Verse;

namespace Libraries
{
    // The different types of books Skill, Research, Tech, Artefact
    // none, Journal, Tome, Scroll, Codex
    public class Book : ThingWithComps
    {
        // this will be overriden as a getter in child classes
        private Building parent = null;   


        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            map.GetComponent<MapComponent_Library>().books.Add(this);
        }

        private CompQuality compQuality = null;
        public CompQuality CompQuality => compQuality ??= this.TryGetComp<CompQuality>();

        private CompArt compArt = null;
        public CompArt CompArt => compArt ??= this.TryGetComp<CompArt>();

        public override string Label
        {
            get
            {
                return base.Label;
            }
        }
    }
}