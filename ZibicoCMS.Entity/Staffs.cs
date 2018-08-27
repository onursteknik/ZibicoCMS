using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZibicoCMS.Entity
{
    public class Staffs
    {
        [Key]
        public long ID { get; set; }
        public string StaffName { get; set; }
        public string StaffRole { get; set; }
        public string StaffImageURL { get; set; }
        public int StaffOrderNumber { get; set; }
        public bool Status { get; set; }
    }
}
