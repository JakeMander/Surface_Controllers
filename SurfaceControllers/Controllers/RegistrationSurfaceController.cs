using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Schema;
using SurfaceControllers.Models;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;
using Umbraco.Web.Mvc;

namespace SurfaceControllers.Controllers
{
    public class RegistrationSurfaceController : SurfaceController
    {
        private IContentTypeBaseServiceProvider _contentTypeBaseServiceProvider;
        // GET: RegistrationSurface
        public ActionResult Index()
        {
            return PartialView("~/Views/Partials/RegisterForm.cshtml", new RegistrationFormModel());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(RegistrationFormModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            var memberService = Services.MemberService;
            
            //  First Check To Make Sure Our Newly Received Registration Request Has A Unique Username So We Are Not Duplicating Logins.
            if (memberService.GetByEmail(model.Email) != null)
            {
                ModelState.AddModelError("email", "An Account With This Email Currently Exists.");
                return CurrentUmbracoPage();
            }

            //  Use The MemberService API To First Create A Member Of Umbraco's Base Type And Fill in The Basic Properties.
            //  "CreateMemberWithIdentity" Persists The Changes And Is Equivalent To Calling "CreateMember()" and "Save()"
            var member = memberService.CreateMemberWithIdentity(model.Email, model.Email, model.Name, "bossMan");

            //  Set The Value Of The Properties We Created As Part Of Our Member Group.
            member.SetValue("biography", model.Biography);

            //  Some Jiggery-Pokery Required For Images In Umbraco 8. Umbraco 7 Used To Let You Get Away With "SetValue(model.Avatar)".
            //  However U8 Requires You To Pass In A _contentTypeBaseServiceProvider Then Extract The File Input From The HttpPostedFileBase object.
            member.SetValue(_contentTypeBaseServiceProvider, "avatar", model.Avatar.FileName, model.Avatar.InputStream);

            memberService.AssignRole(member.Username, "bossMan");

            //  Save The Submitted Password To The Member We've Just Created To Update The New Member Object.
            memberService.SavePassword(member, model.Password);

            //  Our User Has Essentially Authenticated, So Automatically Log Them in.
            Members.Login(model.Email, model.Password);

            //  We'll Still Need To Call Save To Persist The Change's We've Made Via The Calls To SetValue And SavePassword.
            memberService.Save(member);


            return RedirectToCurrentUmbracoPage();
        }
    }
}