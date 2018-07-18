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
        public IActionResult Details(int id)
        {
            Book book = db.Books.FirstOrDefault(books => books.Id == id);
            return View(book);
        }

    }
}
