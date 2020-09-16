using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services.Description;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services.Implement;
using Umbraco.Web.Media;
using Umbraco.Web.WebApi.Filters;

namespace SurfaceControllers.Models
{
    public class GalleryDisplayModel
    {
        private IEnumerable<IPublishedContent> _imageList;

        public IEnumerable<IPublishedContent> ImageList
        {
            get
            {
                if(!_imageList.Any() || _imageList == null)
                {
                    throw new NullReferenceException("Image Collection Is Empty");
                }

                return _imageList;
            }

            set => _imageList = value;
        }

        [FileInputValidation(ErrorMessage = "Please Submit A Valid Set Of Files")]
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
    }
}