using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZibicoCMS.Entity
{
    public class SocialMedia
    {
        [Key]
        public long ID { get; set; }
        public string PlatformName { get; set; }
        public string PlatformIcon { get; set; }
        public string AccountName { get; set; }
        public string AccountURL { get; set; }
        public bool Status { get; set; }
    }
}
