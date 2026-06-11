using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class ExportReceiptDetail
    {
        public int Id { get; set; }

        // Phiếu xuất
        public int ExportReceiptId { get; set; }

        // Navigation property
        public ExportReceipt? ExportReceipt { get; set; }

        // Thiết bị
        [Display(Name = "Thiết bị")]
        public int DeviceId { get; set; }

        // Navigation property
        public Device? Device { get; set; }

        // Số lượng
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        // Đơn giá xuất
        [Display(Name = "Đơn giá xuất")]
        public decimal UnitPrice { get; set; }

        // Thành tiền
        [Display(Name = "Thành tiền")]
        public decimal TotalPrice { get; set; }
    }
}