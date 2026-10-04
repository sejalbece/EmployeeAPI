using Interview_API.DTOs;

namespace Interview_API.Services
{
    public interface IEmployeeService
    {
        Task<ResponseEmployee> CreateEmployeeAsync(CreateEmployeeRequest request);
        Task<ResponseEmployee?> GetEmployeeByIdAsync(int id);
        Task<ResponseEmployee?> UpdateEmployeeAsync(UpdateEmployee request);
        Task<bool> DeleteEmployeeAsync(int id);

        //Task<List<ResponseEmployee>> GetAllEmployee();

        Task<PageResponse<ResponseEmployee>> GetEmployeeAsync(int pageNumber, int pageSize);
    }
}
