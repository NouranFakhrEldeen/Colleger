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
        public ActionResult Result(int specializationId, double Startgrade, string iii, string Governorate)
        {
            var bb = iii.Split(',');
            var interstids = new List<int>();
            foreach (var b in bb)
            {

                interstids.Add(Int32.Parse(b));
            }

            var interestList = db.Interests.Where(a => interstids.Contains(a.Id));

            var result = db.Tansiq.Where(a => a.SpecializationId == specializationId && a.Division.Faculty.University.Governorate == Governorate && a.Startgrade < Startgrade).ToList();
            //var avg = db.Tansiq.Select(a=>a.Actual).Average();
            var result2 = result.Where(r => r.Division.Interests.Intersect(interestList).Any()).ToList();
           
            List<ResultViewModel> Results = new List<ResultViewModel>();
            
            foreach(var i in result2)
            {

            Results.Add(new ResultViewModel { FacultyName = i.Division.Faculty.Name , UniversityName = i.Division.Faculty.University.Name,
                Interests = i.Division.Interests.Select(a=>a.name) ,SearchInterests = interstids ,
                Description = i.Division.Description ,
                Division = i.Division.Name
                , Average = 0 , grade = Startgrade  });
            }


            SearchHistory searchHistory = new SearchHistory()
            {
                Governorate = Governorate,
                SpecializationId = specializationId,
                Grade = Startgrade,
                Timestamp = DateTime.Now


            };
            db.SearchHistories.Add(searchHistory);
            db.SaveChanges();

            foreach(var i in interestList)
            {
                searchHistory.Interests.Add(i);
            }
            db.SaveChanges();
            return View(Results);
        }
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in db.Universities where temp.Id == Id select temp.Logo;
            byte[] cover = q.First();
            return cover;
        }


    }
}