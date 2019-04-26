 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.MVC.Models
{
    public class SearchViewModel
    {
        public string Grade { get; set; }
        public int SpecializationId { get; set; }
        public string Governorate { get; set; }
        public List<string> Interests { get; set; }
    }
}