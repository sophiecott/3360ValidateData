using Microsoft.AspNetCore.Mvc;
using MO9Project.Models.Validation;
using MO9Project.Models;

namespace MO9Project.Controllers
{
    public class EmployeeController : Controller // sales controller
    {
        private SalesContext context { get; set; } // new property of the context of the db
        public EmployeeController(SalesContext ctx) => context = ctx; // get context from the salescontext db
        
        public IActionResult Index() => RedirectToAction("Index", "Home"); // iactionresult index redirect to the homecontroller index

        [HttpGet] // httpget
        public IActionResult Add() // iaction for the add action
        {
            ViewBag.Employees = context.Employees.OrderBy(e => e.Firstname).ToList(); // viewbag employees get the context of the empoyees
            return View(); // return view
        }

        [HttpPost] // httppost
        public IActionResult Add(Employee employee) // action result for the add action
        {
            // server side checks for remote validation
            string msg = Validate.CheckEmployee(context, employee);
            if (!string.IsNullOrEmpty(msg)) // if the input if null or empty
            {
                ModelState.AddModelError(nameof(Employee.DOB), msg); // model state with the add error
            }
            msg = Validate.CheckManagerEmployeeMatch(context, employee); // message comes from the validate class based on context from the db
            if (!string.IsNullOrEmpty(msg)) // if it's not null or empty
            {
                ModelState.AddModelError(nameof(Employee.ManagerId), msg); // add model error based on the managerid
            }

            if (ModelState.IsValid) // if model state is valid
            {
                context.Employees.Add(employee); // add the employee to the db context
                context.SaveChanges(); // save the changes
                TempData["message"] = $"Employee {employee.Fullname} added"; // add the message with the data added
                return RedirectToAction("Index", "Home"); // redirect back home
            }
            else
            {
                ViewBag.Employees = context.Employees.OrderBy(e => e.Firstname).ToList(); // viewbag of the employees
                return View(); // return that view
            }
        }
    }
}
