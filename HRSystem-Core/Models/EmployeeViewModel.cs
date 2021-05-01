using Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRSystem_Core.Models
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            Employee = new EmployeeModel();
        }
        public EmployeeModel Employee { get; set; }

        public string EmployeeId { get; set; }

        [Display(Name = "Days Worked")]
        public double DaysPresent { get; set; }

        [Display(Name = "Workdays for this Month")]
        public int WorkDaysInMonth { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double Salary { get; set; }
    }
}
