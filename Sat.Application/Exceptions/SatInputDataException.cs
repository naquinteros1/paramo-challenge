using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Application.Exceptions
{
    public class SatInputDataException : ApplicationException
    {
        public SatInputDataException()
        {
        }

        public SatInputDataException(string message) : base(message)
        {
        }

        public SatInputDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SatInputDataException(Exception innerException) : base(innerException.Message, innerException)
        {
        }
    }
}
