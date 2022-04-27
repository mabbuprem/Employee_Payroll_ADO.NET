using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

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
        public void AddEmployeeToDatabase(Employee employee)
        {
            try
            {
                SqlCommand command = new SqlCommand("spAddEmployee", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@EmployeeName", employee.employeeName);
                command.Parameters.AddWithValue("@Gender", employee.gender);
                command.Parameters.AddWithValue("@PhoneNumber", employee.phoneNo);
                command.Parameters.AddWithValue("@EmployeeAddress", employee.employeeAddress);
                command.Parameters.AddWithValue("@StartDate", employee.startDate);
                command.Parameters.AddWithValue("@BasicPay", employee.basicPay);
                command.Parameters.AddWithValue("@Deductions", employee.deductions);
                command.Parameters.AddWithValue("@IncomeTax", employee.incomeTax);
                command.Parameters.AddWithValue("@CompanySelect", employee.companySelect);
                command.Parameters.AddWithValue("@DepartmentSelect", employee.departmentSelect);
                command.Parameters.AddWithValue("@EmployeeSelect", employee.employeeSelect);
                conn.Open();
                var result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Employee Details added in Database");
                }
                else
                {
                    Console.WriteLine("Employee Details not added in Database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public void UpdateSalaryofEmployee(Employee employee)
        {
            try
            {
                SqlCommand command = new SqlCommand("spUpdateEmployeeSalary", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@EmployeeID", employee.employeeID);
                command.Parameters.AddWithValue("@BasicPay", employee.basicPay);
                command.Parameters.AddWithValue("@EmployeeName", employee.employeeName);
                conn.Open();
                var result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Record updated successfully");
                }
                else
                {
                    Console.WriteLine("Record not updated successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { conn.Close(); }
        }
        public void GetAllEmployeesWithDataAdapter(string query)
        {
            try
            {
                DataSet dataSet = new DataSet();
                SqlConnection connection;
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataSet);
                    if (query.Contains("AverageSalary"))
                    {
                        Console.WriteLine("Average Salary Grouped by Gender");
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            Console.WriteLine(dataRow["AverageSalary"] + ", " + dataRow["Gender"]);
                        }
                    }
                    else if (query.Contains("TotalSalary"))
                    {
                        Console.WriteLine("Total Salary Grouped by Gender");
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            Console.WriteLine(dataRow["TotalSalary"] + ", " + dataRow["Gender"]);
                        }
                    }
                    else if (query.Contains("MaximumSalary"))
                    {
                        Console.WriteLine("Maximum Salary Grouped by Gender");
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            Console.WriteLine(dataRow["MaximumSalary"] + ", " + dataRow["Gender"]);
                        }
                    }
                    else if (query.Contains("MinimumSalary"))
                    {
                        Console.WriteLine("Minimum Salary Grouped by Gender");
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            Console.WriteLine(dataRow["MinimumSalary"] + ", " + dataRow["Gender"]);
                        }
                    }
                    else if (query.Contains("CountSalary"))
                    {
                        Console.WriteLine("Counting Persons Grouped by Gender");
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            Console.WriteLine(dataRow["CountSalary"] + ", " + dataRow["Gender"]);
                        }
                    }
                    else
                    {
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            Console.WriteLine(dataRow["EmployeeID"] + ", " + dataRow["EmployeeName"] + ", " + ", " + dataRow["StartDate"] + ", " + dataRow["Gender"] + ", " + dataRow["PhoneNo"] + ", " + dataRow["EmployeeAddress"] + ", " + dataRow["DepartmentName"] + ", " + dataRow["BasicPay"] + ", " + dataRow["Deductions"] + ", " + dataRow["TaxablePay"] + ", " + dataRow["IncomeTax"] + ", " + dataRow["NetPay"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}