using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Employee_Payroll_ADO.NET
{
    internal class EmployeePayrollDatabase
    {

        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog =payroll_service; Integrated Security = True;";
        SqlConnection conn = new SqlConnection(connectionString);
        public void GetAllEmployeeRecords()
        {
            SqlDataReader reader = default;
            try
            {
                Employee employee = new Employee();
                using (this.conn)
                {
                    string query = @"SELECT EmployeeID,EmployeeName,Gender,CompanyName,DepartmentName,PhoneNo,EmployeeAddress,StartDate,BasicPay,Deductions,TaxablePay,IncomeTax,NetPay
                                    FROM CompanyTable
                                    INNER JOIN EmployeeTable ON CompanyTable.CompanyID = EmployeeTable.CompanySelect
                                    INNER JOIN PayrollTable ON PayrollTable.EmployeeSelect = EmployeeTable.EmployeeID
                                    INNER JOIN EmployeeDepartmentTable ON EmployeeDepartmentTable.EmployeeSelect = EmployeeTable.EmployeeID
                                    INNER JOIN DepartmentTable ON DepartmentTable.DepartmentID = EmployeeDepartmentTable.DepartmentSelect";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    this.conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employee.employeeID = reader.GetInt32(0);
                            employee.employeeName = reader.GetString(1);
                            employee.gender = reader.GetString(2);
                            employee.companyName = reader.GetString(3);
                            employee.departmentName = reader.GetString(4);
                            employee.phoneNo = reader.GetInt64(5);
                            employee.employeeAddress = reader.GetString(6);
                            employee.startDate = reader.GetDateTime(7);
                            employee.basicPay = reader.GetDouble(8);
                            employee.deductions = reader.GetDouble(9);
                            employee.taxablePay = reader.GetDouble(10);
                            employee.incomeTax = reader.GetDouble(11);
                            employee.netPay = reader.GetDouble(12);
                            Console.WriteLine($"{employee.employeeID},{employee.employeeName},{employee.gender},{employee.companyName},{employee.departmentName},{employee.phoneNo},{employee.employeeAddress},{employee.startDate},{employee.basicPay},{employee.deductions},{employee.taxablePay},{employee.incomeTax},{employee.netPay}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data found");
                    }
                    reader.Close();
                    this.conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                reader.Close();
                this.conn.Close();
            }
        }
    }
}