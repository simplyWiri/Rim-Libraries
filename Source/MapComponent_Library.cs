using Libraries.Books;
using Libraries.Buildings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        private List<Building_Bookcase> shelves = new List<Building_Bookcase>();

        public IEnumerable<Building_Bookcase> AllShelves => shelves;
        public IEnumerable<Building_Bookcase> DustyShelves => shelves.Where(shelf => shelf.Dusty());
        

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

        public override void MapComponentTick()
        {
            base.MapComponentTick();
            var ticks = GenTicks.TicksAbs;

            if (ticks % 15 != 0) return;

            if (ticks % 250 == 0)
            {
                foreach (var shelf in shelves)
                {
                    shelf.AddDust(Rand.Range(0.001f, 0.01f) * 10);
                }
            }
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

        public void RegisterBookshelf(Building_Bookcase shelf) => shelves.Add(shelf);
        public void DeRegisterBookshelf(Building_Bookcase shelf) => shelves.Remove(shelf);
    }
}