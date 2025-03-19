using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MO9Project.Models;
using System.Diagnostics;

namespace MO9Project.Controllers
{
    public class HomeController : Controller // hime controller
    {
            private SalesContext context { get; set; } // sales context property
            public HomeController(SalesContext ctx) => context = ctx; // geth the context of the db from the salescontext

            [HttpGet] // httpget
            public ViewResult Index(int id) // index page view that result
            {
                // build sales query based on whether there's an employee id to filter by
                IQueryable<Sales> query = context.Sales
                    .Include(s => s.Employee)
                    .OrderBy(s => s.Year);
                if (id > 0)
                    query = query.Where(s => s.EmployeeId == id);

                var vm = new SalesListViewModel // call the salesviewmodel
                {
                    Sales = query.ToList(),  // execute sales query
                    Employees = context.Employees.OrderBy(e => e.Firstname).ToList(), // execute the employees query
                    EmployeeId = id // execute the employee query
                };
                return View(vm); // return that view
            }

            [HttpPost] // httppost
            public RedirectToActionResult Index(Employee employee) // redirect index action
            {
                if (employee.EmployeeId > 0)
                    return RedirectToAction("Index", new { id = employee.EmployeeId }); // redurect to the index page based on the employee
                else
                    // pass empty string for id segment to clear any previous values
                    return RedirectToAction("Index", new { id = "" });
            }

       
    }
}