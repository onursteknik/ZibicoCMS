using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZibicoCMS.CommonHelpers;

namespace ZibicoCMS.WebAdminUI.Controllers
{
    [ZbcAuthorize]
    public class HomeController : Controller
    {
        Helper help = new Helper();
        public ActionResult Index()
        {
            ViewBag.StaffCount = help.GetAllStaffs().Count;
            ViewBag.ProductsCount = help.GetAllMenuFoods().Count;
            ViewBag.Reservations = help.GetLastReservations(20);
            return View();
        }
    }
}