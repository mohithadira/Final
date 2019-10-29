using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.EntityLayer
{
    class PatientAppointmentIN
    {
        public int appointmentID { get; set; }
        public int patientID { get; set; }
        public string doctor { get; set; }
        public string roomNo{ get; set; }
        public DateTime dateOfVisit { get; set; }
        public string remarks{ get; set; }
    }
}
