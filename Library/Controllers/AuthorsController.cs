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
    public class AuthorsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        [HttpGet("/authors")]
        public ActionResult Index()
        {
            return View(db.Authors.ToList());
        }

        [HttpGet("/authors/new")]
        public ActionResult Create()
        {
            ViewBag.BookIds = db.Books.ToList()
                .Select(book => new SelectListItem
                {
                    Value = book.BookId.ToString(),
                    Text = book.BookTitle
                })
                .ToList();
            return View();
        }

        [HttpPost("/authors/new")]
        public ActionResult Create(Author author, List<int> BookIds)
        {
            db.Authors.Add(author);
            foreach (int bookId in BookIds)
            {
                BookAuthor newBookAuthor = new BookAuthor(bookId, author.AuthorId);
                db.BookAuthors.Add(newBookAuthor);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/authors/{id}")]
        public ActionResult Details(int id)
        {
            Author author = db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
            var entryList = db.BookAuthors.Where(entry => entry.AuthorId == id).ToList();
            List<Book> bookList = new List<Book> ();
            foreach (var book in entryList)
            {
                int bookId = book.BookId;
                bookList.Add(db.Books.FirstOrDefault(record => record.BookId == bookId));
            }
            ViewBag.BookList = bookList;
            return View(author);
        }

        [HttpGet("authors/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Author author = db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
            ViewBag.BookIds = db.Books.ToList()
                .Select(book => new SelectListItem
                {
                    Value = book.BookId.ToString(),
                    Text = book.BookTitle
                })
                .ToList();
            return View(author);
        }

        [HttpPost("authors/{id}/edit")]
        public ActionResult Edit(Author author, List<int> BookIds)
        {
            db.Entry(author).State = EntityState.Modified;
            var authorMatchesInJoinTable = db.BookAuthors.Where(entry => entry.AuthorId == author.AuthorId).ToList();
            // Remove all BookAuthor entries for specified author.
            foreach (var book in authorMatchesInJoinTable)
            {
                int bookId = book.BookId;
                var joinEntry = db.BookAuthors
                                  .Where(entry => entry.BookId == bookId)
                                  .Where(entry => entry.AuthorId == author.AuthorId);
                foreach (var entry in joinEntry)
                {
                    db.BookAuthors.Remove(entry);
                }
            }
            // Add BookAuthor entries based on authorMatches.
            foreach (var id in BookIds)
            {
                Book book = db.Books.FirstOrDefault(_ => _.BookId == id);
                BookAuthor newBookAuthor = new BookAuthor(book.BookId, author.AuthorId);
                db.BookAuthors.Add(newBookAuthor);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("authors/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Author author = db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
            BookAuthor joinEntry = db.BookAuthors.FirstOrDefault(entry => entry.AuthorId == id);
            db.Authors.Remove(author);
            if (joinEntry != null) db.BookAuthors.Remove(joinEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
