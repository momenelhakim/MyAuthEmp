using System.Text.Json.Serialization;

namespace MyAuthEmp.Models
{
    public class SalaryDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal Gross { get; set; }
        public decimal Balance { get; set; }
        public decimal Taxed { get; set; }

    }
}
