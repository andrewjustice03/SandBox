using Common;
using Service.Interface;
using Service.Salary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public static class Factory
    {
        public static ISalary GetService(EmployeeTypeEnum employeeType)
        {
            switch (employeeType)
            {
                case EmployeeTypeEnum.Regular:
                    return new RegularSalary();
                case EmployeeTypeEnum.Contractual:
                    return new ContractualSalary();
            }

            return null;
        }
    }
}
