using AutoMapper;

namespace CourseManager.Web.AutoMapper
{
    public static class MappingProfile
    {
        public static readonly IMapper Mapper = InitializeAutoMapper().CreateMapper();

        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebMappingProfile());  //mapping between Web and Business layer objects
            });

            return config;
        }
    }
}
