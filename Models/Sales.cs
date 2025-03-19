using Microsoft.AspNetCore.Mvc;
using MO9Project.Models.Validation;
using System.ComponentModel.DataAnnotations;
using System;

namespace MO9Project.Models
{
    public class Sales // sales class
    {
        public int SalesId { get; set; } // salesid property

        [Required(ErrorMessage = "Please enter a quarter.")] // error message if left blank
        [Range(1, 4, ErrorMessage = "Quarter must be between 1 and 4.")] // range based on the quarter number
        public int? Quarter { get; set; } // quarter property

        [Required(ErrorMessage = "Please enter a year.")] // error message if left balnk
        [GreaterThan(2000, ErrorMessage = "Year may not be before 2000.")] // greater than class with range and error message
        public int? Year { get; set; } // year property

        [Required(ErrorMessage = "Please enter an amount.")] // error message if left blank
        [GreaterThan(0.0, ErrorMessage = "Amount must be greater than zero.")] // greater than class with range and error message
        public double? Amount { get; set; } // amount property

        [GreaterThan(0, ErrorMessage = "Please select an employee.")] // greater than class with range and error message
        [Remote("CheckSales", "Validation", AdditionalFields = "Quarter, Year")] // remote validation from controller and checksales
        [Display(Name = "Employee")] // display name employee
        public int EmployeeId { get; set; } // employeeid property
        public Employee? Employee { get; set; } = null!; // employee from the employee class property
    }
}
