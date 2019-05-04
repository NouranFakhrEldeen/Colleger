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

            var SearchInterestsIds = rec.SearchHistory.Interests.Select(i => i.Id).ToList();

            rvm.SearchInterests = rec.Division.Interests.Where(i => SearchInterestsIds.Contains(i.Id)).ToList();

            if (SearchInterestsIds.Count > 0)
            {
                rvm.Interests = rec.Division.Interests.Where(i => !SearchInterestsIds.Contains(i.Id)).Select(i => i.name).ToList();
            } else
            {
                rvm.Interests = rec.Division.Interests.Select(i => i.name).ToList();
            }

            rvm.likelihood = getLikelihood(rec.SearchHistory.Grade, divisionTansiq.Startgrade, divisionTansiq.Endgrade);

            rvm.Fees = rec.Division.Fees;

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

        public static int getLikelihood(double sGrade, double min, double max)
        {
            double avg = (min + max) / 2;
            
            if (sGrade >= min && sGrade < avg)
            {
                return 0;
            }
            else if(sGrade >= avg && sGrade < max)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}