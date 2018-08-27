using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZibicoCMS.Entity
{
    public class Products
    {
        [Key]
        public long ID { get; set; }
        public long CategoryID { get; set; }
        public string ProductName { get; set; }
        [AllowHtml]
        public string ProductDescription { get; set; }
        public string ProductImageURL { get; set; }
        public bool Status { get; set; }
    }
}
