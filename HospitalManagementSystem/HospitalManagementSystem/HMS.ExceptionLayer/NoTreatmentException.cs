using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.ExceptionLayer
{
    public class NoTreatmentException: Exception
    {
        public override string Message
        {
            get
            {
                return "No such appointment/treatment data exists!";
            }
        }
    }
}
