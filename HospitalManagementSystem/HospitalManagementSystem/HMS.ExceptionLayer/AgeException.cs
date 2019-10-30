using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.ExceptionLayer
{
    public class AgeException:Exception
    {
        public override string Message
        {
            get
            {
                return "Age must be a positive integer!";
            }
        }
    }
}
