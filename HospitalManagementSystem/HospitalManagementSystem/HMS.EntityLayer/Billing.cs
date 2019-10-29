using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.EntityLayer
{
    class Billing
    {
        public int BillId { get; set; }
        public int treatmentId { get; set; }
        public int doctorFees { get; set; }
        public int roomCharges { get; set; }
        public int totalDays { get; set; }
        public int operationCharges { get; set; }
        public int medicineFees { get; set; }
        public int labFees { get; set; }
        public int totalAmount { get; set; }
    }
}
