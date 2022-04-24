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
            EmployeePayrollDatabase.Create_Database();
        }
    }
}
