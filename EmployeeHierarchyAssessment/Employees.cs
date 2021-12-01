using System;
using System.Collections.Generic;

namespace EmployeeHierarchyAssessment
{
    public class Employees
    {
        private string emp_id, manager_id = "";
        private long emp_salary = 0;


        /// gets and sets the id of employees' manager
       
        public string ManagerId
        {
            get => manager_id;
            set => manager_id = value;
        }
      
        /// gets and sets the salary of the employee
      

        public long Salary
        {
            get => emp_salary;
            set => emp_salary = value;
        }
       
        /// gets and sets the id of the employee
       
        public string Id
        {
            get => emp_id;
            set => emp_id = value;
        }

        public override bool Equals(object obj)
        {
            Employees emp1 = (Employees)obj;
            return (emp1.Id.ToUpper().Equals(Id.ToUpper()));
        }
    }
}
