using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Common;
using HRSystem_Core.Data;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using HRSystem_Core.Models;

namespace HRSystem_Core.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HRSystem_CoreContext _context;

        public EmployeeController(HRSystem_CoreContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var getEmployee = client.GetAsync("https://localhost:44310/api/employee/");
                    getEmployee.Wait();

                    var result = getEmployee.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var buffer = await result.Content.ReadAsStreamAsync();
                        var buffer1 = await result.Content.ReadAsStringAsync();

                        var readEmployee = JsonConvert.DeserializeObject<List<EmployeeModel>>(buffer1);
                        return View("Index", readEmployee);
                    }
                }
                catch (Exception err)
                {
                    throw new Exception("Error on listing data!", err);
                }

            }

            return View();
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var getEmployee = client.GetAsync("https://localhost:44310/api/employee/details?id=" + id);
                    getEmployee.Wait();

                    var result = getEmployee.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var buffer1 = await result.Content.ReadAsStringAsync();
                        var readEmployee = JsonConvert.DeserializeObject<EmployeeModel>(buffer1);
                        var returnObj = new EmployeeViewModel();
                        returnObj.Employee = readEmployee;
                        return View(returnObj);
                    }
                }
                catch (Exception err)
                {

                    throw new Exception("Error on getting details!", err);
                }

            }

            return NotFound();
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Birthday,TIN,EmployeeType,Rate,Tax")] EmployeeModel employeeModel)
        {

            using (var client = new HttpClient())
            {
                try
                {
                    employeeModel.Tax = employeeModel.Tax / 100; // convert to real percentage
                    var payload = JsonConvert.SerializeObject(employeeModel);
                    var content = new StringContent(payload, Encoding.UTF8, "application/json");
                    var postEmployee = client.PostAsync("https://localhost:44310/api/employee", content);
                    postEmployee.Wait();

                    var result = postEmployee.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var buffer = await result.Content.ReadAsStreamAsync();
                        var buffer1 = await result.Content.ReadAsStringAsync();

                        var readEmployee = JsonConvert.DeserializeObject<EmployeeModel>(buffer1);
                        return RedirectToAction("Details", new { id = readEmployee.Id });
                    }
                }
                catch (Exception err)
                {
                    throw new Exception("Error during create!", err);
                }

            }

            return View(employeeModel);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var getEmployee = client.GetAsync("https://localhost:44310/api/employee/details?id=" + id);
                    getEmployee.Wait();

                    var result = getEmployee.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var buffer = await result.Content.ReadAsStreamAsync();
                        var buffer1 = await result.Content.ReadAsStringAsync();

                        var readEmployee = JsonConvert.DeserializeObject<EmployeeModel>(buffer1);
                        readEmployee.Tax = readEmployee.Tax * 100;
                        return View(readEmployee);
                    }
                }
                catch (Exception err)
                {
                    throw new Exception("Error while getting data!", err);
                }

            }

            return NotFound();
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Birthday,TIN,EmployeeType,Rate,Tax")] EmployeeModel employeeModel)
        {

            using (var client = new HttpClient())
            {
                try
                {
                    employeeModel.Tax = employeeModel.Tax / 100; // convert to real percentage
                    var payload = JsonConvert.SerializeObject(employeeModel);
                    var content = new StringContent(payload, Encoding.UTF8, "application/json");
                    var putEmployee = client.PutAsync("https://localhost:44310/api/employee", content);
                    putEmployee.Wait();

                    var result = putEmployee.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var buffer = await result.Content.ReadAsStreamAsync();
                        var buffer1 = await result.Content.ReadAsStringAsync();

                        var readEmployee = JsonConvert.DeserializeObject<EmployeeModel>(buffer1);
                        return RedirectToAction("Details", new { id = readEmployee.Id });
                    }
                }
                catch (Exception err)
                {
                    throw new Exception("Error during edit!", err);
                }

            }

            return View(employeeModel);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var getEmployee = client.GetAsync("https://localhost:44310/api/employee/details?id=" + id);
                    getEmployee.Wait();

                    var result = getEmployee.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var buffer = await result.Content.ReadAsStreamAsync();
                        var buffer1 = await result.Content.ReadAsStringAsync();

                        var readEmployee = JsonConvert.DeserializeObject<EmployeeModel>(buffer1);
                        return View(readEmployee);
                    }
                }
                catch (Exception err)
                {
                    throw new Exception("Error on getting details!", err);
                }

            }

            return NotFound();
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {


            using (var client = new HttpClient())
            {
                try
                {
                    var deleteEmployee = client.DeleteAsync("https://localhost:44310/api/employee?id=" + id);
                    deleteEmployee.Wait();

                    var result = deleteEmployee.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var buffer = await result.Content.ReadAsStreamAsync();
                        var buffer1 = await result.Content.ReadAsStringAsync();

                        var readEmployee = JsonConvert.DeserializeObject<EmployeeModel>(buffer1);
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception err)
                {
                    throw new Exception("Error during delete!", err);
                }

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ComputeSalary(string id)
        {
            return View(new EmployeeViewModel() { EmployeeId = id });
        }


        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComputeSalary([Bind("EmployeeId,DaysPresent,WorkDaysInMonth")] EmployeeViewModel employeeViewModel)
        {
            var returnObject = new EmployeeViewModel();
            returnObject.EmployeeId = employeeViewModel.EmployeeId;
            returnObject.DaysPresent = employeeViewModel.DaysPresent;
            returnObject.WorkDaysInMonth = employeeViewModel.WorkDaysInMonth;

            using (var client = new HttpClient())
            {
                try
                {
                    //HTTP GET
                    var getEmployee = client.GetAsync("https://localhost:44310/api/employee/details?id=" + employeeViewModel.EmployeeId);
                    getEmployee.Wait();

                    var result = getEmployee.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var buffer = await result.Content.ReadAsStreamAsync();
                        var buffer1 = await result.Content.ReadAsStringAsync();
                        if (ViewBag.Salary == null)
                        {

                            ViewBag.Salary = new EmployeeViewModel();
                        }
                        var readEmployee = JsonConvert.DeserializeObject<EmployeeModel>(buffer1);
                        returnObject.Employee = readEmployee;
                    }
                }
                catch (Exception err)
                {
                    throw new Exception("Error while getting details!", err);
                }

            }

            using (var client = new HttpClient())
            {
                try
                {
                    var getSalary = client.GetAsync("https://localhost:44310/api/employee/ComputeSalary?id=" + employeeViewModel.EmployeeId + 
                                                                                                "&daysPresent=" + employeeViewModel.DaysPresent +
                                                                                                "&workDaysInMonth=" + employeeViewModel.WorkDaysInMonth);
                    getSalary.Wait();

                    var result = getSalary.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var buffer = await result.Content.ReadAsStreamAsync();
                        var buffer1 = await result.Content.ReadAsStringAsync();

                        var readSalary = JsonConvert.DeserializeObject<double>(buffer1);

                        returnObject.Salary = readSalary;
                        return View("Details", returnObject);
                    }
                }
                catch (Exception err)
                {
                    throw new Exception("Error during computation of salary!", err);
                }

            }

            return View("Details", employeeViewModel);
        }
    }
}
