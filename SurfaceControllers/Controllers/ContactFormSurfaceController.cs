using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SurfaceControllers.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace SurfaceControllers.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            //  When A Client Requests Our Contact Page View, The Server Renders The Markup, But Also Fires off A New Request For Our Partial View Which Is Received By The Controller.
            //  The Snippet Below Grabs The File Containing The Markup And Writes The Markup To The HTTP Response. It Then  The Calling View Then Carries On Rendering The Remainder of It's
            //  Own 
            return PartialView("~/Views/Partials/ContactForm.cshtml", new ContactFormModel());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactFormModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            var message = new MailMessage();
            message.To.Add(model.Email);
            message.Subject = "New Contact Form Submission From " + model.Name;
            message.From = new MailAddress(model.Email);
            message.Body = model.Message;

            var smtp = new SmtpClient("smtp.outlook.com")
            {
                Credentials = new NetworkCredential("jmmander1@hotmail.co.uk", "PASSWORD NEEDS TO BE INSERTED HERE"),
                EnableSsl = true
                
            };

            smtp.Send(message);

            TempData["success"] = true;

            return RedirectToCurrentUmbracoPage();
        }
    }
}