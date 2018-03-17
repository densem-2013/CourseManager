using System.ComponentModel.DataAnnotations;

namespace CourseManager.Web.Models
{
    public class CourseDto
    {
        [Required]
        [MaxLength(ErrorMessage = "Course Name's Length too long!!!")]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
    }

    public class EditCourseNameDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
    public class EditCourseColorDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Color { get; set; }
    }
}