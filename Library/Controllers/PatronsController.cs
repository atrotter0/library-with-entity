using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Controllers
{
    public class PatronsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        [HttpGet("/patrons")]
        public ActionResult Index()
        {
            return View(db.Patrons.ToList());
        }

        [HttpGet("/patrons/new")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("/patrons/new")]
        public ActionResult Create(Patron patron)
        {
            db.Patrons.Add(patron);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet("/patrons/{id}")]
        public ActionResult Details(int id)
        {
            Patron patron = db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            var entryList = db.PatronsBooks.Where(entry => entry.PatronId == id).ToList();
            List<Book> bookList = new List<Book>();
            foreach (var book in entryList)
            {
                int bookId = book.BookId;
                bookList.Add(db.Books.FirstOrDefault(record => record.BookId == bookId));
            }
            ViewBag.BookList = bookList;
            return View(patron);
        }


        [HttpGet("patrons/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Patron patron = db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            ViewBag.BookIds = db.Books.ToList()
                .Select(book => new SelectListItem
                {
                    Value = book.BookId.ToString(),
                    Text = book.BookId.ToString()
                })
                .ToList();
            return View(patron);
        }

        [HttpPost("patrons/{id}/edit")]
        public ActionResult Edit(Patron patron)
        {
            db.Entry(patron).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/patrons/{id}/checkout")]
        public ActionResult Checkout(int id)
        {
            Patron patron = db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            ViewBag.BookIds = db.Books.ToList()
                .Select(book => new SelectListItem
                {
                    Value = book.BookId.ToString(),
                    Text = book.BookTitle
                })
                .ToList();
            return View(patron);
        }

        [HttpPost("/patrons/{id}/checkout")]
        public ActionResult Checkout(int id, List<int> BookIds)
        {
            // Add PatronBook entries based on BookId.
            foreach (var bookId in BookIds)
            {
                Book book = db.Books.FirstOrDefault(bookItem => bookItem.BookId == bookId);
                PatronBook newPatronBook = new PatronBook(id, book.BookId);
                db.PatronsBooks.Add(newPatronBook);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("patrons/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Patron patron = db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            PatronBook joinEntry = db.PatronsBooks.FirstOrDefault(entry => entry.PatronId == id);
            db.Patrons.Remove(patron);
            if (joinEntry != null) db.PatronsBooks.Remove(joinEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
