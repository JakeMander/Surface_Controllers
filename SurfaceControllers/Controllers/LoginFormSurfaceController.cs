using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurfaceControllers.Models;
using Umbraco.Web.Mvc;

namespace SurfaceControllers.Controllers
{
    public class LoginFormSurfaceController : SurfaceController
    {
        // GET: LoginFormSurface
        public ActionResult Index()
        {
            return PartialView("~/Views/Partials/LoginForm.cshtml", new LoginFormModel());
        }

        public ActionResult Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            //  If Login Successful Redirect To The Hidden Page.
            if (Members.Login(model.Username, model.Password))
            {
                return Redirect("/example-page");
            }

            //  Login Has Failed - Return The Page That Was Rendered Previously And Update The Model Error State.
            ModelState.AddModelError("", "Invalid Login");
            return CurrentUmbracoPage();
        }

        public ActionResult Logout()
        {
            Members.Logout();
            return Redirect("/");
        }
    }
}