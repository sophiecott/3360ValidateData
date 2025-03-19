using Microsoft.AspNetCore.Mvc;
using MO9Project.Models.Validation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MO9Project.Models
{
    public class Employee // employee class
    {
        public int EmployeeId { get; set; } // employeeid property

        [Required(ErrorMessage = "Please enter a first name.")] // required error message if left blank
        [StringLength(100)] // string length
        [Display(Name = "First Name")] // display the first name as "First Name" label
        public string? Firstname { get; set; } // first name property

        [Required(ErrorMessage = "Please enter a last name.")] // please enter a last name error message
        [StringLength(100)] // string length
        [Display(Name = "Last Name")] // last name display for label
        public string? Lastname { get; set; } // last name property

        [Required(ErrorMessage = "Please enter a birth date.")] // birth date error message
        [PastDate(ErrorMessage = "Birth date must be a valid date that's in the past.")] // past date error message if date is in the past
        [Remote("CheckEmployee", "Validation", AdditionalFields = "Firstname, Lastname")] // call the check employee from the validation controller
        [Display(Name = "Birth Date")] // display name as birth date
        public DateTime? DOB { get; set; } // date of birth property

        [Required(ErrorMessage = "Please enter a hire date.")] // error message if left blank
        [PastDate(ErrorMessage = "Hire date must be a valid date that's in the past.")] // if past date display error
        [GreaterThan("1/1/1995", ErrorMessage = "Hire date can't be before company was formed in 1995.")] // greater than class with the hire date
        [Display(Name = "Hire Date")] // display name hire date
        public DateTime? DateOfHire { get; set; } // hire date property

        [GreaterThan(0, ErrorMessage = "Please select a manager.")] // greater than class display a message if not selected
        [Remote("CheckManager", "Validation", AdditionalFields = "Firstname, Lastname, DOB")] // check manager from the validation controller
        [Display(Name = "Manager")] // display name manager
        public int ManagerId { get; set; } // managerid property

        public string Fullname => $"{Firstname} {Lastname}"; // fullname property combine last and first
    }
}
