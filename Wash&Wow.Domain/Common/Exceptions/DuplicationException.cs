using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wash_Wow.Domain.Common.Exceptions
{
    public class DuplicationException : Exception
    {
        public DuplicationException(string message) : base(message)
        {
        }
    }
}
