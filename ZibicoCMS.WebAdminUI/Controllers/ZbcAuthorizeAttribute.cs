using System;
using System.Web;
using System.Web.Mvc;

namespace ZibicoCMS.WebAdminUI.Controllers
{
    public class ZbcAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string url = HttpContext.Current.Request.ServerVariables["URL"];
            if (Helpers.Info == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }
    }
}