using GraduationProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.MVC.Models
{
    public class ResultViewModel
    {
        public string FacultyName { get; set; }
        public string UniversityName { get; set; }
        public IEnumerable<string> Interests { get; set; }
        public List<Interest> SearchInterests { get; set; }
        public string Division { get; set; }
        public string Description { get; set; }
        public double avgDivisionstart { get; set; }
        public double avgDivisionend { get; set; }

        public double avgFacstart { get; set; }
        public double avgFacend { get; set; }
        public double grade { get; set; }
        public int recommendationId { get; set; }
        public int facultyId { get; set; }
        public int universityId { get; set; }
        public int divisionId { get; set; }
        public bool saved { get; set; }
        public int likelihood { get; set; }
    }
}