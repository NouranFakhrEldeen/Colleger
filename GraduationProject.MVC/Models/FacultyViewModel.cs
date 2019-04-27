using GraduationProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.MVC.Models
{
    public class FacultyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] logo { get; set; }
        public int UniversityId { get; set; }
        public List<Tansiq> Tansiq { get; set; }

    }
}