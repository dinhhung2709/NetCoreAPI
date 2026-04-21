using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải >= 0")]
        public double Price { get; set; }

        // Quan hệ 1-n với OrderDetail
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}