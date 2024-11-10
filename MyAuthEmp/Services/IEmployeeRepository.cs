using MyAuthEmp.Models;

namespace MyAuthEmp.Services
{
  
        public interface IEmployeeRepository
        {
            Task<List<EmployeeDto>> GetAllAsync(int pageNumber, int pageSize);
            Task<EmployeeDto> GetAsync(int id);
            Task AddAsync(Employee employee);
            Task<EmployeeDto> UpdateAsync(EmployeeDto employeeDto);
            Task<EmployeeDto> DeleteAsync(int id);
        }

    }
