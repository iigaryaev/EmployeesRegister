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
    public class UsersController : Controller
    {
        private readonly AuthUnitOfWork auth;

        public UsersController()
        {
            this.auth = new AuthUnitOfWork(new EmployeeRegisterDb());
        }
        // GET: Users
        public ActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Add(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = this.auth.UserRepository.Register(model.Login, model.Password);
               
                if (res.IsSuccess)
                {
                    var user = this.auth.UserRepository.GetByLogin(this.User.Identity.Name);
                    this.auth.Save(user);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("Login", res.MEssage);
            }

            return View(model);
        }
    }
}