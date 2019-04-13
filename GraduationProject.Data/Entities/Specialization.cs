using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Entities
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }



    }
}
