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
                    //client.BaseAddress = new Uri("http://localhost:44310/api/");

                    //HTTP GET
                    var getEmployee = client.GetAsync("https://localhost:44310/api/employee/");
                    getEmployee.Wait();

                    var result = getEmployee.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var buffer = await result.Content.ReadAsStreamAsync();
                        var buffer1 = await result.Content.ReadAsStringAsync();
                        var buffer2 = await result.Content.ReadAsByteArrayAsync();
                        var readEmployee = JsonSerializer.Deserialize<EmployeeModel>(Regex.Unescape(buffer1));
                        return View("Index", readEmployee);
                    }
                }
                catch (AggregateException err)
                {
                }

            }

            return View();
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.EmployeeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
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
            if (ModelState.IsValid)
            {
                employeeModel.Id = Guid.NewGuid();
                _context.Add(employeeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.EmployeeModel.FindAsync(id);
            if (employeeModel == null)
            {
                return NotFound();
            }
            return View(employeeModel);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Birthday,TIN,EmployeeType,Rate,Tax")] EmployeeModel employeeModel)
        {
            if (id != employeeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeModelExists(employeeModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.EmployeeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employeeModel = await _context.EmployeeModel.FindAsync(id);
            _context.EmployeeModel.Remove(employeeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeModelExists(Guid id)
        {
            return _context.EmployeeModel.Any(e => e.Id == id);
        }
    }
}
