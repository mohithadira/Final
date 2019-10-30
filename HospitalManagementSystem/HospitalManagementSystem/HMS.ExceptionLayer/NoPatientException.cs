using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.ExceptionLayer
{
    public class NoPatientException:Exception
    {
        public override string Message
        {
            get
            {
                return "No such patient exists!";
            }
        }
    }
}
