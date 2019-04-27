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
        public ICollection<int> SearchInterests { get; set; }
        public string Division { get; set; }
        public string Description { get; set; }
        public double Average { get; set; }
        public double grade { get; set; }

    }
}