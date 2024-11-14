using AutoMapper;
namespace MyAuthEmp.Models
{
    public class DepartmentProfile: Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
        }
    }
}
