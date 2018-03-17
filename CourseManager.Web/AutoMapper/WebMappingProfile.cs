using AutoMapper;
using CourseManager.Core.Models;
using CourseManager.Web.Models;

namespace CourseManager.Web.AutoMapper
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<Course, CourseView>();
            CreateMap<CourseDto, Course>();
            CreateMap<CourseHour, CourseHourView>(); ;
            CreateMap<CourseHour, CourseHourWeekView>();
            CreateMap<CourseHourView, CourseHour>();
            CreateMap<Core.Models.CourseManager, CourseManagerView>();
            CreateMap<Core.Models.CourseManager, CourseManagerWeekView>();
        }
    }
}
