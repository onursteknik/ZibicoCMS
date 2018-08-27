using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZibicoCMS.CommonHelpers;
using ZibicoCMS.Entity;

namespace ZibicoCMS.WebAdminUI.Controllers
{
    public class ReservationsController : Controller
    {
        Helper help = new Helper();
        public ActionResult Index()
        {
            return View(help.GetAllReservations());
        }
        
        public ActionResult Delete()
        {
            string ID = Request.QueryString["ID"];
            if (ID == null) return HttpNotFound();
            Reservations res = help.GetReservation(long.Parse(ID));
            if (res == null) return HttpNotFound();
            Helper.Delete(res);

            return Redirect("/Reservations/Index?Status=DeleteSuccess");
        }
    }
}