using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZibicoCMS.Entity
{
    public class Editors
    {
        [Key]
        public long ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ImageURL { get; set; }
        public string HomeAddress { get; set; }
        [AllowHtml]
        public string StaffDescription { get; set; }
        public Guid? SecretKey { get; set; }
        public bool Status { get; set; }
        
    }
}
