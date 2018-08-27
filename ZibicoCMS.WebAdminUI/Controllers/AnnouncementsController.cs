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
    public class AnnouncementsController : Controller
    {
        Helper help = new Helper();
        public ActionResult Index()
        {
            return View(help.GetAllAnnouncements());
        }
        public ActionResult Create(long? ID)
        {
            if (ID != null)
            {
                Announcements anno = help.GetAnnouncement((long)ID);
                if (anno == null) return HttpNotFound();
                return View(anno);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Announcements anno)
        {
            if (string.IsNullOrWhiteSpace(anno.AnnouncementTitle))
            {
                ViewBag.MessageFail = "Başlık boş bırakılamaz!";
                return View();
            }
            if (string.IsNullOrWhiteSpace(anno.AnnouncementContent))
            {
                ViewBag.MessageFail = "İçerik boş bırakılamaz!";
                return View();
            }

            string annoID = Request.QueryString["ID"];
            if (annoID == null)
            {
                anno.Status = true;
                anno.CreateDate = DateTime.Now;

                bool result = Helper.Insert(anno);
                if (result)
                {
                    ViewBag.Message = "Duyuru başarıyla eklendi.";
                    return View();
                }
                else
                {
                    ViewBag.MessageFail = "Duyuru kaydedilirken bir hata meydana geldi.";
                    return View();
                }
            }
            else
            {
                Announcements oldANno = help.GetAnnouncement(long.Parse(annoID));
                oldANno.AnnouncementContent = anno.AnnouncementContent;
                oldANno.AnnouncementTitle = anno.AnnouncementTitle;
                Helper.Update(oldANno);

                ViewBag.Message = "Duyuru başarıyla güncellendi.";
                return View(oldANno);
            }
        }
        
        public ActionResult Delete()
        {
            string ID = Request.QueryString["ID"];
            if (ID == null) return HttpNotFound();
            Announcements ann = help.GetAnnouncement(long.Parse(ID));
            if (ann == null) return HttpNotFound();
            Helper.Delete(ann);
            return Redirect("/Announcements/Index?Status=DeleteSuccess");
        }
    }
}