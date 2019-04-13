using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Entities
{
    public class Division
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExtraInfo { get; set; }
        public int Views { get; set; }

        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }

        public virtual ICollection<Tansiq> Tansiqs { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }



    }
}
