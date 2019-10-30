using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.ExceptionLayer
{
    public class DaysException: Exception
    {
        public override string Message
        {
            get
            {
                return "Number of days must be a non-negative integer";
            }
        }
    }
}
