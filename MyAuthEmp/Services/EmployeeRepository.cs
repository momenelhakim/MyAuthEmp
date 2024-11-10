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

                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                      .Select(e => new EmployeeDto
                      {
                          Id = e.Id,
                          Name = e.Name,
                          DepartmentName = e.Department != null ? e.Department.DepartmentName : null
                      })

                .ToListAsync();

            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetAsync(int id)
        {
            var employee = await _context.Employees
                   .Include(e => e.Department)
              .Where(e => e.Id == id)
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                DepartmentName = e.Department != null ? e.Department.DepartmentName : null
            })
            .FirstOrDefaultAsync();


            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeDto> UpdateAsync(EmployeeDto employeeDto)
        {
            var existingEmployee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == employeeDto.Id);

            if (existingEmployee == null)
                return null;

            existingEmployee.Name = employeeDto.Name;


            if (!string.IsNullOrEmpty(employeeDto.DepartmentName) && existingEmployee.Department != null)
            {
                existingEmployee.Department.DepartmentName = employeeDto.DepartmentName;
            }

            await _context.SaveChangesAsync();


            return _mapper.Map<EmployeeDto>(existingEmployee);
        }
        public async Task<EmployeeDto> DeleteAsync(int id)
        {
            var existingEmployee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (existingEmployee == null)
                return null;

            _context.Employees.Remove(existingEmployee);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeDto>(existingEmployee);
        }
    }
}
