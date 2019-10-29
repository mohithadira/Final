using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.EntityLayer
{
    class PatientAppointmentOUT
    {
        public int appointmentID { get; set; }
        public int patientID { get; set; }
        public string doctor { get; set; }
        public string roomNo { get; set; }
        public DateTime dateOfAdmission { get; set; }
        public DateTime dateOfDischarge { get; set; }
        public string remarks { get; set; }
    }
}
