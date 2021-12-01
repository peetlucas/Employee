using System;
using System.Collections.Generic;
using System.Text;
using EmployeeHierarchyAssessment.ErrorHandling;
namespace EmployeeHierarchyAssessment
{
   public class Company
    {
        readonly Dictionary<string, List<string>> OrdinaryEmp_list = new Dictionary<string, List<string>>();
        private List<Employees> employee_list = new List<Employees>();

        public List<Employees> EmployeeList => employee_list;

        
        /// load data from csv file
        public Company(String[] data)
        {
            ProcessData(data);

            foreach (var emp in employee_list)
            {
                Add(emp.ManagerId, emp.Id);
            }
        }

        ///check if the data id fetched fine
        
        public void ProcessData(string[] data)
        {
            //check the no. of managers
            int totalmanagers = 0;


            foreach (var li in data)
            {
                try
                {
                    var parts = li.Split(',');
                    var temp = new Employees();
                    temp.Id = parts[0];
                    if (parts[1].Equals(""))
                    {
                        temp.ManagerId = "";
                        totalmanagers++;
                        if (totalmanagers > 1)
                        {
                            throw new MultipleManagers("Erro!, Manager cannot be more than one...");
                        }
                    }
                    else
                    {
                        temp.ManagerId = parts[1];
                    }


                    long salary;
                    var isvalid = Int64.TryParse(parts[2], out salary);
                    
                    if (isvalid)
                    {
                     
                        if (salary > 0)
                        {
                            temp.Salary = salary;
                        }
                        else
                        {
                            throw new InvalidSalary("Salary cannot be Negative!");
                        }

                    }
                    else
                    {
                        throw new InvalidSalary("Salary is not valid");
                    }

                    EmployeeList.Add(temp);
                }
                catch (MultipleManagers ex)
                {
                   
                    EmployeeList.Clear();
                    Console.WriteLine(ex.Message);
                    return;
                }
                catch (InvalidSalary ex)
                {
                    employee_list.Clear();
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            //check if the manager belongs to the employee or exists
            if (totalmanagers != 1)
            {
                Console.WriteLine("Manager Unavailable!,try again...");
                EmployeeList.Clear();
            }
        }
        /// gets the list of all employee under manager
        public List<String> GetSubordinates(String empId)
        {
            return OrdinaryEmp_list[empId];
        }
       
        ///  calculate all the salary of junior staff below a manager.
        /// This method uses Depth Transversal as the algorithm to find all the junior staffs and their salary
        
        public long getSalaryBudget(String root)
        {
            long salary = 0;
            HashSet<String> visited = new HashSet<String>();
            Stack<String> stack = new Stack<String>();
            stack.Push(root);
            while (stack.Count != 0)
            {
                String empId = stack.Pop();
                if (!visited.Contains(empId))
                {
                    visited.Add(empId);
                    foreach (String v in GetSubordinates(empId))
                    {
                        stack.Push(v);
                    }
                }
            }

            if (visited.Count == 0) return salary;
            foreach (var id in visited)
            {
                salary += LookUp(id).Salary;
            }

            return salary;
        }
        
        /// Adds an employee id into the Graph
        
        public void Add(string employeeId)
        {
           
            if (OrdinaryEmp_list.ContainsKey(employeeId))
            {
                return;
            }

            OrdinaryEmp_list.Add(employeeId, new List<string>());
        }
        
        /// Adds a Junior employee to a list of all junior staff reporting to the manager
       
        public void Add(string manager, string employeeId)
        {
            Add(manager);
            Add(employeeId);
            OrdinaryEmp_list[manager].Add(employeeId);
        }
       
        /// Given an Id returns the employee details from the list
       
        public Employees LookUp(string id)
        {
            foreach (Employees emp in EmployeeList)
            {
                if (emp.Id.Equals(id))
                {
                    return emp;
                }
            }

            return null;
        }


    }
}
