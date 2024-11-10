using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyAuthEmp.Models
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }

    }
}
