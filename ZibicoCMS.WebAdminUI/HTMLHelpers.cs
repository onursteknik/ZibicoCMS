using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZibicoCMS.CommonHelpers;

namespace ZibicoCMS.WebAdminUI
{
    public static class HTMLHelpers
    {
        public static MvcHtmlString GetSettingValue(this HtmlHelper hh, string key)
        {
            string html = Helper.Settings.FirstOrDefault(x => x.SettingKey == key).SettingValue;
            return new MvcHtmlString(html);
        }
    }
}