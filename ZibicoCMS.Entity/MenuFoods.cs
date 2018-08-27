using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZibicoCMS.Entity
{
    public class MenuFoods
    {
        [Key]
        public long ID { get; set; }
        public string FoodTitle { get; set; }
        [AllowHtml]
        public string FoodDescription { get; set; }
        public decimal FoodPrice { get; set; }
        public string FoodImageURL { get; set; }
        public bool Status { get; set; }
    }
}
