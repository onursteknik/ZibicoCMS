using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZibicoCMS.Entity;

namespace ZibicoCMS.WebAdminUI
{
    public class Helpers
    {
        public static Editors Info { get { return HttpContext.Current.Session["ActiveUser"] as Editors; } set { HttpContext.Current.Session["ActiveUser"] = value; } }
        public static List<Editors> DeletedEditors = new List<Editors>();
        
    }
}