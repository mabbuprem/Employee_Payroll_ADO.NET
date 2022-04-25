using System;
using System.Collections.Generic;
using System.Data;
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
                phoneNo = 9991661664,
                employeeAddress = "Salanuthal prakasam",
                startDate = DateTime.Now,
                basicPay = 1200000,
                deductions = 200000,
                incomeTax = 100000,
                companySelect = 1,
                employeeSelect = 5,
                departmentSelect = 1,
            };
            //employeePayrollDatabase.AddEmployeeToDatabase(employee);
            Employee employee1 = new Employee
            {
                employeeName = "Ram",
                employeeID = 5,
                basicPay = 3000000
            };
            //employeePayrollDatabase.UpdateSalaryofEmployee(employee1);


            string dateQuery = @"SELECT EmployeeID,EmployeeName,Gender,CompanyName,DepartmentName,PhoneNo,EmployeeAddress,StartDate,BasicPay,Deductions,TaxablePay,IncomeTax,NetPay
                                    FROM CompanyTable
                                    INNER JOIN EmployeeTable ON CompanyTable.CompanyID = EmployeeTable.CompanySelect
                                    INNER JOIN PayrollTable ON PayrollTable.EmployeeSelect = EmployeeTable.EmployeeID
                                    INNER JOIN EmployeeDepartmentTable ON EmployeeDepartmentTable.EmployeeSelect = EmployeeTable.EmployeeID
                                    INNER JOIN DepartmentTable ON DepartmentTable.DepartmentID = EmployeeDepartmentTable.DepartmentSelect WHERE StartDate between cast ('2019-05-01' as DATE) and GETDATE()";
            employeePayrollDatabase.GetAllEmployeesWithDataAdapter(dateQuery);
        }

    }
}

