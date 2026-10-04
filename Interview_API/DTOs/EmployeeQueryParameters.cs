using Interview_API.Enums;

namespace Interview_API.DTOs
{
    public class EmployeeQueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public EmployeeDepartment? Department { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
    }
}
