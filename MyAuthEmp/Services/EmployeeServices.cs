﻿using AutoMapper;
using MyAuthEmp.Models;

namespace MyAuthEmp.Services
{

    public class EmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeServices(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        public async Task<List<EmployeeDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var employees = await _employeeRepository.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<List<EmployeeDto>>(employees);
        }


        public async Task<EmployeeDto> GetAsync(int id)
        {
            var employee = await _employeeRepository.GetAsync(id);
            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }


        public async Task AddAsync(Employee employee)
        {


            await _employeeRepository.AddAsync(employee);
        }

        public async Task<EmployeeDto> UpdateAsync(int Id, EmployeeDto employeeDto)
        {

            var updatedEmployee = await _employeeRepository.UpdateAsync(Id, employeeDto);

            return updatedEmployee;
        }

        public async Task<Employee> DeleteAsync(int id)
        {
            var deletedEmployee = await _employeeRepository.DeleteAsync(id);
            return deletedEmployee;
        }
    }
}

