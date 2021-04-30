using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Service;

namespace API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public List<EmployeeModel> Get()
        {
            return PretendDB.Employees.EmployeeCollection;
        }

        [HttpGet("Details")]
        public EmployeeModel Details(string id)
        {
            return PretendDB.Employees.EmployeeCollection.First(x => x.Id == id);
        }

        [HttpPost]
        public EmployeeModel Post(EmployeeModel employeeModel)
        {
            if (PretendDB.Employees == null)
            {
                PretendDB.Employees = new EmployeeList();
            }
            employeeModel.Id = Guid.NewGuid().ToString();
            PretendDB.Employees.EmployeeCollection.Add(employeeModel);
            return employeeModel;
        }

        [HttpPut]
        public EmployeeModel Put(EmployeeModel employeeModel)
        {
            var modelToUpdate = PretendDB.Employees.EmployeeCollection.FirstOrDefault(_ => _.Id == employeeModel.Id);

            if (modelToUpdate != null)
            {
                modelToUpdate.Name = employeeModel.Name;
                modelToUpdate.Birthday = employeeModel.Birthday;
                modelToUpdate.TIN = employeeModel.TIN;
                modelToUpdate.EmployeeType = employeeModel.EmployeeType;
                modelToUpdate.Rate = employeeModel.Rate;
                modelToUpdate.Tax = employeeModel.Tax;
            }

            return modelToUpdate;
        }

        [HttpDelete]
        public void Delete(EmployeeModel employeeModel)
        {

            var modelToDelete = PretendDB.Employees.EmployeeCollection.FirstOrDefault(_ => _.Id == employeeModel.Id);

            if (modelToDelete != null)
            {
                PretendDB.Employees.EmployeeCollection.Remove(modelToDelete);
            }
        }

        [HttpGet("ComputeSalary")]
        public double ComputeSalary(string id, double daysPresent, int workDaysInMonth)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {

                var modelToProcess = PretendDB.Employees.EmployeeCollection.FirstOrDefault(_ => _.Id == guid.ToString());

                if (modelToProcess != null)
                {
                    var service = Factory.GetService(modelToProcess.EmployeeType);
                    return service.ComputeSalary(modelToProcess, daysPresent, workDaysInMonth);
                }

            }
            return 0;
        }
    }
}
