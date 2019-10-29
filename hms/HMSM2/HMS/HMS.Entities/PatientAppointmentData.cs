using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class PatientAppointmentData
    {
        public PatientAppointmentData()
        {
            PatientData = new Patient();
            Bill = new BillData();
            LabData = new Lab();
        }
        public int AppointmentId { get; set; }
        public Patient PatientData { get; set; }
        public string DoctorName { get; set; }
        public string RoomNo { get; set; }
        public DateTime? DateOfVisit { get; set; } = null;
        public DateTime? AdmissionDate { get; set; } = null;
        public DateTime? DischargeDate { get; set; } = null;
        public string PatientType { get; set; }
        public BillData Bill { get; set; }
        public Lab LabData { get; set; }
    }
}
