using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZibicoCMS.CommonHelpers;
using ZibicoCMS.Entity;

namespace ZibicoCMS.WebAdminUI.Controllers
{
    [ZbcAuthorize]
    public class GalleriesController : Controller
    {
        Helper help = new Helper();
        public ActionResult Index()
        {
            return View(help.GetAllPhotoGalleries());
        }
        public ActionResult Create(long? ID)
        {
            if (ID != null)
            {
                PhotoGalleries gallery = help.GetPhotoGallery((long)ID);
                if (gallery == null) return HttpNotFound();
                return View(gallery);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(PhotoGalleries gallery)
        {
            HttpPostedFileBase file = Request.Files["ImageURL"];

            if (string.IsNullOrWhiteSpace(gallery.ImageDescription))
            {
                ViewBag.MessageFail = "Fotoğraf açıklaması boş bırakılamaz!";
                return View();
            }
            
            string galleryID = Request.QueryString["ID"];
            if (galleryID == null)
            {
                if (file == null || file.FileName == "")
                {
                    ViewBag.MessageFail = "Fotoğraf boş bırakılamaz!";
                    return View();
                }

                bool result = Helper.SaveImg(file, out string path);
                gallery.Status = true;
                gallery.ImageURL = path;

                bool result2 = Helper.Insert(gallery);
                if (result2)
                {
                    ViewBag.Message = "Fotoğraf başarıyla eklendi.";
                    return View();
                }
                else
                {
                    ViewBag.MessageFail = "Fotoğraf kaydedilirken bir hata meydana geldi.";
                    return View();
                }
            }
            else
            {
                PhotoGalleries oldGal = help.GetPhotoGallery(long.Parse(galleryID));
                bool result = false;
                string path = "";
                if (file != null && file.FileName != "") result = Helper.SaveImg(file, out path);
                if (result) oldGal.ImageURL = path;
                
                oldGal.ImageDescription = gallery.ImageDescription;

                Helper.Update(oldGal);

                ViewBag.Message = "Fotoğraf başarıyla güncellendi.";
                return View(oldGal);

            }
        }
        
        public ActionResult Delete()
        {
            string ID = Request.QueryString["ID"];
            if (ID == null) return HttpNotFound();
            PhotoGalleries gal = help.GetPhotoGallery(long.Parse(ID));
            if (gal == null) return HttpNotFound();
            Helper.Delete(gal);

            return Redirect("/Galleries/Index?Status=DeleteSuccess");
        }
    }
}