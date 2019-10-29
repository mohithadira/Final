using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class BillData
    {        
        public int BillNo { get; set; }
        public int ? DoctorFees { get; set; }
        public int ? RoomCharge { get; set; }
        public int ? OperationCharge { get; set; }
        public int ? MedicineFees { get; set; }
        public int ? TotalDays { get; set; }
        public int ? LabFees { get; set; }
        public int ? TotalAmount { get; set; }  
    }
}
