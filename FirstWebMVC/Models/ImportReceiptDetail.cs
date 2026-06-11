using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class ImportReceiptDetail
    {
        public int Id { get; set; }

        // Phiếu nhập
        public int ImportReceiptId { get; set; }

        // Navigation property phiếu nhập
        public ImportReceipt? ImportReceipt { get; set; }

        // Thiết bị
        [Display(Name = "Thiết bị")]
        public int DeviceId { get; set; }

        // Navigation property thiết bị
        public Device? Device { get; set; }

        // Số lượng
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        // Đơn giá nhập
        [Display(Name = "Đơn giá nhập")]
        public decimal UnitPrice { get; set; }

        // Thành tiền
        [Display(Name = "Thành tiền")]
        public decimal TotalPrice { get; set; }
    }
}
