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
            Console.WriteLine("Enter the choice in the ADO.NET Program");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    employeePayrollDatabase.GetAllEmployeeRecords();
                    break;
                case 2:
                    Employee employee = new Employee
                    {
                        employeeName = "RamSingh",
                        gender = "M",
                        phoneNo = 9991661664,
                        employeeAddress = "VPO Kotputli, Rajasthan",
                        startDate = DateTime.Now,
                        basicPay = 1200000,
                        deductions = 200000,
                        incomeTax = 100000,
                        companySelect = 1,
                        employeeSelect = 5,
                        departmentSelect = 1,
                    };
                    employeePayrollDatabase.AddEmployeeToDatabase(employee);
                    break;
                case 3:
                    Employee employee1 = new Employee
                    {
                        employeeName = "RamSingh",
                        employeeID = 5,
                        basicPay = 3000000
                    };
                    employeePayrollDatabase.UpdateSalaryofEmployee(employee1);
                    break;
                case 5:
                    string dateQuery = @"SELECT EmployeeID,EmployeeName,Gender,CompanyName,DepartmentName,PhoneNo,EmployeeAddress,StartDate,BasicPay,Deductions,TaxablePay,IncomeTax,NetPay
                                    FROM CompanyTable
                                    INNER JOIN EmployeeTable ON CompanyTable.CompanyID = EmployeeTable.CompanySelect
                                    INNER JOIN PayrollTable ON PayrollTable.EmployeeSelect = EmployeeTable.EmployeeID
                                    INNER JOIN EmployeeDepartmentTable ON EmployeeDepartmentTable.EmployeeSelect = EmployeeTable.EmployeeID
                                    INNER JOIN DepartmentTable ON DepartmentTable.DepartmentID = EmployeeDepartmentTable.DepartmentSelect WHERE StartDate between cast ('2019-05-01' as DATE) and GETDATE()";
                    employeePayrollDatabase.GetAllEmployeesWithDataAdapter(dateQuery);
                    break;
                case 6:
                    string avgQuery = @"SELECT AVG(BasicPay) AS AverageSalary, Gender FROM EmployeeTable
                    INNER JOIN PayrollTable ON PayrollTable.EmployeeSelect = EmployeeTable.EmployeeID GROUP BY GENDER";

                    string sumQuery = @"SELECT SUM(BasicPay) AS TotalSalary, Gender FROM EmployeeTable
                    INNER JOIN PayrollTable ON PayrollTable.EmployeeSelect = EmployeeTable.EmployeeID GROUP BY GENDER";

                    string maxquery = @"SELECT MAX(BasicPay) AS MaximumSalary, Gender FROM EmployeeTable
                    INNER JOIN PayrollTable ON PayrollTable.EmployeeSelect = EmployeeTable.EmployeeID GROUP BY GENDER";

                    string minquery = @"SELECT MIN(BasicPay) AS MinimumSalary, Gender FROM EmployeeTable
                    INNER JOIN PayrollTable ON PayrollTable.EmployeeSelect = EmployeeTable.EmployeeID GROUP BY GENDER";

                    string countquery = @"SELECT COUNT(BasicPay) AS CountSalary, Gender FROM EmployeeTable
                    INNER JOIN PayrollTable ON PayrollTable.EmployeeSelect = EmployeeTable.EmployeeID GROUP BY GENDER";
                    employeePayrollDatabase.GetAllEmployeesWithDataAdapter(avgQuery);
                    employeePayrollDatabase.GetAllEmployeesWithDataAdapter(sumQuery);
                    employeePayrollDatabase.GetAllEmployeesWithDataAdapter(maxquery);
                    employeePayrollDatabase.GetAllEmployeesWithDataAdapter(minquery);
                    employeePayrollDatabase.GetAllEmployeesWithDataAdapter(countquery);
                    break;
            }
        }

    }
}

