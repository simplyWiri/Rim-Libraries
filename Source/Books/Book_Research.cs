using Verse;

namespace Libraries.Books
{
    // we don't want a comp quality on this particular book
    internal class Book_Research : Book
    {
        //private ResearchProjectDef research;

        public bool IsDef(ResearchProjectDef d) => def.shortHash == d.shortHash;
    }
}