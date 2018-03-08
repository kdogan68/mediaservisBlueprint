using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    [UserAuthorize]
    public class UpdateCatalogController : Controller
    {
        // GET: UpdateCatalog
        public ActionResult Index()
        {
            return View();
        }
    }
}