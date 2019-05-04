using GraduationProject.Data;
using GraduationProject.Data.Entities;
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
            ResultViewModel rvm = new ResultViewModel();

            rvm.grade = rec.SearchHistory.Grade;
            rvm.FacultyName = rec.Division.Faculty.Name;
            rvm.UniversityName = rec.Division.Faculty.University.Name;
            rvm.Division = rec.Division.Name;
            rvm.Description = rec.Division.Description;
            rvm.SearchInterests = rec.SearchHistory.Interests.ToList();

            rvm.saved = saved;

            rvm.facultyId = rec.Division.FacultyId;
            rvm.divisionId = rec.DivisionId;
            rvm.universityId = rec.Division.Faculty.UniversityId;
            rvm.recommendationId = rec.Id;

            var divisionTansiq = rec.Division.Tansiqs.Where(t => t.SpecializationId == rec.SearchHistory.SpecializationId).First();

            rvm.avgDivisionstart = divisionTansiq.Startgrade;
            rvm.avgDivisionend = divisionTansiq.Endgrade;
            Tansiq facultyTansiq;
            try
            {
                facultyTansiq = rec.Division.Faculty.Tansiqs.Where(t => t.SpecializationId == rec.SearchHistory.SpecializationId).First();
            } catch (Exception e)
            {
                facultyTansiq = divisionTansiq;
            }

            rvm.avgFacstart = facultyTansiq.Startgrade;
            rvm.avgFacend = facultyTansiq.Endgrade;
            var divIntIds = rec.Division.Interests.Select(i => i.Id).ToList();
            rvm.Interests = rec.SearchHistory.Interests.Where(i => divIntIds.Contains(i.Id)).Select(i => i.name);


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