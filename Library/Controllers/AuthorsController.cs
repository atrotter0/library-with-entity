using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        [HttpPost("/authors/new")]
        public ActionResult Create(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/authors/{id}")]
        public ActionResult Details(int id)
        {
            Author author = db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
            return View(author);
        }

        [HttpGet("authors/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Author author = db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
            return View(author);
        }

        [HttpPost("authors/{id}/edit")]
        public ActionResult Edit(int id, Author author)
        {
            db.Entry(author).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("authors/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Author author = db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
