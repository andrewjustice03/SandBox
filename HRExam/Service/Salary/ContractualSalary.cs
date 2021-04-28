using Common;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Salary
{
    public class ContractualSalary : ISalary
    {
        public double ComputeSalary(EmployeeModel employee, double daysPresent, int workDaysInMonth)
        {
            return employee.Rate * daysPresent;
        }
    }
}
