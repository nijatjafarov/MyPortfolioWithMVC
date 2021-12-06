using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class BlogsController : Controller
    {
        // GET: Blogs
        public ActionResult Index()
        {
            var context = new NijatPortfolioEntities();
            ViewBag.Blogs = context.Blogs.ToList<Blog>();

            return View();
        }
    }
}