using System.Data.Entity;
using CourseManager.Core.Models;

namespace CourseManager.Core
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<CourseManagerContext>
    {

        protected override void Seed(CourseManagerContext context)
        {
            var courses = new[]
            {
                new Course
                {
                    Name = "Intelligent Internet Technologies",
                    Color = "#dd0f20"
                },
                new Course
                {
                    Name = "Intelligent Information Systems",
                    Color = "#f18a31"
                },
                new Course
                {
                    Name = "Information Security",
                    Color = "#f8eb48"
                },
                new Course
                {
                    Name = "Information Systems",
                    Color = "#16813d"
                },
                new Course
                {
                    Name = "Information Systems in Jurisprudence",
                    Color = "#2de3d8"
                },
                new Course
                {
                    Name = "Computer graphics",
                    Color = "#121ae3"
                },
                new Course
                {
                    Name = "Electronic Commerce Systems",
                    Color = "#6412cc"
                }
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();
        }
    }
}
