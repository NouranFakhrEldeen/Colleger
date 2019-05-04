using GraduationProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GraduationProject.MVC.Models;
using GraduationProject.Data.Entities;
using GraduationProject.MVC.Services;

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
        public ActionResult Result(int specializationId, double Startgrade, string iii = "", string Governorate = "" , double Fees = 0, string course = "")
        {
            var bb = iii.Split(',');
            var interstids = new List<int>();
            if (iii.Length > 0)
            {
                foreach (var b in bb)
                {

                    interstids.Add(Int32.Parse(b));
                }
            }
            var cc = course.Split(',');
            var courseids = new List<int>();
            if(course.Length > 0)
            {
                foreach (var c in cc)
                {

                    courseids.Add(Int32.Parse(c));
                }
            }
            var courseList = db.Courses.Where(r => courseids.Contains(r.Id)).ToList();
            var interestList = db.Interests.Where(a => interstids.Contains(a.Id)).ToList();

            var result = db.Tansiq.Where(a => a.SpecializationId == specializationId && a.Startgrade <= Startgrade).ToList();


            if(interestList.Count > 0)
            {
                result = result.Where(r => r.Division.Interests.Intersect(interestList).Any()).ToList();
            }

            if(courseList.Count > 0)
            {
                result = result.Where(n => n.Division.Courses.Intersect(courseList).Any()).ToList();
            }

            if(Governorate != "" && Governorate != null)
            {
                result = result.Where(n => n.Division.Faculty.University.Governorate == Governorate).ToList();
            }

            if(Fees > 0)
            {
                result = result.Where(n => n.Division.Fees >= Fees).ToList();
            }



            SearchHistory searchHistory = new SearchHistory()
            {
                Governorate = Governorate,
                SpecializationId = specializationId,
                Grade = Startgrade,
                Timestamp = DateTime.Now,
                Interests = interestList
            };
            db.SearchHistories.Add(searchHistory);
            db.SaveChanges();

            foreach (var x in result)
            {
                db.Recommendations.Add(new Recommendation { SearchHistoryId = searchHistory.Id, DivisionId = x.Division.Id });
            }
            db.SaveChanges();

            var recIds = db.Recommendations.Where(r => r.SearchHistoryId == searchHistory.Id).Select(r => r.Id).ToList();

            List<ResultViewModel> Results = RecommendationExtractorService.getRecommendationVMListByIdList(recIds);

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