using LinkDev.IKEA.BLL.DTOs.Departments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Entities.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<Department> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService, ILogger<Department> logger, IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }
        [HttpGet] //Get:/Departments/Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDeparments();
            return View(departments);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
                return View(department);

            var message = string.Empty;
            try
            {
                var Result = _departmentService.CreateDepartment(department);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Department is not created";
                    ModelState.TryAddModelError(string.Empty, message);
                    return View(department);
                }

            }
            catch (Exception ex)
            {
                //1.log Exception
                _logger.LogError(ex, ex.Message);

                //2.set Message
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;

                    return View(department);
                }
                else
                {
                    message = "Department is not created";
                    return View("Error", message);
                }
            }
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
            {

                return BadRequest();
            }
            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
            {
                return NotFound();

            }
            return View(department);
        }
    }
}
