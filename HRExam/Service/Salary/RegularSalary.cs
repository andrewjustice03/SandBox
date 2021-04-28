using Common;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Salary
{
    public class RegularSalary : ISalary
    {
        public double ComputeSalary(EmployeeModel employee, double daysPresent, int workDaysInMonth)
        {
            var deduction = (employee.Rate / workDaysInMonth) * (workDaysInMonth - daysPresent);

            return employee.Rate - deduction - (employee.Rate * employee.Tax);
        }
    }
}
