using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZibicoCMS.Entity
{
    public class Announcements
    {
        [Key]
        public long ID { get; set; }
        public string AnnouncementTitle { get; set; }
        [AllowHtml]
        public string AnnouncementContent { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }

    }
}
