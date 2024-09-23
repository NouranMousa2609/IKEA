using LinkDev.IKEA.BLL.DTOs.Departments;
using LinkDev.IKEA.BLL.DTOs.Employees;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.PL.ViewModels.Departments;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region SERVICES
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<Employee> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService, ILogger<Employee> logger, IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index
        [HttpGet] //Get:/Departments/Index
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);

        }
        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var message = string.Empty;
            try
            {
                var Result = _employeeService.CreateEmployee(employee);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Employee is not created";
                    ModelState.TryAddModelError(string.Empty, message);
                    return View(employee);
                }

            }
            catch (Exception ex)
            {
                //1.log Exception
                _logger.LogError(ex, ex.Message);

                //2.set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during creating the employee";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(employee);
        }
        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
            {

                return BadRequest();
            }
            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
            {
                return NotFound();

            }
            return View(employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {

                return BadRequest();
            }
            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
            {
                return NotFound();

            }
            return View(new UpdatedEmployeeDto()
            {
                Name=employee.Name,
                Address=employee.Address,
                Age=employee.Age,
                Email=employee.Email,
                HiringDate=employee.HiringDate,
                IsActive=employee.IsActive,
                PhoneNumber=employee.PhoneNumber,
                Salary=employee.Salary,
                EmployeeType=employee.EmployeeType,
                Gender=employee.Gender,

               

            });
        }




        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            var message = string.Empty;
            try
            {
                
                var Updated = _employeeService.UpdateEmployee(employee) > 0;

                if (Updated)
                    return RedirectToAction("Index");

                message = "an error has occured during updating the employee";

            }
            catch (Exception ex)
            {
                //1.log Exception
                _logger.LogError(ex, ex.Message);

                //2.set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the employee";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(employee);
        }
        #endregion

        #region Delete

      

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {

                var Deleted = _employeeService.DeleteEmployee(id);
                if (Deleted)
                    return RedirectToAction("Index");

                message = " an error has occured during Deleting the employee ";

            }
            catch (Exception ex)
            {
                //1.log Exception
                _logger.LogError(ex, ex.Message);

                //2.set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the employee";

            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));
        }  
        #endregion
       
    }
}
