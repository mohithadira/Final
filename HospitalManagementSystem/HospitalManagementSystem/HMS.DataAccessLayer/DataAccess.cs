using HospitalManagementSystem.HMS.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.DataAccessLayer
{
    class DataAccess
    {
        public List<Billing> bills { get; set; }
        public List<Patient> patients { get; set; }
        public List<LabReport> labReports { get; set; }
        public List<Treatment> treatments { get; set; }

        public DataAccess()
        {
            this.bills = new List<Billing>();
            this.patients = new List<Patient>();
            this.labReports = new List<LabReport>();
            this.treatments = new List<Treatment>();

        }
    }
}
