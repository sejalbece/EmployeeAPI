using Interview_API.Data;
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

        public async Task<(List<Employee> employees, int TotalRecords)> GetEmployeeAsync(int pageNumber, int pageSize)
        {
            var query = _db.Employees.AsNoTracking().OrderBy(e=>e.EmployeeId);
          
            var TotalRecords = await query.CountAsync();

            var employees = await query
                            .Skip((pageNumber-1)*pageSize)
                            .Take(pageSize)
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
