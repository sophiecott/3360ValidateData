using System.Collections.Generic;

namespace MO9Project.Models
{
    public class SalesListViewModel
    {
        public List<Sales>? Sales { get; set; } // list of the Sales class property
        public List<Employee>? Employees { get; set; } // list of Employees from Employee class property
        public int EmployeeId { get; set; } // employeeid property
    }
}
