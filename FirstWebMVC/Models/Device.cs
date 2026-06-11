using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên thiết bị không được để trống")]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        // =========================
        // KHÓA NGOẠI
        // =========================

        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public int DeviceCategoryId { get; set; }
        public DeviceCategory? DeviceCategory { get; set; }
    }
}