using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZibicoCMS.CommonHelpers;
using ZibicoCMS.Entity;

namespace ZibicoCMS.WebUI.Controllers
{
    public class ReservationsController : Controller
    {
        [HttpPost]
        public ActionResult MakeReserv(Reservations res)
        {
            if (string.IsNullOrWhiteSpace(res.Name) || res.Name.Length > 500 || res.Name.Length < 3)
            {
                TempData["Result"] = "Üzgünüz, isim boş olamaz!";
                return Redirect("/#Rezervasyon");
            }
            if (string.IsNullOrWhiteSpace(res.PhoneNumber) || res.PhoneNumber.Length > 500 || res.PhoneNumber.Length < 3)
            {
                TempData["Result"] = "Üzgünüz, telefon boş olamaz!";
                return Redirect("/#Rezervasyon");
            }
            if (res.PersonCount == 0)
            {
                TempData["Result"] = "Üzgünüz, kişi sayısı 1'den az olamaz!";
                return Redirect("/#Rezervasyon");
            }
            res.Status = true;
            Helper.Insert(res);
            TempData["Result"] = "Teşekkürler! Rezervasyon talebinizi aldık. En kısa zamanda sizinle iletişime geçilecektir.";
            return Redirect("/#Rezervasyon");
        }
    }
}