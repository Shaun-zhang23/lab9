namespace UniversityMVC.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UniversityMVC.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<UniversityMVC.Data.UniversityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "UniversityMVC.Data.UniversityContext";
        }

        protected override void Seed(UniversityMVC.Data.UniversityContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var campuses = new List<Campus>
            {
                new Campus {Name = "Manawatu Campus" },
                new Campus {Name = "Albany Campus" },
                new Campus {Name = "Wellington Campus" },
                new Campus {Name = "Distance Learning" }
            };

            campuses.ForEach(c => context.Campuses.AddOrUpdate(cc => cc.Name, c));
            context.SaveChanges();

            var students = new List<Student>
            {
                new Student {Name = "Alice Anderson", Address = "10 Summer Street", CampusID= campuses.Single(c=>c.Name == "Manawatu Campus").ID},
                new Student {Name = "Bob Brown", Address = "25 King Road", CampusID= campuses.Single(c=>c.Name == "Albany Campus").ID},
                new Student {Name = "Charlie Chen", Address = "3 Queen Avenue", CampusID= campuses.Single(c=>c.Name == "Wellington Campus").ID},
                new Student {Name = "David Davis", Address = "88 Church Lane", CampusID= campuses.Single(c=>c.Name == "Manawatu Campus").ID},
                new Student {Name = "Emma Evans", Address = "42 Market Street", CampusID= campuses.Single(c=>c.Name == "Albany Campus").ID},
                new Student {Name = "Frank Fletcher", Address = "7 Park Road", CampusID= campuses.Single(c=>c.Name == "Wellington Campus").ID},
                new Student {Name = "Grace Green", Address = "19 Hill Terrace", CampusID= campuses.Single(c=>c.Name == "Distance Learning").ID},
                new Student {Name = "Henry Harris", Address = "56 River Drive", CampusID= campuses.Single(c=>c.Name == "Distance Learning").ID},
                new Student {Name = "Ivy Ingram", Address = "31 Lake View", CampusID= campuses.Single(c=>c.Name == "Manawatu Campus").ID}
            };

            students.ForEach(s => context.Students.AddOrUpdate(st => st.Name, s));
            context.SaveChanges();
        }
    }
}
