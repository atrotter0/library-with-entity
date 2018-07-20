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
    public class BooksController : Controller
    {
        private LibraryContext db = new LibraryContext();

        [HttpGet("/books")]
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        [HttpGet("/books/new")]
        public ActionResult Create()
        {
            ViewBag.AuthorIds = db.Authors.ToList()
                .Select(author => new SelectListItem {
                    Value = author.AuthorId.ToString(),
                    Text = author.AuthorFirstName + " " + author.AuthorLastName
                })
                .ToList();
            return View();
        }

        [HttpPost("/books/new")]
        public ActionResult Create(Book book, List<int> AuthorIds)
        {
            db.Books.Add(book);
            Copy copy = new Copy();
            db.Copies.Add(copy);
            BookCopy newBookCopy = new BookCopy(book.BookId, copy.CopyId);
            db.BooksCopies.Add(newBookCopy);
            foreach (int authorId in AuthorIds)
            {
                BookAuthor newBookAuthor = new BookAuthor(book.BookId, authorId);
                db.BookAuthors.Add(newBookAuthor);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/books/{id}")]
        public ActionResult Details(int id)
        {
            Book book = db.Books.FirstOrDefault(books => books.BookId == id);
            BookCopy bookCopy = db.BooksCopies.FirstOrDefault(books => books.BookId == id);
            Copy copy = db.Copies.FirstOrDefault(copies => copies.CopyId == bookCopy.CopyId);
            var entryList = db.BookAuthors.Where(entry => entry.BookId == id).ToList();
            List<Author> authorList = new List<Author>();
            foreach (var author in entryList)
            {
                int authorId = author.AuthorId;
                authorList.Add(db.Authors.FirstOrDefault(record => record.AuthorId == authorId));
            }
            ViewBag.AuthorList = authorList;
            ViewBag.CopiesCount = copy.Number;
            return View(book);
        }

        [HttpGet("books/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Book book = db.Books.FirstOrDefault(books => books.BookId == id);
            ViewBag.AuthorIds = db.Authors.ToList()
                .Select(author => new SelectListItem {
                    Value = author.AuthorId.ToString(),
                    Text = author.AuthorFirstName + " " + author.AuthorLastName
                })
                .ToList();
            return View(book);
        }

        [HttpPost("books/{id}/edit")]
        public ActionResult Edit(Book book, List<int> AuthorIds)
        {
            db.Entry(book).State = EntityState.Modified;
            var bookMatchesInJoinTable = db.BookAuthors.Where(entry => entry.BookId == book.BookId).ToList();
            // Remove all BookAuthor entries for specified book.
            foreach (var author in bookMatchesInJoinTable)
            {
                int authorId = author.AuthorId;
                var joinEntry = db.BookAuthors
                                  .Where(entry => entry.AuthorId == authorId)
                                  .Where(entry => entry.BookId == book.BookId);
                foreach (var entry in joinEntry)
                {
                    db.BookAuthors.Remove(entry);
                }
            }
            // Add BookAuthor entries based on bookMatches.
            foreach (var id in AuthorIds)
            {
                Author author = db.Authors.FirstOrDefault(_ => _.AuthorId == id);
                BookAuthor newBookAuthor = new BookAuthor(book.BookId, author.AuthorId);
                db.BookAuthors.Add(newBookAuthor);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("books/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Book book = db.Books.FirstOrDefault(books => books.BookId == id);
            BookAuthor joinEntry = db.BookAuthors.FirstOrDefault(entry => entry.BookId == id);
            BookCopy bookCopy = db.BooksCopies.FirstOrDefault(entry => entry.BookId == id);
            Copy copy = db.Copies.FirstOrDefault(entry => entry.CopyId == bookCopy.CopyId);
            db.Books.Remove(book);
            db.BooksCopies.Remove(bookCopy);
            if (copy != null) db.Copies.Remove(copy);
            if (joinEntry != null) db.BookAuthors.Remove(joinEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
