using Interview_API.Enums;

namespace Interview_API.DTOs
{
    public class ResponseEmployee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public EmployeeDepartment Department { get; set; }
        public double Salary { get; set; }

    }
}
