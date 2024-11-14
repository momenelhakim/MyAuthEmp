using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyAuthEmp.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DepartmentName { get; set; }

        // One-to-One Relationship with Employee
        public int EmployeeId { get; set; } // Foreign key
        [JsonIgnore]
        public Employee? Employee { get; set; } // Navigation property
    }
}
