using Microsoft.AspNetCore.Mvc;
using MO9Project.Models.Validation;
using MO9Project.Models;

namespace MO9Project.Controllers
{
    public class SalesController : Controller // sales controller
    {
        private SalesContext context { get; set; } // context property based on salescontext
        public SalesController(SalesContext ctx) => context = ctx; // get the context of the db from the salescontext

        public IActionResult Index() => RedirectToAction("Index", "Home"); // iaction result for the index page based on the homecontroller

        [HttpGet] // httpget
        public IActionResult Add() // add iactionresult
        {
            ViewBag.Employees = context.Employees.OrderBy(e => e.Firstname).ToList(); // viewbag the list of employees from the context
            return View(); // return that view
        }

        [HttpPost] // httppost
        public IActionResult Add(Sales sales) // iactionresult add the sales
        {
            // server side check for remote validation
            string msg = Validate.CheckSales(context, sales);
            if (!string.IsNullOrEmpty(msg)) // if it doesnt validate correctly
            {
                ModelState.AddModelError(nameof(sales.EmployeeId), msg); // display the error
            }

            if (ModelState.IsValid) // if modelstate is valid
            {
                context.Sales.Add(sales); // add the sales to the context
                context.SaveChanges(); // save the changes
                TempData["message"] = "Sales added"; // add the data with the message
                return RedirectToAction("Index", "Home"); // redirect to the index home page
            }
            else
            {
                ViewBag.Employees = context.Employees.OrderBy(e => e.Firstname).ToList(); // viewbag employees page to display
                return View(); // return that view
            }

        }
    }
}
