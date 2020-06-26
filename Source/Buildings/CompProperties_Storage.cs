using Verse;

namespace Libraries.Buildings
{
    public class CompProperties_Storage : CompProperties
    {
        public GraphicData empty = null;
        public GraphicData spare = null;
        public GraphicData full = null;

        public int sparsethreshold = 5;
        public int fullthreshold = 30;

        public CompProperties_Storage()
        {
            compClass = typeof(CompStorage);
        }
    }
}