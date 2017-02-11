using EmployeesRegister.Models;
using EmployeesRegister.Models.Database;
using EmployeesRegister.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeesRegister.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AuthUnitOfWork auth;

        public HomeController()
        {
            this.auth = new AuthUnitOfWork(new EmployeeRegisterDb());
        }

        public ActionResult Index()
        {
            var employees = this.auth.EmployeeRepository.GetAll();
            return View(employees);
        }
        
        public ActionResult Add()
        {
            this.PrepareViewData();
            return this.View("Edit", new EmployeeViewModel());
        }

        public ActionResult Edit(int id)
        {
            var employee = this.auth.EmployeeRepository.GetById(id);
            var vm = new EmployeeViewModel { Age = employee.Age,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Id = employee.Id,
                Gender = employee.Gender,
                DepartmentId = employee.DepartmentId };

            this.PrepareViewData();
            return this.View("Edit", vm);
        }

        [HttpPost]
        public ActionResult SaveEmployee(EmployeeViewModel model)
        {
            var user = this.auth.UserRepository.GetByLogin(this.User.Identity.Name);
            if(!model.Age.HasValue || model.Age <= 0)
            {
                ModelState.AddModelError("Age", "Age must be a positive number");
            }

            if(!ModelState.IsValid)
            {
                this.PrepareViewData();
                return this.View("Edit", model);
            }

            Employee employee = new Employee();

            if(model.Id.HasValue)
            {
                employee = this.auth.EmployeeRepository.GetById(model.Id.Value);
            }

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Age = model.Age.Value;
            employee.DepartmentId = model.DepartmentId.Value;
            employee.Gender = model.Gender;

            if(model.Id.HasValue)
            {
                this.auth.EmployeeRepository.Update(employee);
            }
            else
            {
                this.auth.EmployeeRepository.Create(employee);
            }
            
            this.auth.Save(user);

            return this.RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            var user = this.auth.UserRepository.GetByLogin(this.User.Identity.Name);
            var employee = this.auth.EmployeeRepository.GetById(id);
            if(employee == null)
            {
                throw new Exception("Employee not found");
            }

            employee.IsActive = false;
            this.auth.EmployeeRepository.Update(employee);
            this.auth.Save(user);

            return this.RedirectToAction("Index");
        }

        public ActionResult Experience(int id)
        {
            var employee = this.auth.EmployeeRepository.GetById(id);
            this.PrepareExpViewData();
            return this.View(employee);           
        }

        public ActionResult AddExperience(int id, int languageId)
        {
            var user = this.auth.UserRepository.GetByLogin(this.User.Identity.Name);

            var existingExp = this.auth.SkillsRepository.EmployeeSkills(id);

            if(existingExp.All(e => e.ProgLanguageId != languageId))
            {
                this.auth.SkillsRepository.Create(new Skills { EmployeeId = id, ProgLanguageId = languageId });
                this.auth.Save(user);
            }

            return this.RedirectToAction("Experience", new { Id = id });
        }

        public ActionResult DeleteExperience(int id, int skillId)
        {
            var user = this.auth.UserRepository.GetByLogin(this.User.Identity.Name);

            var existingExp = this.auth.SkillsRepository.GetById(skillId);

            this.auth.SkillsRepository.Delete(existingExp);
            this.auth.Save(user);

            return this.RedirectToAction("Experience", new { Id = id });
        }

        [HttpPost]
        public ActionResult Search(SearchFilter model)
        {
            var employees = this.auth.EmployeeRepository.Search(model.Filter);
            
            return this.PartialView("EmployeesPartial", employees);
        }

        private void PrepareViewData()
        {
            this.ViewData["Departments"] = this.auth.DepartmentRepository.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name });
        }

        private void PrepareExpViewData()
        {
            this.ViewData["Languages"] = this.auth.ProgLanguageRepository.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name });
        }
    }
}