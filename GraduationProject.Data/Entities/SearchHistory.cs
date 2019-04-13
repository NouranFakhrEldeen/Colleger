using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Entities
{
    public class SearchHistory
    {
        [Key]
        public int Id { get; set; }
        public string Governorate { get; set; }
        public string Grade { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Anonymous { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }

        public virtual ICollection<Recommendation> Recommendations { get; set; }
    }
}
