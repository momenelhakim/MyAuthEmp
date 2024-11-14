using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyAuthEmp.Models
{
 
        public class Salary
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
            public int Id { get; set; }
            public decimal Amount { get; set; }
         public decimal Gross { get; set; }
        public decimal Balance { get; set; }
        public decimal Taxed { get; set; }  
            // Foreign key for Employee
            public int EmployeeId { get; set; }

        // Navigation property to link to Employee
        [JsonIgnore]
            public Employee? Employee { get; set; }
        }
 
}

