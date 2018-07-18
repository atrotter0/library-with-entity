using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        [HttpPost("/books/new")]
        public ActionResult Create(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet("/books/{id}")]
        public ActionResult Details(int id)
        {
            Book book = db.Books.FirstOrDefault(books => books.BookId == id);
            return View(book);
        }

        [HttpGet("books/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Book book = db.Books.FirstOrDefault(books => books.BookId == id);
            return View(book);
        }

        [HttpPost("books/{id}/edit")]
        public ActionResult Edit(int id, Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("books/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Book book = db.Books.FirstOrDefault(books => books.BookId == id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
