using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Umbraco.Core;

namespace SurfaceControllers
{
    public class FileInputValidation : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            //  Empty Value Submitted. Set Model State To Invalid To Prevent An Empty File Going Across.
            if (value == null)
                return false;

            //  Check For Multiple Files Being Sent Across As Value Is Set To An Array of HttpPostedFileBase.
            //  Explicitly Cast To HttpPostedFileBase Else The Check Returns False Despite The Type Being Correct.
            if (value is HttpPostedFileBase[] files)
            {
                foreach (var file in files)
                {
                    if (file == null)
                    {
                        return false;
                    }

                    if (file.ContentLength > 1 * 1024 * 1024)
                    {
                        return false;
                    }
                }

                return base.IsValid(files);
            }

            //  Protect Against Single File Resulted In Cast To Single Instance of HttpPostedFileBase.
            if (value is HttpPostedFileBase singleFile)
            {
                if (singleFile.ContentLength > 1 * 1024 * 1024)
                {
                    return false;
                }

                return base.IsValid(singleFile);

            }

            //  Our Input Object Is Not A HttpPostedFile, So Invalidate The Model And Prevent Image Upload.
            return false;
        }
    }
}