using MongoDB.Bson;
using PatientPortal.BackEnd;
using PatientPortal.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PatientPortal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private PatientRepository _repository = new PatientRepository();

        const string _PPTGENDIR = "target";

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View(_repository.GetAllPatientsWithFileInfo().ToList<PatientViewModel>());
        }

        [Authorize]
        public ActionResult AddPatient()
        {
            return PartialView(new PatientViewModel(new PatientModel(), new List<FileModel>()));
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddPatient(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.AddPatient(model.Patient);
                }
                catch (MongoDB.Driver.MongoSafeModeException e)
                {
                    Response.TrySkipIisCustomErrors = true; 
                    Response.StatusCode = 500;
                    ModelState.AddModelError("", "The Patient ID already exists in the database.");
                }
                //return RedirectToAction("Index");
                return PartialView(model);
            }
            else
            {
                Response.TrySkipIisCustomErrors = true; 
                Response.StatusCode = 500;
                return PartialView(model);
            }
        }

        [HttpGet]
        public ActionResult PatientDetails(string id)
        {
            var model = _repository.GetPatientWithFiles(id);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult PatientDetails(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdatePatient(model);
                return RedirectToAction("Index");
            }
            //return View(model); 
            //TODO: Return properly with model errors (needs to be submitted with AJAX in dialog
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditPatient(string id)
        {
            var model = _repository.GetPatientWithFiles(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPatient(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdatePatient(model);
                return RedirectToAction("Index");
            }
            //return View(model); 
            //TODO: Return properly with model errors (needs to be submitted with AJAX in dialog
            return RedirectToAction("Index");
        }




        [HttpGet]
        public ActionResult PatientDelete(string id)
        {
            _repository.DeletePatient(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PatientLock(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Patient.Locked = true;
                _repository.UpdatePatient(model);
                return RedirectToAction("Index");
            }
            //return View(model); 
            //TODO: Return properly with model errors (needs to be submitted with AJAX in dialog
            return RedirectToAction("Index");

        }
        

        [HttpGet]
        public ActionResult Download(string id)
        {
            var filemodel = new FileModel();
            filemodel.ID = ObjectId.Parse(id);

            var fs = new FileStreamResult(_repository.DownloadFile(filemodel), " application/octet-stream");
            fs.FileDownloadName = filemodel.Filename;
            return fs;
        }

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files, string id)
        {
            foreach (HttpPostedFileBase file in files)
            {
                var fileinfo = new FileModel();
                fileinfo.Filename = file.FileName;
                fileinfo.PatientID = id;
                _repository.UploadFile(file.InputStream, fileinfo); 
                //string filePath = Path.Combine(Server.MapPath(".." + "/target"), file.FileName);
                //System.IO.File.WriteAllBytes(filePath, ReadData(file.InputStream));
            }

            return Json("All files have been successfully stored.");
        }

        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        public ActionResult PPT(string id)
        {
            var patient = _repository.GetPatient(id);
            var bin = "pptx.jar";
            var resource_path = "/ngd/data";



            return Process(_repository.GetPatient(id));
        }


        [HttpPost]
        public FileResult Process(PatientPortal.Models.PatientModel p)
        {
            var fs = new FileStream(Server.MapPath("../..") + "\\Content\\template.pptx", System.IO.FileMode.Open, System.IO.FileAccess.Read);
            var ppt = new Aspose.Slides.Pptx.PresentationEx(fs);
            fs.Close();

            (ppt.Slides[0].Shapes[0] as Aspose.Slides.Pptx.AutoShapeEx).TextFrame.Text = p.ID;

            int x = 1;
            int fields_in_slide = 0;
            var unwritten_text = "";

            foreach (PropertyInfo prop in typeof(PatientModel).GetProperties())
            {
                if (prop.GetValue(p) != null)
                {
                    if (fields_in_slide == 5)
                    {
                        ppt.Slides.AddClone(ppt.Slides[1]);
                        fields_in_slide = 0;
                        x++;
                    }

                    string title;
                    DisplayAttribute[] attributes = (DisplayAttribute[])prop.GetCustomAttributes(typeof(DisplayAttribute), false);

                    if ((attributes != null) && (attributes.Length > 0))
                        title = attributes[0].Name;
                    else
                        title = prop.Name;

                    var value = prop.GetValue(p);

                    unwritten_text = title + ":" + prop.GetValue(p).ToString() + "\n";

                    (ppt.Slides[x].Shapes[1] as Aspose.Slides.Pptx.AutoShapeEx).TextFrame.Text += 
                    fields_in_slide++;

                }

                
                
            }
            //var img = ppt.Images.AddImage(System.Drawing.Image.FromFile(Server.MapPath("../..") + "\\target\\Picture1.png"));

            //ppt.Slides[2].Shapes.AddPictureFrame(ShapeTypeEx.Rectangle, 100, 100, img.Width, img.Height, img);

            ppt.Save(Server.MapPath("../..") + "/target/casepresentation.pptx", Aspose.Slides.Export.SaveFormat.Pptx);

            return new FileStreamResult(new FileStream(Server.MapPath("../..") + "/target/casepresentation.pptx", System.IO.FileMode.Open, System.IO.FileAccess.Read), "application/vnd.openxmlformats-officedocument.presentationml.presentation");
        }



    }
}
