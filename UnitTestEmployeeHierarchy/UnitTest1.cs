using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
//using Xunit;
using EmployeeHierarchyAssessment;
namespace UnitTestEmployeeHierarchy
{
    [TestClass]
    public class UnitTest1
    {
        private Company company;

        [TestInitialize]
        public void TestInitiliaze()
        {
            var data = GetData("../testcases/test1.csv");
            company = new Company(data);
        }

        /// Test to check if the Employees are added to the graph

        [TestMethod()]
        public void AddTest()
        {

            Assert.IsTrue(company.EmployeeList.Contains(new Employees
            { Id = "Employee2", ManagerId = "Employee1", Salary = 800 }));
            Assert.IsTrue(company.EmployeeList.Contains(new Employees
            { Id = "Employee4", ManagerId = "Employee2", Salary = 500 }));
        }

       
        /// Test to check if Manager have employees added
        
        [TestMethod]
        public void SubOrdinate_Not_NULL()
        {
            var subordinates = company.GetSubordinates("Employee2");
            Assert.AreEqual(2, subordinates.Count);
        }

        
        /// As per the test data employee 5 has no subordinates
       
        [TestMethod]
        public void Employee5_has_No_SubOrdinates_Test()
        {
            var subordinates = company.GetSubordinates("Employee5");
            Assert.AreEqual(0, subordinates.Count);
        }

      
        /// Tests if the Lookup function returns a Employee given a valid Employee ID
      
        [TestMethod]
        public void LookUpTest()
        {
            Employees emp = company.LookUp("Employee1");
            Assert.IsNotNull(emp);
        }

      
        /// Tests if lookup returns null on non existence id
      
        [TestMethod]
        public void Lookup_Wrong_emp_id_Test()
        {
            Employees emp = company.LookUp("Employee10");
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
            Assert.AreEqual(1800, company.getSalaryBudget("Employee2"));
            Assert.AreEqual(500, company.getSalaryBudget("Employee3"));
            Assert.AreEqual(3800, company.getSalaryBudget("Employee1"));
        }

        
        /// Using test2.csv which contains employee with non number salary and negative salary
        /// Invalid Salary Employees are not added and the Graph is empty fails to pass this check
       
        [TestMethod]
        public void Test_Invalid_Salary_Not_Added()
        {
            Company h2 = new Company(GetData("../testcases/test2.csv"));
            Assert.IsFalse(h2.EmployeeList.Contains(new Employees
            { Id = "Employee5" }));
            Assert.IsFalse(h2.EmployeeList.Contains(new Employees
            { Id = "Employee2" }));

            Assert.AreEqual(0, h2.EmployeeList.Count);

        }
       
        /// Test3.csv contains two manager. The Graph should be Empty since manager should be one
       
        [TestMethod]
        public void Test_Manager_More_Than_Three()
        {
            Company h = new Company(GetData("../testcases/test3.csv"));
            Assert.IsFalse(h.EmployeeList.Contains(new Employees
            { Id = "Employee5" }));
            Assert.IsFalse(h.EmployeeList.Contains(new Employees
            { Id = "Employee1" }));
            Assert.AreEqual(0, h.EmployeeList.Count);

        }
       
        /// Test4.csv contains one employee with negative salary. 
        
        [TestMethod]
        public void Test_Negative_Salary_Check()
        {
            Company h = new Company(GetData("../testcases/test4.csv"));
            Assert.IsFalse(h.EmployeeList.Contains(new Employees
            { Id = "Employee5" }));
            Assert.AreEqual(0, h.EmployeeList.Count);
        }

        
        /// There is no manager that is not an employee, i.e. all managers are also listed in the employee column.
        /// test5.csv contain no manager record
        
        [TestMethod]
        public void No_Manager_Record()
        {
            Company h = new Company(GetData("../testcases/test5.csv"));
            Assert.AreEqual(0, h.EmployeeList.Count);
        }

    }
}
