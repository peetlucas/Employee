using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeHierarchyAssessment.ErrorHandling
{
    class InvalidSalary : Exception
    {
        public InvalidSalary(string message) : base(message)
        {

        }

    }
}
