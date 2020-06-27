using Libraries.Books;
using Libraries.Buildings;
using System.Collections.Generic;
using Verse;

namespace Libraries
{
    public class MapComponent_Library : MapComponent
    {
        // if in hardcore mode, we need a quick way to check if we have a particular type of book
        private HashSet<Book_Research> researchBooks = new HashSet<Book_Research>();

        // for access to bookshelves
        private Dictionary<IntVec3, Building_Bookcase> bookcases = new Dictionary<IntVec3, Building_Bookcase>();

        public HashSet<Book> books = new HashSet<Book>();

        public MapComponent_Library(Map map) : base(map)
        {
        }

        public Book RandomBookForRecreation()
        {
            return books.RandomElement();
        }

        public override void ExposeData()
        {
            base.ExposeData();
        }
    }
}