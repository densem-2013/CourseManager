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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Course>()
                .HasMany(s => s.CourseHours)
                .WithOptional(g => g.Course)
                .HasForeignKey(s => s.CourseId)
                .WillCascadeOnDelete();
        }
    }
}
