﻿using AutoMapper;
using LinkDev.IKEA.BLL.DTOs.Departments;
 using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        #region SERVICES
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<Department> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService, ILogger<Department> logger, IWebHostEnvironment environment,IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index
        [HttpGet] //Get:/Departments/Index
        public async Task < IActionResult> Index()
        {
            var departments =await  _departmentService.GetAllDeparmentsAsync();
            return View(departments);

        }
        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(DepartmentViewModel departmentvm)
        {
            if (!ModelState.IsValid)
                return View(departmentvm);

            var Message = string.Empty;
            try
            {
                var CreatedDepartment =   _mapper.Map<CreatedDepartmentDto>(departmentvm);
                //{
                //    Code = departmentvm.Code,
                //    Name = departmentvm.Name,
                //    CreationDate = departmentvm.CreationDate,
                //    Description = departmentvm.Description,
                //};
                var Created = await  _departmentService.CreateDepartmentAsync(CreatedDepartment) >0;

                if (!Created)
                {
                    Message = "Department is not created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentvm);
                   

                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Message = _environment.IsDevelopment() ? ex.Message : "Failed To Create";

                TempData["Message"]=Message;
                return RedirectToAction("Index");

            }
            
            return Redirect(nameof(Index));
        }
        #endregion
         
        #region Details

        [HttpGet]
        public async Task <IActionResult> Details(int? id)
        {
            if (id is null)
            {

                return BadRequest();
            }
            var department =await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
            {
                return NotFound();

            }
            return View(department);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task <IActionResult> Edit(int? id)
        {
            if (id is null)
            {

                return BadRequest();
            }
            var department =await  _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
            {
                return NotFound();

            }
            var DepartmentVM= _mapper.Map<DepartmentDetailsDto,DepartmentViewModel>(department);
            return View(DepartmentVM);
            //{

            //    Code = department.Code,
            //    Name = department.Name,
            //    Description = department.Description,
            //    CreationDate = department.CreationDate,

            //});
        }
        

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var message = string.Empty;
            try
            {
                var departmentToUpdate = _mapper.Map<UpdatedDepartmentDto>(departmentVM);
                //{
                //    Id = id,
                //    Code = departmentVM.Code,
                //    Name = departmentVM.Name,
                //    Description = departmentVM.Description,
                //    CreationDate = departmentVM.CreationDate,
                //};
                var Updated =await _departmentService.UpdateDepartmentAsync(departmentToUpdate) > 0;

                if (Updated)
                    return RedirectToAction("Index");

                message = "an error has occured during updating the department";

            }
            catch (Exception ex)
            {
                //1.log Exception
                _logger.LogError(ex, ex.Message);

                //2.set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the department";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);
        }
        #endregion

        #region Delete

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var department = _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
                return NotFound();

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task< IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {

                var Deleted = await _departmentService.DeleteDepartmentAsync(id);
                if (Deleted)
                    return RedirectToAction("Index");

                message = " an error has occured during Deleting the department ";

            }
            catch (Exception ex)
            {
                //1.log Exception
                _logger.LogError(ex, ex.Message);

                //2.set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the department";

            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));
        }  
        #endregion
       
    }
}
