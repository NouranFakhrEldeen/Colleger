using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Entities
{
    public class Recommendation
    {
        [Key]
        public int Id { get; set; }

      
        [ForeignKey("SearchHistory")]
        public int SearchHistoryId { get; set; }
        public virtual SearchHistory SearchHistory { get; set; }
        [ForeignKey("Division")]
        public int DivisionId { get; set; }
        public virtual Division Division { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }


    }
}
