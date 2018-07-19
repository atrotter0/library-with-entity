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
            List<Book> bookList = db.Books.ToList();
            return View(bookList);
        }

        [HttpGet("/books/new")]
        public ActionResult Create()
        {
            ViewBag.AuthorId = db.Authors.ToList()
                .Select(author => new SelectListItem {
                    Value = author.AuthorId.ToString(), Text = author.AuthorFirstName + " " + author.AuthorLastName
                })
                .ToList();
            return View();
        }

        [HttpPost("/books/new")]
        public ActionResult Create(Book book, int AuthorId)
        {
            db.Books.Add(book);
            BookAuthor newBookAuthor = new BookAuthor(book.BookId, AuthorId);
            db.BookAuthors.Add(newBookAuthor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/books/{id}")]
        public ActionResult Details(int id)
        {
            Book book = db.Books.FirstOrDefault(books => books.BookId == id);
            var entryList = db.BookAuthors.Where(entry => entry.BookId == id).ToList();
            List<Author> authorList = new List<Author>();
            foreach (var author in entryList)
            {
                int authorId = int.Parse(author.AuthorId.ToString());
                authorList.Add(db.Authors.FirstOrDefault(record => record.AuthorId == authorId));
            }
            ViewBag.AuthorList = authorList;
            return View(book);
        }

        [HttpGet("books/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Book book = db.Books.FirstOrDefault(books => books.BookId == id);
            ViewBag.AuthorId = db.Authors.ToList()
                .Select(author => new SelectListItem {
                    Value = author.AuthorId.ToString(), Text = author.AuthorFirstName + " " + author.AuthorLastName
                })
                .ToList();
            return View(book);
        }

        [HttpPost("books/{id}/edit")]
        public ActionResult Edit(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("books/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Book book = db.Books.FirstOrDefault(books => books.BookId == id);
            BookAuthor joinEntry = db.BookAuthors.FirstOrDefault(entry => entry.BookId == id);
            db.Books.Remove(book);
            db.BookAuthors.Remove(joinEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
