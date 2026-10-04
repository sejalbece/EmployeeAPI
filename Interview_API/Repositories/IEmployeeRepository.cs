using Interview_API.DTOs;
using Interview_API.Entities;

namespace Interview_API.Repositories
{
    public interface IEmployeeRepository
    {
        Task AddEmployee(Employee emp);
        Task DeleteEmployee(int id);
        //Task<List<Employee>> GetEmployeeList();
        Task<Employee?> GetEmployeeById(int id);
        Task UpdateEmployee(Employee emp);

        Task<int> SaveChangesAsync();

        Task<(List<Employee> employees, int TotalRecords)> GetEmployeeAsync(EmployeeQueryParameters parameters);
    }
}
