using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        // FK -> Customer
        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        // 1 Order -> nhiều OrderDetail
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}