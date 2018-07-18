using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class AuthorsController : Controller
    {
        [HttpGet("/authors")]
        public ActionResult Index()
        {
            return View();
        }


    }
}
