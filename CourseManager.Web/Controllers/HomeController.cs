using System.Web.Mvc;
using AutoMapper;
using CourseManager.Core.Models;
using CourseManager.Core.UnitOfWork;
using CourseManager.Web.AutoMapper;

namespace CourseManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MappingProfile.Mapper;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var curWeekYear = Core.Models.CourseManager.GetCurrentWeek();

            var manager = new Core.Models.CourseManager(_unitOfWork, curWeekYear);

            var view = _mapper.Map<Core.Models.CourseManager, CourseManagerView>(manager);

            return View(view);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}