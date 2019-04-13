using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GraduationProject.Data.Entities;

namespace GraduationProject.Data
{
    public class DatabaseContext: DbContext
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
    }
}
