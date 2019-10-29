using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class Lab
    {
        public int ReportId { get; set; }
        public DateTime TestDate { get; set; }
        public string TestType { get; set; }
        public string Remarks { get; set; }
    }
}
