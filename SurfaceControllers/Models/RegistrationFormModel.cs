using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace SurfaceControllers.Models
{
    public class RegistrationFormModel
    {
        
        [Required (ErrorMessage = "Please Enter Your Name")] 
        public string Name { get; set; }

        [Required (ErrorMessage = "Please Enter An Email Address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required (ErrorMessage = "Please Enter A Password")] 
        public string Password { get; set; }

        public string Biography { get; set; }

        [FileInputValidation]
        public HttpPostedFileBase Avatar { get; set; }
    }
}