using System.Data.Entity;

namespace UniversityMVC.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext() : base("name=UniversityContext")
        {
        }

        public System.Data.Entity.DbSet<UniversityMVC.Models.Campus> Campuses { get; set; }

        public System.Data.Entity.DbSet<UniversityMVC.Models.Student> Students { get; set; }
    }
}
