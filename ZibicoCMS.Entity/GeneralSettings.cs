using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace ZibicoCMS.Entity
{
    
   public class GeneralSettings
    {
        [Key]
        public long ID { get; set; }
        [AllowHtml]
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
    }
}
