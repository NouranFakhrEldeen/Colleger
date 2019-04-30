using GraduationProject.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GraduationProject.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Dashboard
        public ActionResult Index()
        {
            
            ViewBag.usersCount = db.Students.Count();
            ViewBag.universitiesCount = db.Universities.Count();
            ViewBag.searchCount = db.SearchHistories.Count();
            ViewBag.savedRecommendations = db.Recommendations.Count(r => r.UserId != null);

            ViewBag.countPerDayLabels = db.SearchHistories
                .GroupBy(x => DbFunctions.TruncateTime(x.Timestamp))
                .Select(x => new
                {
                    Day = (DateTime)x.Key
                }).ToList();
            ViewBag.countPerDayData = db.SearchHistories
                .GroupBy(x => DbFunctions.TruncateTime(x.Timestamp))
                .Select(x => new
                {
                        Value = x.Count()
                }).ToList();


            return View();
        }
    }
}