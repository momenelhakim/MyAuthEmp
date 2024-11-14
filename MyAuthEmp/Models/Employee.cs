using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyAuthEmp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        // One-to-One Relationship with Department

        public ICollection<Salary> Salaries { get; set; } = new List<Salary>();
        public Department Department { get; set; } // Navigation property
    }
}