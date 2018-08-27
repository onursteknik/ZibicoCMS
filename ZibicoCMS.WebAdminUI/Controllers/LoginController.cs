using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZibicoCMS.CommonHelpers;
using ZibicoCMS.Entity;

namespace ZibicoCMS.WebAdminUI.Controllers
{
    public class LoginController : Controller
    {
        ZibicoCMS.CommonHelpers.Helper help = new ZibicoCMS.CommonHelpers.Helper();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Editors editor)
        {
            if (Helpers.Info != null) return Redirect("/");

            if (editor == null | editor.Nickname == null | editor.Password == null)
            {
                TempData["LoginError"] = "Şifre veya kullanıcı adınız hatalı!";
                return Redirect("/Login/Index");
            }
            else if (editor.Nickname.Length > 50 || editor.Password.Length > 50)
            {
                TempData["LoginError"] = "Şifre veya kullanıcı adınız hatalı!";
                return Redirect("/Login/Index");
            }
            
            Editors edt = help.LoginControl(editor);

            if (edt != null)
            {
                bool remember = Convert.ToBoolean(Request.Form["RememberMe"]);
                if (remember)
                {
                    HttpCookie user = new HttpCookie("User");
                    user.Expires = DateTime.Now.AddDays(14);
                    user.Values.Add("Nickname", edt.Nickname);
                    user.Values.Add("SKey", edt.SecretKey.ToString());
                    Response.Cookies.Add(user);

                }

                Helpers.Info = edt;
                return Redirect("/");
            }
            else
            {
                TempData["LoginError"] = "Şifre veya kullanıcı adınız hatalı!";
                return Redirect("/Login/Index");
            }

        }

        public ActionResult Logout()
        {
            if (Helpers.Info != null)
            {
                Session.Abandon();

                HttpCookie user = new HttpCookie("User");
                user.Expires = DateTime.Now.AddDays(-14);
                Response.Cookies.Add(user);
            }
            return Redirect("/Login");
        }
    }
}