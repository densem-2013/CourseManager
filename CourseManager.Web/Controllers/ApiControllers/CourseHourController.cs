using System.Web.Http;
using AutoMapper;
using CourseManager.Core.Models;
using CourseManager.Core.UnitOfWork;
using CourseManager.Web.AutoMapper;

namespace CourseManager.Web.Controllers.ApiControllers
{
    [RoutePrefix("api/coursehour")]
    public class CourseHourController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MappingProfile.Mapper;

        public CourseHourController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetWeekHours(int week,int year, bool next)
        {
            var dto = new WeekYear
            {
                Week = week,
                Year = year
            };
            WeekYear weekDto = next ? Core.Models.CourseManager.GetNextWeekYear(dto) : Core.Models.CourseManager.GetPrevWeekYear(dto);

            var manager = new Core.Models.CourseManager(_unitOfWork, weekDto);

            var view = _mapper.Map<Core.Models.CourseManager, CourseManagerWeekView>(manager);

            return Ok(view);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(CourseHourView dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hoursRepo = _unitOfWork.Repository<CourseHour>();
            var hour = hoursRepo.FirstOrDefault(h =>
                h.Week == dto.Week && h.Year == dto.Year && h.Day == dto.Day && h.Hour == dto.Hour);

            if (hour != null)
            {
                if (hour.CourseId != dto.CourseId)
                {
                    hour.CourseId = dto.CourseId;
                    hoursRepo.Update(hour);
                    hoursRepo.Save();

                    return Ok(dto);
                }

                hoursRepo.Delete(hour);
                hoursRepo.Save();

                return Ok();
            }

            hour = _mapper.Map<CourseHourView, CourseHour>(dto);
            hoursRepo.Create(hour);
            hoursRepo.Save();

            return Ok(dto);
        }
        
    }
}
