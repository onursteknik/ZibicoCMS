using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZibicoCMS.CommonHelpers;
using ZibicoCMS.Entity;

namespace ZibicoCMS.WebAdminUI.Controllers
{
    public class SocialMediasController : Controller
    {
        Helper help = new Helper();
        public ActionResult Index()
        {
            return View(help.GetAllSocialMedias());
        }
        public ActionResult Create(long? ID)
        {
            if (ID == null) return Redirect("/");
            else
            {
                SocialMedia med = help.GetSocialMedia((long)ID);
                if (med == null) return Redirect("/");
                return View(med);
            }
        }

        [HttpPost]
        public ActionResult Create(SocialMedia media)
        {
            string MediaID = Request.QueryString["ID"];
            SocialMedia med = help.GetSocialMedia(long.Parse(MediaID));
            med.AccountName = media.AccountName;
            med.AccountURL = media.AccountURL;

            Helper.Update(med);

            ViewBag.Message = "Hesap bilgisi başarıyla güncellendi.";
            return View(med);
        }
    }
}