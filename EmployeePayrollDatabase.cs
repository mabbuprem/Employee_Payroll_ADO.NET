using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Employee_Payroll_ADO.NET
{
    internal class EmployeePayrollDatabase
    {
        public static void Create_Database()
        {
            try
            {
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog =payroll_service; Integrated Security = True;";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("Create database payroll_service;", con);
                cmd.ExecuteNonQuery();
                Console.WriteLine("payroll_service Database created successfully!");
                con.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
