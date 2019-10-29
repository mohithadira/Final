using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.EntityLayer
{
    public class Patient
    {
        public int patientID { get; set; }
        public string patientName { get; set; }
        public int patientAge { get; set; }
        public char patientGender { get; set; }        
        public string patientAddress { get; set; }
        public string patientContact { get; set; }
        public float patientWeight { get; set; }
    }
}
