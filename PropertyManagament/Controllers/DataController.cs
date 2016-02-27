using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace PropertyManagament.Controllers
{
    public class DataController : Controller
    {

        [HttpPost]
        public JsonResult SaveFiles(string description)
        {
            string Message, fileName, actualFileName, pictureURL;
            Message = fileName = actualFileName = pictureURL = string.Empty;
            bool flag = false;
            if (Request.Files != null)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;

                try
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    pictureURL = Path.Combine(@"C:\PropertyPictures", fileName);
                    file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), fileName));
           
                    Message = "File uploaded successfully";
                    flag = true;
                   
                }
                catch (Exception exp)
                {
                    Message = String.Format("File upload failed! {0}", exp.Message);
                }

            }
            return new JsonResult { Data = new { Message = Message, Status = flag, URL = fileName } };
        }
    }
}

