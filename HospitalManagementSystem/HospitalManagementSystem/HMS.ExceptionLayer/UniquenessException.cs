using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.ExceptionLayer
{
    public class UniquenessException:Exception
    {
        public override string Message
        {
            get
            {
                return "ID already exists!";
            }
        }
    }
}
