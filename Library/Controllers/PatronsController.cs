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
            var entryList = db.PatronsCopies.Where(entry => entry.PatronId == id).ToList();
            List<Copy> copyList = new List<Copy>();
            foreach (var copy in entryList)
            {
                int copyId = copy.CopyId;
                copyList.Add(db.Copies.FirstOrDefault(record => record.CopyId == copyId));
            }
            ViewBag.CopyList = copyList;
            return View(patron);
        }


        [HttpGet("patrons/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Patron patron = db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            ViewBag.CopyIds = db.Copies.ToList()
                .Select(copy => new SelectListItem
                {
                    Value = copy.CopyId.ToString(),
                    Text = copy.CopyId.ToString()
                })
                .ToList();
            return View(patron);
        }

        [HttpPost("patrons/{id}/edit")]
        public ActionResult Edit(Patron patron, List<int> CopyIds)
        {
            db.Entry(patron).State = EntityState.Modified;
            var patronMatchesInJoinTable = db.PatronsCopies.Where(entry => entry.PatronId == patron.PatronId).ToList();
            // Remove all PatronCopy entries for specified patron.
            foreach (var copy in patronMatchesInJoinTable)
            {
                int copyId = copy.CopyId;
                var joinEntry = db.PatronsCopies
                                  .Where(entry => entry.CopyId == copyId)
                                  .Where(entry => entry.PatronId == patron.PatronId);
                foreach (var entry in joinEntry)
                {
                    db.PatronsCopies.Remove(entry);
                }
            }
            // Add BookAuthor entries based on authorMatches.
            foreach (var id in CopyIds)
            {
                Copy copy = db.Copies.FirstOrDefault(_ => _.CopyId == id);
                PatronCopy newPatronCopy = new PatronCopy(patron.PatronId, copy.CopyId);
                db.PatronsCopies.Add(newPatronCopy);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("patrons/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Patron patron = db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            PatronCopy joinEntry = db.PatronsCopies.FirstOrDefault(entry => entry.PatronId == id);
            db.Patrons.Remove(patron);
            if (joinEntry != null) db.PatronsCopies.Remove(joinEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
