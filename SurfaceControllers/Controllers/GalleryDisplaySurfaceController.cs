using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using SurfaceControllers.Models;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;
using Umbraco.Web;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;

namespace SurfaceControllers.Controllers
{
    public class GalleryDisplaySurfaceController : SurfaceController
    {
        private int _galleryID = 1095;
        private IContentTypeBaseServiceProvider _contentTypeBaseServiceProvider;

        // GET: GalleryDisplay
        public ActionResult Index()
        {

            //  First return a list of all our images for the page in the "Gallery Images" folder. We'll then use this to render out our grid of images.
            var imagesOnPage= new List<Media>();

            IPublishedContent imagesFolder = Umbraco.Media(_galleryID);
            var galleryImages = imagesFolder.Children.OrderBy(x => x.CreateDate);


            return PartialView("~/Views/Partials/GalleryDisplay.cshtml", new GalleryDisplayModel {ImageList = galleryImages});
        }

        // POST: HandleFormSubmit
        public ActionResult HandleFormSubmit(GalleryDisplayModel model)
        {
            Debug.Print("Current Value Stored In Files: {0}", model.Files.ToString());

            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            
            //  Iterate Through Our Files That We've Retrieved From Our Request
            foreach (var file in model.Files)
            {
                var media = Services.MediaService.CreateMedia(file.FileName, _galleryID, "Image");
                media.SetValue(_contentTypeBaseServiceProvider,"umbracoFile", file.FileName, file.InputStream);
                Services.MediaService.Save(media);
            }


            return RedirectToCurrentUmbracoPage();
        }

    }
}