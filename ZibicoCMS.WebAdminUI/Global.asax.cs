using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ZibicoCMS.CommonHelpers;
using ZibicoCMS.Entity;

namespace ZibicoCMS.WebAdminUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        void Session_Start()
        {
            Helper help = new Helper();
            HttpCookie user = Request.Cookies["User"];
            if (user != null)
            {
                string username = user.Values["Nickname"];
                Guid gKey = Guid.Parse(user.Values["SKey"]);
                Editors editor = new Editors();
                editor.Nickname = username;
                editor.SecretKey = gKey;

                Editors edt = help.CookieControl(editor);

                if (edt != null)
                {
                    Helpers.Info = edt;
                }
                else
                {
                    user.Expires = DateTime.Now.AddDays(-10);
                    Response.Cookies.Add(user);
                    Session.Abandon();
                }

            }
        }

        void Application_BeginRequest()
        {
            HttpCookie user = Request.Cookies["User"];
            if (user != null)
            {
                bool result = Helpers.DeletedEditors.Any(x => x.Nickname == user.Values["Nickname"]);
                if (result)
                {
                    user.Expires = DateTime.Now.AddDays(-10);
                    Response.Cookies.Add(user);
                    Session.Abandon();
                }
            }

        }
    }
}
