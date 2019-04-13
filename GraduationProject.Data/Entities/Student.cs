using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Entities
{
    public class Student : User
    {
        
        public double Grade { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Governorate { get; set; }
        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<Recommendation> Recommendation { get; set; }


    }
}
