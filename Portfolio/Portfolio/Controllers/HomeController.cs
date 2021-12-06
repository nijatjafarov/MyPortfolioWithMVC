using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;
using Portfolio.Models;
using System.Web.Mvc;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var context = new NijatPortfolioEntities();

            ViewBag.AboutMe = context.AboutMes.FirstOrDefault();
            ViewBag.InfoList = context.AdditionalInfoes.ToList<AdditionalInfo>();
            ViewBag.Technologies = context.Technologies.ToList<Technology>();
            ViewBag.Tasks = context.Tasks;
            ViewBag.Reviews = context.Reviews.ToList<Review>();

            return View();
        }
    }
}