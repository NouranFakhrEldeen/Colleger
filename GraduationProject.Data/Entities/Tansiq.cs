using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Entities
{
    public class Tansiq
    {
        [Key]
        public int Id { get; set; }
        public double Startgrade { get; set; }
        public double Endgrade { get; set; }
        public double Actual { get; set; }
        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
        [ForeignKey("Faculty")]
        public int? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public int? DivisionId { get; set; }
        public virtual Division Division { get; set; }



    }
}
