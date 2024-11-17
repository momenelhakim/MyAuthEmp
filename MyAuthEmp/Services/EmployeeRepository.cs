using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAuthEmp.Models;

namespace MyAuthEmp.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var employees = await _context.Employees
   .Include(e => e.Department)
   .Include(e => e.Salaries)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                      .Select(e => new EmployeeDto
                      {
                          Id = e.EmployeeId,
                          Name = e.Name,
                          DepartmentName = e.Department != null ? e.Department.DepartmentName : null,
                          Salaries = e.Salaries.Select(s => new Salary 
                          {
                              Amount = s.Amount,
                              Gross = s.Gross,
                              Balance=s.Balance,
                              Taxed=s.Taxed

                          }).ToList()
                
                      })

                .ToListAsync();
            await _context.SaveChangesAsync();


            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetAsync(int id)
        {
            var employee = await _context.Employees
                   .Include(e => e.Department)
                   .Include(e => e.Salaries)
              .Where(e => e.EmployeeId == id)
            .Select(e => new EmployeeDto
            {
                Id = e.EmployeeId,
                Name = e.Name,
                DepartmentName = e.Department != null ? e.Department.DepartmentName : null,

                Salaries = e.Salaries.Select(s => new Salary
                {
                    Amount = s.Amount,
                    Gross = s.Gross,
                    Balance = s.Balance,
                    Taxed = s.Taxed

                }).ToList()

            })
            .FirstOrDefaultAsync();
            await _context.SaveChangesAsync();


            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }


        public async Task<Employee> AddAsync(Employee employee)
        {
            // Add the new employee to the context
            await _context.Employees.AddAsync(employee);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Reload the employee with related data (Department and Salaries) if necessary
            var addedEmployee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Salaries)
                 .FirstOrDefaultAsync(e => e.EmployeeId == e.Department.EmployeeId && e.Salaries.Any(s => s.EmployeeId == employee.EmployeeId)&& e.Id==e.EmployeeId);
               

            return addedEmployee;
        }



        public async Task<EmployeeDto> UpdateAsync(int id, EmployeeDto employeeDto)
        {
            
            var existingEmployee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Salaries)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (existingEmployee == null)
                return null;

            
            existingEmployee.Name = employeeDto.Name;

         
            if (!string.IsNullOrEmpty(employeeDto.DepartmentName))
            {
                var department = await _context.Departments
                    .FirstOrDefaultAsync(d => d.DepartmentName == employeeDto.DepartmentName);

                if (department == null)
                {
                   
                    department = new Department { DepartmentName = employeeDto.DepartmentName };
                    await _context.Departments.AddAsync(department);
                    await _context.SaveChangesAsync();
                }

             
                existingEmployee.Department = department;
            }

            foreach (var newSalary in employeeDto.Salaries)
            {
                
                var existingSalary = existingEmployee.Salaries
                    .FirstOrDefault(s => s.EmployeeId == newSalary.EmployeeId);

                if (existingSalary != null)
                {
               
                    existingSalary.Amount = newSalary.Amount;
                   existingSalary.Balance= newSalary.Balance;
                    existingSalary.Gross = newSalary.Gross;
                    existingSalary.Taxed  = newSalary.Taxed;
                }
                else
                {
                    
                    existingEmployee.Salaries.Add(new Salary
                    {
                        EmployeeId = existingEmployee.EmployeeId,
                        Amount = newSalary.Amount,
                        Balance = newSalary.Balance,
                        Gross = newSalary.Gross,
                        Taxed = newSalary.Taxed,
                    });
                }
            }
            existingEmployee.Salaries= employeeDto.Salaries;
            await _context.SaveChangesAsync();

            
            var result = _mapper.Map<EmployeeDto>(existingEmployee);

         
            if (result.DepartmentName == null && existingEmployee.Department != null)
            {
                result.DepartmentName = existingEmployee.Department.DepartmentName;
            }

            return result;
        }



        public async Task<Employee> DeleteAsync(int id)
        {
          
            var existingEmployee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Salaries)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (existingEmployee == null)
                return null;

        
            _context.Salaries.RemoveRange(existingEmployee.Salaries);

            existingEmployee.Department = null;

            // Remove the employee
            _context.Employees.Remove(existingEmployee);
            await _context.SaveChangesAsync();

            return existingEmployee;
        }
    }
}
