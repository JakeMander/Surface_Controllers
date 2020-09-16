using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using SurfaceControllers.Models;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;

namespace SurfaceControllers.Controllers
{
    public class CommentFormSurfaceController : SurfaceController
    {
        // GET: CommentForm
        public ActionResult Index()
        {
            return PartialView("~/Views/Partials/CommentForm.cshtml");
        }


        [HttpPost]
        public ActionResult HandleFormSubmit(CommentFormModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();


            var createdContent = Services.ContentService.Create(model.Name + "_" + DateTime.Now, CurrentPage.Id, "comment");
            createdContent.SetValue("usersName", model.Name);
            createdContent.SetValue("email", model.Email);
            createdContent.SetValue("postComment", model.Message);

            Services.ContentService.SaveAndPublish(createdContent);

            TempData["success"] = true;


            return RedirectToCurrentUmbracoPage();
        }
    }
}
