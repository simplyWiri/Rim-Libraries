using Verse;

namespace Libraries.Buildings
{
    public class CompStorage : ThingComp
    {
        public CompProperties_Storage Props => props as CompProperties_Storage;
        public Graphic CurrentGraphic => parent.DefaultGraphic;
    }
}