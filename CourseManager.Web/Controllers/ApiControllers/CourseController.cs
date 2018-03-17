using System.Web.Http;
using AutoMapper;
using CourseManager.Core.Models;
using CourseManager.Core.UnitOfWork;
using CourseManager.Web.AutoMapper;
using CourseManager.Web.Models;

namespace CourseManager.Web.Controllers.ApiControllers
{
    [RoutePrefix("api/course")]
    public class CourseController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MappingProfile.Mapper;

        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(CourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var courseRepo = _unitOfWork.Repository<Course>();
            var course = _mapper.Map<CourseDto, Course>(dto);
            courseRepo.Create(course);
            courseRepo.Save();

            var view = _mapper.Map<Course, CourseView>(course);

            return Ok(view);
        }

        [HttpPut]
        [Route("name")]
        public IHttpActionResult UpdateCourseName(EditCourseNameDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var courseRepo = _unitOfWork.Repository<Course>();
            var course = courseRepo.Get(dto.Id);
            if (course == null)
            {
                return NotFound();
            }
            course.Name = dto.Name;
            courseRepo.Update(course);
            courseRepo.Save();

            var view = _mapper.Map<Course, CourseView>(course);

            return Ok(view);
        }

        [HttpPut]
        [Route("color")]
        public IHttpActionResult UpdateCourseColor(EditCourseColorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var courseRepo = _unitOfWork.Repository<Course>();
            var course = courseRepo.Get(dto.Id);
            if (course == null)
            {
                return NotFound();
            }
            course.Color = dto.Color;
            courseRepo.Update(course);
            courseRepo.Save();

            var view = _mapper.Map<Course, CourseView>(course);

            return Ok(view);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult RemoveCourse(int id)
        {
            var courseRepo = _unitOfWork.Repository<Course>();

            var course = courseRepo.Get(id);
            if (course == null)
            {
                return NotFound();
            }

            courseRepo.Delete(course);
            courseRepo.Save();

            return Ok();
        }
    }
}
