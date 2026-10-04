using Interview_API.Data;
using Interview_API.DTOs;
using Interview_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Interview_API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _db;

        public EmployeeRepository(AppDbContext db)
        {
            _db = db;            
        }
        public async Task AddEmployee(Employee emp)
        {
            await _db.Employees.AddAsync(emp);
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee != null)
            {
                _db.Employees.Remove(employee);
            }            
           
        }

        public async Task<(List<Employee> employees, int TotalRecords)> GetEmployeeAsync(EmployeeQueryParameters parameters)
        {
            IQueryable<Employee> query = _db.Employees.AsNoTracking().OrderBy(e=>e.EmployeeId);

            //filter

            if (parameters.Department.HasValue)
            {
                query = query.Where(e => e.Department == parameters.Department);
            }

            //serach


            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                query = query.Where(e=>
                                e.Name.Contains(parameters.Search) ||
                                e.Email.Contains(parameters.Search));
            }

            //sort

            query = parameters.SortBy?.ToLower() switch
            {
                "name" => parameters.SortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(e => e.Name)
                        : query.OrderBy(e => e.Name),
                "email" => parameters.SortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(e => e.Email)
                        : query.OrderBy(e => e.Email),
                "department" => parameters.SortOrder?.ToLower() == "desc"
                       ? query.OrderByDescending(e => e.Department)
                       : query.OrderBy(e => e.Department),
                "salary" => parameters.SortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(e => e.Salary)
                        : query.OrderBy(e => e.Salary),
                "createddate" => parameters.SortOrder?.ToLower() == "desc"
                       ? query.OrderByDescending(e => e.CreatedDate)
                       : query.OrderBy(e => e.CreatedDate),

                _ => query.OrderBy(e => e.EmployeeId)

            };

            //count
                    
          
            var TotalRecords = await query.CountAsync();

            var employees = await query
                            .Skip((parameters.PageNumber-1)*parameters.PageSize)
                            .Take(parameters.PageSize)
                            .ToListAsync();

            return (employees, TotalRecords);
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
          return await _db.Employees.FindAsync(id);
        }

        //public async Task<List<Employee>> GetEmployeeList() //1-10,2-skip10,take10
        //{
           
        //   return _db.Employees.ToList();
        //}

       
        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task UpdateEmployee(Employee emp)
        {
            _db.Employees.Update(emp);
        }

        //50 employee -- skip(50-pageSize).take(pagesize)
    }
}
