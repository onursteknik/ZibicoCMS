using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ZibicoCMS.CommonHelpers;
using ZibicoCMS.Entity;

namespace ZibicoCMS.WebAdminUI.Controllers
{
    [ZbcAuthorize]
    public class SettingsController : Controller
    {
        Helper help = new Helper();
        #region GeneralSettings
        public ActionResult GeneralSettings()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GeneralSettings(object variable)
        {
            string WebSiteTitle = Request.Unvalidated.Form["WebSiteTitle"];
            string HomepageDescription = Request.Unvalidated.Form["HomepageDescription"];
            string MetaNameDescription = Request.Unvalidated.Form["MetaNameDescription"];
            string MetaNameKeywords = Request.Unvalidated.Form["MetaNameKeywords"];

            bool result1 = false;
            bool result2 = false;

            HttpPostedFileBase FaviconURL = Request.Files["FaviconURL"];
            HttpPostedFileBase WebSiteLogoURL = Request.Files["WebSiteLogoURL"];

            if (string.IsNullOrWhiteSpace(WebSiteTitle) || WebSiteTitle.Length > 500)
            {
                ViewBag.CallbackMessageFail = "Title boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(HomepageDescription) || HomepageDescription.Length > 500)
            {
                ViewBag.CallbackMessageFail = "Açıklama boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(MetaNameDescription) || MetaNameDescription.Length > 500)
            {
                ViewBag.CallbackMessageFail = "Meta Description boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(MetaNameKeywords) || MetaNameKeywords.Length > 500)
            {
                ViewBag.CallbackMessageFail = "Meta Keywords boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }

            GeneralSettings gs1 = Helper.Settings.GetSettingByKey("FaviconURL");
            if (gs1 == null)
            {
                if (FaviconURL == null || FaviconURL.FileName == "")
                {
                    ViewBag.CallbackMessageFail = "Fotoğraf seçmediniz!";
                    return View();
                }
                result1 = Helper.SaveImg(FaviconURL, out string path);
                gs1.SettingValue = path;
                Helper.Update<GeneralSettings>(gs1);
            }
            else
            {
                if (FaviconURL != null && FaviconURL.FileName != "")
                {
                    result1 = Helper.SaveImg(FaviconURL, out string path);
                    gs1.SettingValue = path;
                    Helper.Update<GeneralSettings>(gs1);
                }
            }
            GeneralSettings gs2 = Helper.Settings.GetSettingByKey("WebSiteLogoURL");
            if (gs2 == null)
            {
                if (WebSiteLogoURL == null || WebSiteLogoURL.FileName == "")
                {
                    ViewBag.CallbackMessageFail = "Fotoğraf seçmediniz!";
                    return View();
                }
                result2 = Helper.SaveImg(WebSiteLogoURL, out string path);
                gs2.SettingValue = path;
                Helper.Update<GeneralSettings>(gs2);
            }
            else
            {
                if (WebSiteLogoURL != null && WebSiteLogoURL.FileName != "")
                {
                    result2 = Helper.SaveImg(WebSiteLogoURL, out string path);
                    gs2.SettingValue = path;
                    Helper.Update<GeneralSettings>(gs2);
                }
            }
            GeneralSettings gs3 = Helper.Settings.GetSettingByKey("WebSiteTitle");
            gs3.SettingValue = WebSiteTitle;
            Helper.Update<GeneralSettings>(gs3);

            GeneralSettings gs4 = Helper.Settings.GetSettingByKey("HomepageDescription");
            gs4.SettingValue = HomepageDescription;
            Helper.Update<GeneralSettings>(gs4);

            GeneralSettings gs5 = Helper.Settings.GetSettingByKey("MetaNameDescription");
            gs5.SettingValue = MetaNameDescription;
            Helper.Update<GeneralSettings>(gs5);

            GeneralSettings gs6 = Helper.Settings.GetSettingByKey("MetaNameKeywords");
            gs6.SettingValue = MetaNameKeywords;
            Helper.Update<GeneralSettings>(gs6);

            Helper.RefreshSettingsCache("WebAdminUI");

            ViewBag.CallbackMessage = "Ayarlar başarıyla kaydedildi.";
            return View();
        } 
        #endregion
        public ActionResult ClearSettingsCachePage()
        {
            Helper.RefreshSettingsCache("WebAdminUI");
            return Redirect("/");
        }
        #region Düzen/Tasarım
        public ActionResult AboutSettings()
        {
            return View();
        }
        public ActionResult MenuSettings()
        {
            return View();
        }
        public ActionResult CategorySettings()
        {
            return View();
        }
        public ActionResult ShineProductSettings()
        {
            ViewBag.Products = help.GetAllProducts();
            return View();
        }
        public ActionResult StaffSettings()
        {
            return View();
        }
        public ActionResult GallerySettings()
        {
            return View();
        }
        public ActionResult ReservationSettings()
        {
            return View();
        }
        public ActionResult BlogSettings()
        {
            return View();
        }
        public ActionResult MapSettings()
        {
            return View();
        }
        public ActionResult HourSettings()
        {
            return View();
        }
        public ActionResult BackgroundSettings()
        {
            return View();
        }
        #endregion

        #region Post Metotlar
        [HttpPost]
        public ActionResult AboutSettings(object variable)
        {
            string AboutTitle = Request.Unvalidated.Form["AboutTitle"];
            string AboutDescription = Request.Unvalidated.Form["AboutDescription"];
            HttpPostedFileBase AboutImage1URL = Request.Files["AboutImage1URL"];
            HttpPostedFileBase AboutImage2URL = Request.Files["AboutImage2URL"];

            if (string.IsNullOrWhiteSpace(AboutTitle) || AboutTitle.Length > 500)
            {
                ViewBag.CallbackMessage = "Başlık boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(AboutDescription) || AboutDescription.Length > 500)
            {
                ViewBag.CallbackMessage = "Açıklama boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }

            GeneralSettings gs1 = Helper.Settings.GetSettingByKey("AboutTitle");
            gs1.SettingValue = AboutTitle;
            Helper.Update<GeneralSettings>(gs1);

            GeneralSettings gs2 = Helper.Settings.GetSettingByKey("AboutDescription");
            gs2.SettingValue = AboutDescription;
            Helper.Update<GeneralSettings>(gs2);

            bool result1 =  false;
            bool result2 =  false;

            string path1 = "";
            if (AboutImage1URL != null && AboutImage1URL.FileName != "") result1 = Helper.SaveImg(AboutImage1URL, out path1);
            if (result1)
            {
                GeneralSettings gs3 = Helper.Settings.GetSettingByKey("AboutImage1URL");
                gs3.SettingValue = path1;
                Helper.Update<GeneralSettings>(gs3);
            }
            string path2 = "";
            if (AboutImage2URL != null && AboutImage2URL.FileName != "") result2 = Helper.SaveImg(AboutImage2URL, out path1);
            if (result2)
            {
                GeneralSettings gs4 = Helper.Settings.GetSettingByKey("AboutImage2URL");
                gs4.SettingValue = path2;
                Helper.Update<GeneralSettings>(gs4);
            }

            Helper.RefreshSettingsCache("WebAdminUI");
            ViewBag.CallbackMessage = "Ayarlar başarıyla kaydedildi.";
            return View();
        }
        [HttpPost]
        public ActionResult CategorySettings(object variable)
        {
            string CategoriesTitle = Request.Unvalidated.Form["CategoriesTitle"];
            string CategoryDescription = Request.Unvalidated.Form["CategoryDescription"];

            if (string.IsNullOrWhiteSpace(CategoriesTitle) || CategoriesTitle.Length > 500)
            {
                ViewBag.CallbackMessage = "Başlık boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(CategoryDescription) || CategoryDescription.Length > 500)
            {
                ViewBag.CallbackMessage = "Açıklama boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            
            GeneralSettings gs1 = Helper.Settings.GetSettingByKey("CategoriesTitle");
            gs1.SettingValue = CategoriesTitle;
            Helper.Update<GeneralSettings>(gs1);

            GeneralSettings gs2 = Helper.Settings.GetSettingByKey("CategoryDescription");
            gs2.SettingValue = CategoryDescription;
            Helper.Update<GeneralSettings>(gs2);
            
            Helper.RefreshSettingsCache("WebAdminUI");
            
            ViewBag.CallbackMessage = "Ayarlar başarıyla kaydedildi.";
            return View();
        }
        [HttpPost]
        public ActionResult MenuSettings(object variable)
        {
            string MenuTitle = Request.Unvalidated.Form["MenuTitle"];
            string MenuDescription = Request.Unvalidated.Form["MenuDescription"];
            HttpPostedFileBase MenuImageURL = Request.Files["MenuImageURL"];

            if (string.IsNullOrWhiteSpace(MenuTitle) || MenuTitle.Length > 500)
            {
                ViewBag.CallbackMessage = "Başlık boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(MenuDescription) || MenuDescription.Length > 500)
            {
                ViewBag.CallbackMessage = "Açıklama boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
          
            GeneralSettings gs1 = Helper.Settings.GetSettingByKey("MenuTitle");
            gs1.SettingValue = MenuTitle;
            Helper.Update<GeneralSettings>(gs1);

            GeneralSettings gs2 = Helper.Settings.GetSettingByKey("MenuDescription");
            gs2.SettingValue = MenuDescription;
            Helper.Update<GeneralSettings>(gs2);

            bool result = false;
            string path1 = "";
            if (MenuImageURL != null && MenuImageURL.FileName != "") result = Helper.SaveImg(MenuImageURL, out path1);
            if (result || !result)
            {
                GeneralSettings gs3 = Helper.Settings.GetSettingByKey("MenuImageURL");
                gs3.SettingValue = path1;
                Helper.Update<GeneralSettings>(gs3);
            }
            Helper.RefreshSettingsCache("WebAdminUI");
            ViewBag.CallbackMessage = "Ayarlar başarıyla kaydedildi.";
            return View();
        }
        [HttpPost]
        public ActionResult ShineProductSettings(object variable)
        {
            ViewBag.Products = help.GetAllProducts();

            string ShineProductID = Request.Unvalidated.Form["ShineProductID"];

            if (string.IsNullOrWhiteSpace(ShineProductID) || ShineProductID == "0")
            {
                ViewBag.CallbackMessage = "Ürün seçiniz!";
                return View();
            }

            GeneralSettings gs1 = Helper.Settings.GetSettingByKey("ShineProductID");
            gs1.SettingValue = ShineProductID;
            Helper.Update<GeneralSettings>(gs1);

            Helper.RefreshSettingsCache("WebAdminUI");
            ViewBag.CallbackMessage = "Ayarlar başarıyla kaydedildi.";
            return View();
        }
        [HttpPost]
        public ActionResult StaffSettings(object variable)
        {
            string StaffTitle = Request.Unvalidated.Form["StaffTitle"];
            string StaffDescription = Request.Unvalidated.Form["StaffDescription"];

            if (string.IsNullOrWhiteSpace(StaffTitle) || StaffTitle.Length > 500)
            {
                ViewBag.CallbackMessage = "Başlık boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(StaffDescription) || StaffDescription.Length > 500)
            {
                ViewBag.CallbackMessage = "Açıklama boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }

            GeneralSettings gs1 = Helper.Settings.GetSettingByKey("StaffTitle");
            gs1.SettingValue = StaffTitle;
            Helper.Update<GeneralSettings>(gs1);

            GeneralSettings gs2 = Helper.Settings.GetSettingByKey("StaffDescription");
            gs2.SettingValue = StaffDescription;
            Helper.Update<GeneralSettings>(gs2);

            Helper.RefreshSettingsCache("WebAdminUI");
            ViewBag.CallbackMessage = "Ayarlar başarıyla kaydedildi.";
            return View();
        }
        [HttpPost]
        public ActionResult GallerySettings(object variable)
        {
            string GalleryTitle = Request.Unvalidated.Form["GalleryTitle"];
            string GalleryDescription = Request.Unvalidated.Form["GalleryDescription"];

            if (string.IsNullOrWhiteSpace(GalleryTitle) || GalleryTitle.Length > 500)
            {
                ViewBag.CallbackMessage = "Başlık boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(GalleryDescription) || GalleryDescription.Length > 500)
            {
                ViewBag.CallbackMessage = "Açıklama boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }

            GeneralSettings gs1 = Helper.Settings.GetSettingByKey("GalleryTitle");
            gs1.SettingValue = GalleryTitle;
            Helper.Update<GeneralSettings>(gs1);

            GeneralSettings gs2 = Helper.Settings.GetSettingByKey("GalleryDescription");
            gs2.SettingValue = GalleryDescription;
            Helper.Update<GeneralSettings>(gs2);

            Helper.RefreshSettingsCache("WebAdminUI");
            ViewBag.CallbackMessage = "Ayarlar başarıyla kaydedildi.";
            return View();
        }
        [HttpPost]
        public ActionResult ReservationSettings(object variable)
        {
            string ReservationTitle = Request.Unvalidated.Form["ReservationTitle"];
            string ReservationDescription = Request.Unvalidated.Form["ReservationDescription"];

            if (string.IsNullOrWhiteSpace(ReservationTitle) || ReservationTitle.Length > 500)
            {
                ViewBag.CallbackMessage = "Başlık boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(ReservationDescription) || ReservationDescription.Length > 500)
            {
                ViewBag.CallbackMessage = "Açıklama boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }

            GeneralSettings gs1 = Helper.Settings.GetSettingByKey("ReservationTitle");
            gs1.SettingValue = ReservationTitle;
            Helper.Update<GeneralSettings>(gs1);

            GeneralSettings gs2 = Helper.Settings.GetSettingByKey("ReservationDescription");
            gs2.SettingValue = ReservationDescription;
            Helper.Update<GeneralSettings>(gs2);

            Helper.RefreshSettingsCache("WebAdminUI");
            ViewBag.CallbackMessage = "Ayarlar başarıyla kaydedildi.";
            return View();
        }
        [HttpPost]
        public ActionResult BlogSettings(object variable)
        {
            string BlogTitle = Request.Unvalidated.Form["BlogTitle"];
            string BlogDescription = Request.Unvalidated.Form["BlogDescription"];

            if (string.IsNullOrWhiteSpace(BlogTitle) || BlogTitle.Length > 500)
            {
                ViewBag.CallbackMessage = "Başlık boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(BlogDescription) || BlogDescription.Length > 500)
            {
                ViewBag.CallbackMessage = "Açıklama boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }

            GeneralSettings gs1 = Helper.Settings.GetSettingByKey("BlogTitle");
            gs1.SettingValue = BlogTitle;
            Helper.Update<GeneralSettings>(gs1);

            GeneralSettings gs2 = Helper.Settings.GetSettingByKey("BlogDescription");
            gs2.SettingValue = BlogDescription;
            Helper.Update<GeneralSettings>(gs2);

            Helper.RefreshSettingsCache("WebAdminUI");
            ViewBag.CallbackMessage = "Ayarlar başarıyla kaydedildi.";
            return View();
        }
        [HttpPost]
        public ActionResult MapSettings(object variable)
        {
            string MapTitle = Request.Unvalidated.Form["MapTitle"];
            string MapAddress = Request.Unvalidated.Form["MapAddress"];
            string MapLatitude = Request.Unvalidated.Form["MapLatitude"];
            string MapLongitude = Request.Unvalidated.Form["MapLongitude"];

            if (string.IsNullOrWhiteSpace(MapTitle) || MapTitle.Length > 500)
            {
                ViewBag.CallbackMessage = "Başlık boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(MapAddress) || MapAddress.Length > 500)
            {
                ViewBag.CallbackMessage = "Açıklama boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(MapLatitude) || MapLatitude.Length > 500)
            {
                ViewBag.CallbackMessage = "Enlem boş olamaz!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(MapLongitude) || MapLongitude.Length > 500)
            {
                ViewBag.CallbackMessage = "Boylam boş olamaz!";
                return View();
            }

            GeneralSettings gs1 = Helper.Settings.GetSettingByKey("MapTitle");
            gs1.SettingValue = MapTitle;
            Helper.Update<GeneralSettings>(gs1);

            GeneralSettings gs2 = Helper.Settings.GetSettingByKey("MapAddress");
            gs2.SettingValue = MapAddress;
            Helper.Update<GeneralSettings>(gs2);

            GeneralSettings gs3 = Helper.Settings.GetSettingByKey("MapLatitude");
            gs3.SettingValue = MapLatitude;
            Helper.Update<GeneralSettings>(gs3);

            GeneralSettings gs4 = Helper.Settings.GetSettingByKey("MapLongitude");
            gs4.SettingValue = MapLongitude;
            Helper.Update<GeneralSettings>(gs4);

            Helper.RefreshSettingsCache("WebAdminUI");
            ViewBag.CallbackMessage = "Ayarlar başarıyla kaydedildi.";
            return View();
        }
        [HttpPost]
        public ActionResult HourSettings(object variable)
        {
            string OpeningHoursTitle = Request.Unvalidated.Form["OpeningHoursTitle"];
            string OpeningHoursWeekdays = Request.Unvalidated.Form["OpeningHoursWeekdays"];
            string OpeningHoursWeekend = Request.Unvalidated.Form["OpeningHoursWeekend"];

            if (string.IsNullOrWhiteSpace(OpeningHoursTitle) || OpeningHoursTitle.Length > 500)
            {
                ViewBag.CallbackMessage = "Başlık boş veya 500 karakterden uzun olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(OpeningHoursWeekdays) || OpeningHoursWeekdays.Length > 500)
            {
                ViewBag.CallbackMessage = "Açık saatler boş olmamalıdır!";
                return View();
            }
            else if (string.IsNullOrWhiteSpace(OpeningHoursWeekend) || OpeningHoursWeekend.Length > 500)
            {
                ViewBag.CallbackMessage = "Açık saatler boş olmamalıdır!";
                return View();
            }

            GeneralSettings gs1 = Helper.Settings.GetSettingByKey("OpeningHoursTitle");
            gs1.SettingValue = OpeningHoursTitle;
            Helper.Update<GeneralSettings>(gs1);

            GeneralSettings gs2 = Helper.Settings.GetSettingByKey("OpeningHoursWeekdays");
            gs2.SettingValue = OpeningHoursWeekdays;
            Helper.Update<GeneralSettings>(gs2);

            GeneralSettings gs3 = Helper.Settings.GetSettingByKey("OpeningHoursWeekend");
            gs3.SettingValue = OpeningHoursWeekend;
            Helper.Update<GeneralSettings>(gs3);

            Helper.RefreshSettingsCache("WebAdminUI");
            ViewBag.CallbackMessage = "Ayarlar başarıyla kaydedildi.";
            return View();
        }
        [HttpPost]
        public ActionResult BackgroundSettings(object variable)
        {
            HttpPostedFileBase HomepageImageURL = Request.Files["HomepageImageURL"];
            HttpPostedFileBase MenuImageURL = Request.Files["MenuImageURL"];
            HttpPostedFileBase Announcement = Request.Files["Announcement"];

            bool rs1 = false; bool rs2 = false; bool rs3 = false;
            string path1 = ""; string path2 = ""; string path3 = "";

            if (HomepageImageURL != null && HomepageImageURL.FileName != "") rs1 = Helper.SaveImg(HomepageImageURL, out path1);
            if (rs1)
            {
                GeneralSettings gs1 = Helper.Settings.GetSettingByKey("HomepageImageURL");
                gs1.SettingValue = path1;
                Helper.Update<GeneralSettings>(gs1);
            }
            
            if (MenuImageURL != null && MenuImageURL.FileName != "") rs1 = Helper.SaveImg(MenuImageURL, out path2);
            if (rs2)
            {
                GeneralSettings gs2 = Helper.Settings.GetSettingByKey("MenuImageURL");
                gs2.SettingValue = path2;
                Helper.Update<GeneralSettings>(gs2);
            }
            
            if (Announcement != null && Announcement.FileName != "") rs1 = Helper.SaveImg(Announcement, out path3);
            if (rs3)
            {
                GeneralSettings gs3 = Helper.Settings.GetSettingByKey("Announcement");
                gs3.SettingValue = path3;
                Helper.Update<GeneralSettings>(gs3);
            }
            
            Helper.RefreshSettingsCache("WebAdminUI");
            ViewBag.CallbackMessage = "Ayarlar başarıyla kaydedildi.";
            return View();
        }
        #endregion
    }
}