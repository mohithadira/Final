using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.EntityLayer
{
    public class Treatment
    {
        public int appointmentId { get; set; }
        public int patientId { get; set; }
        public string doctorName { get; set; }
        public string roomNo { get; set; }
        public string patientType { get; set; }
        public DateTime? dateOfVisit { get; set; }
        public DateTime? admissionDate { get; set; }
        public DateTime? dischargeDate { get; set; }
    }
}
