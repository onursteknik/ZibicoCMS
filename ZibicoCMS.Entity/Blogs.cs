using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZibicoCMS.Entity
{
    public class Blogs
    {
        [Key]
        public long ID { get; set; }
        public string BlogTitle { get; set; }
        [AllowHtml]
        public string BlogContent { get; set; }
        public string BlogImageURL { get; set; }
        public string BlogSeoURL { get; set; }
        public bool Status { get; set; }

    }
}
