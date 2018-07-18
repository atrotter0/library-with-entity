using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CopiesController : Controller
    {
        [HttpGet("/copies")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
