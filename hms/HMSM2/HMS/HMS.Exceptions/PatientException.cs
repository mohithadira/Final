using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Exceptions
{
    public class PatientException:ApplicationException
    {
        public PatientException():base()
        {

        }
        public PatientException(string message):base(message)
        {

        }
        public PatientException(string message, Exception innerException):base(message,innerException)
        {

        }
    }
}
