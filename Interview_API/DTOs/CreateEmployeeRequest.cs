using Interview_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Interview_API.DTOs
{
    public class CreateEmployeeRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public EmployeeDepartment Department { get; set; }
        
        [Required]
        public double Salary { get; set; }
        //public DateTime CreatedDate { get; set; }
    }
}
