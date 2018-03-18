using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CourseManager.Core.UnitOfWork;

namespace CourseManager.Core.Models
{
    public class CourseManager
    {
        public int Week { get; set; }
        public int Year { get; set; }
        public string WeekTitle { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<CourseHour> WeekCourseHours { get; set; }

        public CourseManager(IUnitOfWork unitOfWork, WeekYear curWeekYear)
        {
            Week = curWeekYear.Week;
            Year = curWeekYear.Year;

            var courseRepo = unitOfWork.Repository<Course>();
            Courses = courseRepo.GetAll();

            var hoursRepo = unitOfWork.Repository<CourseHour>();

            WeekCourseHours = hoursRepo.Select(h => h.Week == Week && h.Year == Year).ToList();

            WeekTitle = GetWeekTitle(curWeekYear);
        }

        public static readonly Func<WeekYear> GetCurrentWeek = () =>
        {
            CultureInfo myCi = CultureInfo.CurrentCulture;
            Calendar myCal = myCi.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCwr = CalendarWeekRule.FirstFourDayWeek;
            DayOfWeek myFirstDow = myCi.DateTimeFormat.FirstDayOfWeek;
            DateTime curDay = DateTime.Now;
            var curWeekYear = new WeekYear
            {
                Year = curDay.Year,
                Week = myCal.GetWeekOfYear(curDay, myCwr, myFirstDow)
            };
            return curWeekYear;
        };

        public static WeekYear GetNextWeekYear(WeekYear wyDto)
        {
            WeekYear result = new WeekYear();
            if (wyDto.Week >= 52)
            {
                DateTime lastDay = new DateTime(wyDto.Year, 12, 31);
                if ((wyDto.Week == 52 && lastDay.DayOfWeek > DayOfWeek.Thursday) || wyDto.Week == 53)
                {
                    result.Week = 1;
                    result.Year = wyDto.Year + 1;
                }
            }
            else
            {
                result.Week = wyDto.Week + 1;
                result.Year = wyDto.Year;
            }

            return result;
        }

        public static WeekYear GetPrevWeekYear(WeekYear wyDto)
        {
            WeekYear result = new WeekYear();
            if (wyDto.Week == 1)
            {
                result.Week = YearWeekCount(wyDto.Year);
                result.Year = wyDto.Year - 1;
            }
            else
            {
                result.Week = wyDto.Week - 1;
                result.Year = wyDto.Year;
            }

            return result;
        }
        //Get Last Week Number of Year
        private static readonly Func<int, int> YearWeekCount = year =>
        {
            CultureInfo myCi = new CultureInfo("uk-UA");
            Calendar myCal = myCi.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCwr = myCi.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDow = myCi.DateTimeFormat.FirstDayOfWeek;
            DateTime lastweek = new DateTime(year, 12, 31);
            return myCal.GetWeekOfYear(lastweek, myCwr, myFirstDow);
        };

        public static DateTime DateOfWeekIso8601(int year, int weekOfYear, int daynum)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            var result = firstThursday.AddDays(weekNum * 7);

            return result.AddDays(-4 + daynum);
        }
        public static string GetWeekTitle(WeekYear dto)
        {
            DateTime firstday = DateOfWeekIso8601(dto.Year, dto.Week, 1);
            DateTime lastday = DateOfWeekIso8601(dto.Year, dto.Week, 5);

            CultureInfo myCi = new CultureInfo("en-US");
            return
                $"Week {dto.Week}   {firstday.Date.ToString("ddd:dd-MM-yy", myCi)}   {lastday.Date.ToString("ddd:dd-MM-yy", myCi)}";
        }
    }

    public class CourseManagerView
    {
        public int Week { get; set; }
        public int Year { get; set; }
        public string WeekTitle { get; set; }
        public IEnumerable<CourseView> Courses { get; set; }
        public IEnumerable<CourseHourWeekView> WeekCourseHours { get; set; }
    }

    public class CourseManagerWeekView
    {
        public int Week { get; set; }
        public int Year { get; set; }
        public string WeekTitle { get; set; }
        public IEnumerable<CourseHourWeekView> WeekCourseHours { get; set; }
    }
}