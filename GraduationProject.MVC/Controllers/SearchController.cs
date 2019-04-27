using GraduationProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GraduationProject.MVC.Models;
using GraduationProject.Data.Entities;
namespace GraduationProject.MVC.Controllers
{
    public class SearchController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Search
        public ActionResult Index()
        {
        //    var userId = int.Parse(User.Identity.GetUserId());
            
        //    ViewBag.specialization = db.Students.Where(r=>r.Id== userId).Select(a=>a.SpecializationId);
            ViewBag.specializationId = new SelectList(db.Specializations, "Id", "Name");
           // ViewBag.GovernorateId = db.Universities.Select(a => a.Governorate);
            //    ViewBag.Governorate = db.Students.Where(a => a.Id == userId).Select(n => n.Governorate);
            return View();
        }

        [HttpPost]
        public ActionResult Result( int SpecializationId , double? Startgrade, List<string> Interests, University university)
        {

            var interstids = db.Interests.Select(a => a.Id).ToList();
            var result = db.Tansiq.Where(a => a.SpecializationId == SpecializationId /*&& a.Faculty.University.Governorate == university.Governorate*/ && a.Startgrade < Startgrade
            ).ToList();
            //var result2 = db.Inerests.Contains( )
            //var result1 = db.Tansiq.Where(a => a.Division.Interests.Where(r => interstids.Contains(a.Id)) == Interests).ToList();
            //    .Select(n=>new { x = n.Faculty.Name , y = n.Faculty.University.Name , r = n.Division.Interests.SelectMany(a=>a.name)}) ;
            
            return View();
        }

        [HttpGet]
        public ActionResult Result()
        {
            return View();
        }


    }
}