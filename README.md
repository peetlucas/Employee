# EmployeeHierachyAssessment

Task Description Scenario: Develop a .NET library assembly (DLL) for a system that handles the employee hierarchy, implemented as class Employees. This class has a constructor that takes a CSV string that contains all the employees from the company, the manager they report to, and the employee’s salary. Develop the library assembly that contains the code for the questions below. Make sure to provide unit tests for your code and an efficient solution. a. Create a constructor that takes a CSV1

input string containing a list of employee info and validates the string. The first CSV column contains the id of the employee, the second one contains the id of the manager, and the third one contains the employee’s salary. The CEO of the company is the only employee that doesn't have a manager; in his case, the manager field will be empty. The list is not guaranteed to be sorted and can come in any random order. See the example below. The constructor should validate that:

    The salaries in the CSV are valid integer numbers.
    One employee does not report to more than one manager.
    There is only one CEO, i.e. only one employee with no manager.
    There is no circular reference, i.e. a first employee reporting to a second employee that is also under the first employee.
    There is no manager that is not an employee, i.e. all managers are also listed in the employee column.

b. Add an instance method that returns the salary budget from the specified manager. The salary budget from a manager is defined as the sum of the salaries of all the employees reporting (directly or indirectly) to a specified manager, plus the salary of the manager. See the example below. Input type: String Return type: lon


# Problem solving plan
On this task, the hierarchy of employees is displayed as a decision tree which assumes a directed acyclic graph. The graph stores manager’s id with all the employees under him, and to obtain the aggregate budget of salary, a depth-first search is done by traversing to get employee ids.
I did an emphasis to ensure that an employee only appears once by adding all employees to a dictionary data structure. My solution is thoroughly unit-tested and it passed all the test cases. 
