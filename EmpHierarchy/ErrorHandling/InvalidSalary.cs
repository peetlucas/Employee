using System;
using System.Collections.Generic;
using System.Text;

namespace EmpHierarchy.ErrorHandling
{
    class InvalidSalary : Exception
    {
        public InvalidSalary(string message) : base(message)
        {

        }

    }
}
