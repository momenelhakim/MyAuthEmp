using AutoMapper;

namespace MyAuthEmp.Models
{
    public class SalaryProfile: Profile
    {
        public SalaryProfile() 
        {
            CreateMap<Salary,SalaryDto>();  
            CreateMap<SalaryDto,Salary>();  
        }
    }
}
