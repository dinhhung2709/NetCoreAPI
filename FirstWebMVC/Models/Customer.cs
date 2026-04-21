using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = "";

        public List<Order>? Orders { get; set; }
    }
}