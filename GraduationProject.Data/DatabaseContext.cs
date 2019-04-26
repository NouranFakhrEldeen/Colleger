using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GraduationProject.Data.Entities;

namespace GraduationProject.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }
        static DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(null);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Division> Division { get; set; }

        public virtual DbSet<Tansiq> Tansiq { get; set; }
        public virtual DbSet<Recommendation> Recommendations { get; set; }
       
        public System.Data.Entity.DbSet<GraduationProject.Data.Entities.Interest> Interests { get; set; }

        public System.Data.Entity.DbSet<GraduationProject.Data.Entities.University> Universities { get; set; }

        public System.Data.Entity.DbSet<GraduationProject.Data.Entities.Faculty> Faculties { get; set; }

        public System.Data.Entity.DbSet<GraduationProject.Data.Entities.Specialization> Specializations { get; set; }
        public System.Data.Entity.DbSet<GraduationProject.Data.Entities.Student> Students { get; set; }

    }
}
