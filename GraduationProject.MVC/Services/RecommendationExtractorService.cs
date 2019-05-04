using GraduationProject.Data;
using GraduationProject.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.MVC.Services
{
    public class RecommendationExtractorService
    {

        public static ResultViewModel getRecommendationVMById(int recommendationId, bool saved = false)
        {
            DatabaseContext db = new DatabaseContext();
            var rec = db.Recommendations.Where(r => r.Id == recommendationId).First();
            var rvm = new ResultViewModel()
            {
                grade = rec.SearchHistory.Grade,
                FacultyName = rec.Division.Faculty.Name,
                UniversityName = rec.Division.Faculty.University.Name,
                Division = rec.Division.Name,
                Description = rec.Division.Description,
                SearchInterests = rec.SearchHistory.Interests.ToList(),

                saved = saved,

                facultyId = rec.Division.FacultyId,
                divisionId = rec.DivisionId,
                universityId = rec.Division.Faculty.UniversityId,
                recommendationId = rec.Id,

                avgDivisionstart = rec.Division.Tansiqs.Where(t => t.SpecializationId == rec.SearchHistory.SpecializationId).First().Startgrade,
                avgDivisionend = rec.Division.Tansiqs.Where(t => t.SpecializationId == rec.SearchHistory.SpecializationId).First().Endgrade,

                avgFacstart = rec.Division.Faculty.Tansiqs.Where(t => t.SpecializationId == rec.SearchHistory.SpecializationId).First().Startgrade,
                avgFacend = rec.Division.Faculty.Tansiqs.Where(t => t.SpecializationId == rec.SearchHistory.SpecializationId).First().Endgrade,
            };

            return rvm;
        }

        public static List<ResultViewModel> getRecommendationVMListByIdList (List<int> lIds, bool saved = false)
        {
            var lRvm = new List<ResultViewModel>();
            foreach(var i in lIds)
            {
                lRvm.Add(getRecommendationVMById(i, saved));
            }
            return lRvm;
        }
    }
}