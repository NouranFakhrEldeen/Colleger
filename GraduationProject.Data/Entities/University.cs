using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Entities
{
    public class University
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zipcode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Governorate { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }



        public virtual ICollection<Faculty> Faculty { get; set; }


    }
}
