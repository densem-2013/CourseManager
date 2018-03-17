using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManager.Core.Models
{
    public class CourseHour
    {
        [Key, Column(Order = 0)]
        public int Week { get; set; }
        [Key, Column(Order = 1)]
        public int Year { get; set; }
        [Key, Column(Order = 2)]
        public int CourseId { get; set; }
        [Key, Column(Order = 3)]
        public int Day { get; set; }
        [Key, Column(Order = 4)]
        public int Hour { get; set; }
        public virtual Course Course { get; set; }
    }

    public class CourseHourView
    {
        [Required]
        public int Week { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int Day { get; set; }
        [Required]
        public int Hour { get; set; }

    }
    public class CourseHourWeekView
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int Day { get; set; }
        [Required]
        public int Hour { get; set; }

    }
}
