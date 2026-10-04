using Interview_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Interview_API.DTOs
{
    public class UpdateEmployee
    {
       public int EmployeeId { get; set; }
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public EmployeeDepartment Department { get; set; }

        
        public double Salary { get; set; }
    }
}
