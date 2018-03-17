using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManager.Core.Models
{
    public sealed class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public ICollection<CourseHour> CourseHours { get; set; }

        public Course()
        {
            CourseHours = new HashSet<CourseHour>();
        }
    }

    public class CourseView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
