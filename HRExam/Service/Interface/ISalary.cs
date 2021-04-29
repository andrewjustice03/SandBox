using Common;
using System;

namespace Service.Interface
{
    public interface ISalary
    {
        public double ComputeSalary(EmployeeModel employee, double daysPresent, int workDaysInMonth);
    }
}
