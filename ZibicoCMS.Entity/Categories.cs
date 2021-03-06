﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZibicoCMS.Entity
{
    public class Categories
    {
        [Key]
        public long ID { get; set; }
        public string CategoryName { get; set; }
        [AllowHtml]
        public string CategoryDescription { get; set; }
        public string CategoryIcon { get; set; }
        public bool Status { get; set; }
    }
}
