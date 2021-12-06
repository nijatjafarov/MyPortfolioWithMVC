using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class WorksController : Controller
    {
        // GET: Works
        public ActionResult Index()
        {
            var context = new NijatPortfolioEntities();
            ViewBag.Projects = context.Projects.ToList<Project>();

            return View();
        }
    }
}