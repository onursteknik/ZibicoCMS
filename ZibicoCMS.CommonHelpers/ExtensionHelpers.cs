using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZibicoCMS.Entity;

namespace ZibicoCMS.CommonHelpers
{
   public static class ExtensionHelpers
    {
        public static string GetValue(this List<GeneralSettings> list, string key)
        {
            return list.FirstOrDefault(x => x.SettingKey == key).SettingValue;
        }
        public static GeneralSettings GetSettingByKey(this List<GeneralSettings> list, string key)
        {
            return list.FirstOrDefault(x => x.SettingKey == key);
        }
    }
}
