using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
//using Xunit;
using EmpHierarchy;
namespace UnitTestEmployeeHierarchy
{
    [TestClass]
    public class EmployeesHierarchyUnitTest
    {
        private EmployeesHierarchy empHierarchy;

        [TestInitialize]
        public void TestInitiliaze()
        {
            var data = GetData("../testcases/test1.csv");
            empHierarchy = new EmployeesHierarchy(data);
        }

        /// Test to check if the Employees are added to the graph

        [TestMethod()]
        public void AddTest()
        {

            Assert.IsTrue(empHierarchy.EmployeeList.Contains(new Employee
            { Id = "Employee2", ManagerId = "Employee1", Salary = 800 }));
            Assert.IsTrue(empHierarchy.EmployeeList.Contains(new Employee
            { Id = "Employee4", ManagerId = "Employee2", Salary = 500 }));
        }

       
        /// Test to check if Manager have employees added
        
        [TestMethod]
        public void SubOrdinate_Not_NULL()
        {
            var subordinates = empHierarchy.GetSubordinates("Employee2");
            Assert.AreEqual(2, subordinates.Count);
        }

        
        /// As per the test data employee 5 has no subordinates
       
        [TestMethod]
        public void Employee5_has_No_SubOrdinates_Test()
        {
            var subordinates = empHierarchy.GetSubordinates("Employee5");
            Assert.AreEqual(0, subordinates.Count);
        }

      
        /// Tests if the Lookup function returns a Employee given a valid Employee ID
      
        [TestMethod]
        public void LookUpTest()
        {
            Employee emp = empHierarchy.LookUp("Employee1");
            Assert.IsNotNull(emp);
        }

      
        /// Tests if lookup returns null on non existence id
      
        [TestMethod]
        public void Lookup_Wrong_emp_id_Test()
        {
            Employee emp = empHierarchy.LookUp("Employee10");
            Assert.IsNull(emp);
        }

        string[] GetData(String path)
        {

            return File.ReadAllLines(path);
        }

       
        /// Tests if the correct budget is added  
       
        [TestMethod]
        public void GetBudgetTest()
        {
            Assert.AreEqual(1800, empHierarchy.getSalaryBudget("Employee2"));
            Assert.AreEqual(500, empHierarchy.getSalaryBudget("Employee3"));
            Assert.AreEqual(3800, empHierarchy.getSalaryBudget("Employee1"));
        }

        
        /// Using test2.csv which contains employee with non number salary and negative salary
        /// Invalid Salary Employees are not added and the Graph is empty fails to pass this check
       
        [TestMethod]
        public void Test_Invalid_Salary_Not_Added()
        {
            EmployeesHierarchy h2 = new EmployeesHierarchy(GetData("../testcases/test2.csv"));
            Assert.IsFalse(h2.EmployeeList.Contains(new Employee
            { Id = "Employee5" }));
            Assert.IsFalse(h2.EmployeeList.Contains(new Employee
            { Id = "Employee2" }));

            Assert.AreEqual(0, h2.EmployeeList.Count);

        }
       
        /// Test3.csv contains two manager. The Graph should be Empty since manager should be one
       
        [TestMethod]
        public void Test_Manager_More_Than_Three()
        {
            EmployeesHierarchy h = new EmployeesHierarchy(GetData("../testcases/test3.csv"));
            Assert.IsFalse(h.EmployeeList.Contains(new Employee
            { Id = "Employee5" }));
            Assert.IsFalse(h.EmployeeList.Contains(new Employee
            { Id = "Employee1" }));
            Assert.AreEqual(0, h.EmployeeList.Count);

        }
       
        /// Test4.csv contains one employee with negative salary. 
        
        [TestMethod]
        public void Test_Negative_Salary_Check()
        {
            EmployeesHierarchy h = new EmployeesHierarchy(GetData("../testcases/test4.csv"));
            Assert.IsFalse(h.EmployeeList.Contains(new Employee
            { Id = "Employee5" }));
            Assert.AreEqual(0, h.EmployeeList.Count);
        }

        
        /// There is no manager that is not an employee, i.e. all managers are also listed in the employee column.
        /// test5.csv contain no manager record
        
        [TestMethod]
        public void No_Manager_Record()
        {
            EmployeesHierarchy h = new EmployeesHierarchy(GetData("../testcases/test5.csv"));
            Assert.AreEqual(0, h.EmployeeList.Count);
        }

    }
}
