using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeHierarchyAssessment.ErrorHandling
{
    class MultipleManagers : Exception
    {
       
            public MultipleManagers(string message) : base(message)
            {

            }
        
    }
}
