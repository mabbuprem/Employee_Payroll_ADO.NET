using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Payroll_ADO.NET
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll Database");
            EmployeePayrollDatabase employeePayrollDatabase = new EmployeePayrollDatabase();
            //employeePayrollDatabase.GetAllEmployeeRecords();
            Employee employee = new Employee
            {
                employeeName = "Ram",
                gender = "M",
                phoneNo = 7206594149,
                employeeAddress = "Salanuthal prakasam",
                startDate = DateTime.Now,
                basicPay = 2000000,
                deductions = 20000,
                incomeTax = 1980000,
                companySelect = 1,
                employeeSelect = 5,
                departmentSelect = 1,
            };
            //employeePayrollDatabase.AddEmployeeToDatabase(employee);
            Employee employee1 = new Employee
            {
                employeeName = "Ram",
                employeeID = 1,
                basicPay = 3000000
            };
            employeePayrollDatabase.UpdateSalaryofEmployee(employee1);
        }
    }
}

