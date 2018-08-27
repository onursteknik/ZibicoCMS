using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ZibicoCMS.CommonHelpers;
using ZibicoCMS.Entity;

namespace ZibicoCMS.WebUI.Controllers
{
    public class HomeController : Controller
    {
        Helper help = new Helper();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ClearSettingsCachePage()
        {
            Helper.RefreshSettingsCache("WebUI");
            return Redirect("/");
        }
        public ActionResult PartialHome()
        {
            return View();
        }
        public ActionResult PartialAbout()
        {
            return View();
        }
        public ActionResult PartialCategories()
        {
            ViewBag.Categories = help.GetLastCategories(9);
            return View();
        }
        public ActionResult PartialMenuFoods()
        {
            ViewBag.MenuFoods = help.GetLastMenuFoods(9);
            return View();
        }
        public ActionResult PartialShineFood()
        {
            ViewBag.ShideProduct = help.GetProduct(Convert.ToInt64(Helper.Settings.GetValue("ShineProductID")));
            return View();
        }
        public ActionResult PartialStaff()
        {
            ViewBag.Staffs = help.GetLastStaffs(3);
            return View();
        }
        public ActionResult PartialPhotoGalleries()
        {
            ViewBag.PhotoGalleries = help.GetLastPhotoGalleries(6);
            return View();
        }
        public ActionResult PartialReservation()
        {
            return View();
        }
        public ActionResult PartialAnnouncement()
        {
            List<Announcements> anno = help.GetLastAnnouncements(1);
            if (anno.Count>0) ViewBag.LastAnnouncement = anno[0]; 
            return View();
        }
        public ActionResult PartialBlogs()
        {
            ViewBag.Blogs = help.GetLastBlogs(3);
            return View();
        }
        public ActionResult PartialMap()
        {
            return View();
        }
        public ActionResult PartialFooter()
        {
            ViewBag.SocialMedias = help.GetLastSocialMedias(5);
            return View();
        }
    }
}