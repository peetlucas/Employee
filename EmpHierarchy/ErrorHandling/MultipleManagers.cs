using System;
using System.Collections.Generic;
using System.Text;

namespace EmpHierarchy.ErrorHandling
{
    class MultipleManagers : Exception
    {
       
            public MultipleManagers(string message) : base(message)
            {

            }
        
    }
}
