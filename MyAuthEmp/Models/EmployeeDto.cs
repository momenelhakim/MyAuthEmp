namespace MyAuthEmp.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; init; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public ICollection<Salary> Salaries { get; set; } = new List<Salary>();
    }
}
