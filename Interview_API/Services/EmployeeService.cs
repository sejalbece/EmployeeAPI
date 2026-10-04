using Interview_API.DTOs;
using Interview_API.Entities;
using Interview_API.Exceptions;
using Interview_API.Repositories;

namespace Interview_API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;            
        }
        public async Task<ResponseEmployee> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            if (request != null)
            {
                var emp = new Employee
                {
                    Name = request.Name,
                    Email = request.Email,
                    Department = request.Department,
                    Salary = request.Salary,

                };
                await _repo.AddEmployee(emp);
                await _repo.SaveChangesAsync();

                var result = MapToResponse(emp);
                return result;
            }
            return null;
            
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            if (id != null)
            { 
                await _repo.DeleteEmployee(id);
                await _repo.SaveChangesAsync();
                return true;
            }
            return false;
        }
        

        public async Task<PageResponse<ResponseEmployee>> GetEmployeeAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                pageNumber = 1;
            if(pageSize<=0)
                pageSize = 10;
            if(pageSize>100)
                pageSize = 100;
            


            var response = await _repo.GetEmployeeAsync(pageNumber, pageSize);  //employeeList, totalRecords
            var result = new PageResponse<ResponseEmployee> { 
                Data = response.employees
                    .Select(emp => new ResponseEmployee
                    {
                        EmployeeId = emp.EmployeeId,
                        Name = emp.Name,
                        Email = emp.Email,
                        Department = emp.Department,
                        Salary = emp.Salary
                    }).ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = response.TotalRecords,
                TotalPages = (int)Math.Ceiling((double)response.TotalRecords / pageSize)

        };
            
          return result;
            
        }

        public async Task<ResponseEmployee?> GetEmployeeByIdAsync(int id)
        {
            var response = await _repo.GetEmployeeById(id);
            if (response == null)
            {
                throw new NotFoundException($"Employee with ID {id}was not found");
            }               

            var ResponseEmp = new ResponseEmployee { 
                EmployeeId = response.EmployeeId,
                Name = response.Name,
                Email = response.Email,
                Department = response.Department,
                Salary = response.Salary,
            };
            return ResponseEmp;
        }

        public async Task<ResponseEmployee?> UpdateEmployeeAsync(UpdateEmployee request)
        {
            var employee = await _repo.GetEmployeeById(request.EmployeeId);

            if (employee != null)
            {
                employee.Name = request.Name;
                employee.Email = request.Email;
                employee.Department = request.Department;
                employee.Salary = request.Salary;

               
                await _repo.UpdateEmployee(employee);
                await _repo.SaveChangesAsync();
                var result = MapToResponse(employee);
                return result;
            }
            return null;
            
        }

        private static ResponseEmployee MapToResponse(Employee e)
        {
            var response = new ResponseEmployee {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                Email = e.Email,
                Department = e.Department,
                Salary = e.Salary
            };

            return response;
        }

      
    }
}
