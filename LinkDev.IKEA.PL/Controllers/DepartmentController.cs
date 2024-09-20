using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Entities.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
       
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
           
        }
        [HttpGet] //Get:/Departments/Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDeparments();
            return View(departments);
            
        }
    }
}
