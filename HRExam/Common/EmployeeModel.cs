using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string TIN { get; set; }

        public EmployeeTypeEnum EmployeeType { get; set; }

        public double Rate { get; set; }

        public double Tax { get; set; }
    }
}
