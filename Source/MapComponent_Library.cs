using Libraries.Books;
using Libraries.Buildings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.WebCam;
using Verse;
using Verse.AI;

namespace Libraries
{
    public class MapComponent_Library : MapComponent
    {
        // if in hardcore mode, we need a quick way to check if we have a particular type of book
        private HashSet<Book_Research> researchBooks = new HashSet<Book_Research>();

        // for access to bookshelves
        public Dictionary<IntVec3, Building_Bookcase> bookcases = new Dictionary<IntVec3, Building_Bookcase>();

        public HashSet<Book> books = new HashSet<Book>();
        public bool BookHaulablesDirty = false;

        private IEnumerable<Book> haulBooks = null;
        public IEnumerable<Book> HaulableBooks
        {
            get
            {
                if(BookHaulablesDirty || haulBooks == null)
                {
                    haulBooks = books.Where(book => !(book.ParentHolder is Building_InternalStorage));
                }

                return haulBooks;
            }
        }



        public MapComponent_Library(Map map) : base(map)
        {
        }

        public Book RandomBookForRecreation(Pawn p)
        {
            foreach(var book in books)
                if (p.CanReserve(book)) return book;
            
            return null;
        }

        public override void ExposeData()
        {
            base.ExposeData();
        }

        public void RegisterBook(Book book)
        {
            books.Add(book);
        }
        public void DeRegisterBook(Book book)
        {
            books.Remove(book);
        }

        public void RegisterBookshelf(Building_Bookcase shelf) => bookcases.Add(shelf.Position, shelf);
        public void DeRegisterBookshelf(Building_Bookcase shelf) => bookcases.Remove(shelf.Position);
    }
}