using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    public class PagesListController : Controller
    {
        // GET: PagesList
        public ActionResult Index()
        {
            return View();
        }
    }
}