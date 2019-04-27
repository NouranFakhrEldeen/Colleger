using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Entities
{
   public class Faculty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] logo { get; set; }
        public int searchAppearancesCount { get; set; }
        [ForeignKey("University")]
        public int UniversityId { get; set; }
        public virtual University University { get; set; }
        public virtual ICollection<Tansiq> Tansiqs { get; set; }
        public virtual ICollection<Division> Division { get; set; }




    }
}
