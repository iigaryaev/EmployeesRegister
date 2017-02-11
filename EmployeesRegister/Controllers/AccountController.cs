using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EmployeesRegister.Models;
using EmployeesRegister.Models.Database;
using System.Web.Security;

namespace EmployeesRegister.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly EmployeeRegisterDb db;
        private readonly AuthContext auth;

        public AccountController()
        {
            this.db = new EmployeeRegisterDb();
            this.auth = new AuthContext(this.db);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var account = this.auth.Authorize(model.Login, model.Password);
            if (account == null)
            {
                ModelState.AddModelError("Login", "Password incorrect or login not exists");
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(model.Login, true);
            return this.RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}