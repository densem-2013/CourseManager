using System.Data.Entity;
using CourseManager.Core.Models;

namespace CourseManager.Core
{
    /// <inheritdoc />
    public class CourseManagerContext : DbContext
    {
        public CourseManagerContext()
            : base("name = DefaultConnection")
        {
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
        }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseHour> CourseHours { get; set; }

        static CourseManagerContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
    }
}
