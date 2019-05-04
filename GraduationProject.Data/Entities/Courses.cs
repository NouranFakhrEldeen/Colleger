using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Entities
{
    public class Courses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Division> Divisions { get; set; }


    }
}
