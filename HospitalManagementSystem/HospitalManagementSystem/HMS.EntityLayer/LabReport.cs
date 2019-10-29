using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.EntityLayer
{
    class LabReport
    {
        public int reportID { get; set; }
        public string patientName { get; set; }
        public DateTime testDate { get; set; }
        public string testType { get; set; }
        public string doctor { get; set; }
        public string remarks { get; set; }
    }
}
