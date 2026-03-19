using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string StudentCode { get; set; } = "";

        [Required]
        public string FullName { get; set; } = "";

        public int? Age { get; set; }

        public string? Email { get; set; }
    }
}