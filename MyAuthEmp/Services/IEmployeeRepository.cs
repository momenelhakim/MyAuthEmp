﻿using MyAuthEmp.Models;

namespace MyAuthEmp.Services
{
  
        public interface IEmployeeRepository
        {
            Task<List<EmployeeDto>> GetAllAsync(int pageNumber, int pageSize);
            Task<EmployeeDto> GetAsync(int id);
            Task<Employee> AddAsync(Employee employee );
            Task<EmployeeDto> UpdateAsync(int Id,EmployeeDto employeeDto);
            Task<Employee> DeleteAsync(int id);
        }

    }
