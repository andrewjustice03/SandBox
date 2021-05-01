using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common
{
    public class EmployeeList
    {
        public EmployeeList()
        {
            EmployeeCollection = new List<EmployeeModel>();
        }
        public List<EmployeeModel> EmployeeCollection { get; set; }
    }
    public class EmployeeModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public string TIN { get; set; }

        [Display(Name = "Employee Type")]
        public EmployeeTypeEnum EmployeeType { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double Rate { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}")]
        [Display(Name = "Tax (%)")]
        public double Tax { get; set; }
    }
}
