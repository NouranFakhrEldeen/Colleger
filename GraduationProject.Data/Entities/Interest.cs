using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Entities
{
    public class Interest
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public virtual ICollection<SearchHistory> SearchHistory { get; set; }

        public virtual ICollection<Division> Division { get; set; }

    }
}
