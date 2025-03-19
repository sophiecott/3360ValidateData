using Microsoft.AspNetCore.Mvc;
using MO9Project.Models.Validation;
using MO9Project.Models;

namespace MO9Project.Controllers
{
    public class ValidationController : Controller // validation controller
    {
        private SalesContext context { get; set; } // context property from te db
        public ValidationController(SalesContext ctx) => context = ctx; // get the context from the db

        public JsonResult CheckEmployee(DateTime dob, string firstname, string lastname) // check employee json result with parameters
        {
            var employee = new Employee // new value equal to the employee model
            {
                Firstname = firstname, // first name from the employee mode
                Lastname = lastname, // last name from the employee model
                DOB = dob // dob from the employee model
            };
            string msg = Validate.CheckEmployee(context, employee); // check the validation of check employee based on the context of the database and the employee inside
            if (string.IsNullOrEmpty(msg)) // if the db is empty of what the user inputted
                return Json(true); // return json as true
            else
                return Json(msg); // else return json with a message
        }

        public JsonResult CheckManager(int managerId, string firstname, string lastname, DateTime dob) // check manager json result
        {
            var employee = new Employee // new employee from the employee model
            {
                Firstname = firstname, // firstname from the employee model
                Lastname = lastname, // lastname from the employee model
                DOB = dob, // dob from the employee model
                ManagerId = managerId // manager from the employee model
            };
            string msg = Validate.CheckManagerEmployeeMatch(context, employee); // check manager and employee match with parameter ofthe context of the db
            if (string.IsNullOrEmpty(msg)) // if it's empty 
                return Json(true); // return it as true
            else
                return Json(msg); // else return the message
        }

        public JsonResult CheckSales(int quarter, int year, int employeeId) // checksales json result
        {
            var sales = new Sales // new sales from the sales model
            {
                Quarter = quarter, // quarter from the sales model
                Year = year, // year from the sales model
                EmployeeId = employeeId // employee from the sales model
            };
            string msg = Validate.CheckSales(context, sales); // validate to check sales based on the context of the db
            if (string.IsNullOrEmpty(msg)) // if it is null or empty
                return Json(true); // return as true
            else
                return Json(msg); // else return error message
        }

    }
}
