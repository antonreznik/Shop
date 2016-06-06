using RealShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Ninject;
using System.Web.Security;
namespace RealShop.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Administrator"))
            {
                membership.CreateUserAndAccount("admin", "Mil@n2005");
                roles.CreateRole("Administrator");
                roles.AddUsersToRoles(new[] { "admin" }, new[] { "Administrator" });
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel obj)
        {
            if(ModelState.IsValid && WebSecurity.Login(obj.login,obj.password, persistCookie:false))
            {
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("","Ошибка ввода логина или пароля");
            return View(obj);
            
        }

    }
}
