using System.ComponentModel.DataAnnotations;

namespace MyAuthEmp.Models
{
    public class Employee
    {
        [Key]

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal Salary { get; set; }
        public Department Department { get; set; }
    
}
}
