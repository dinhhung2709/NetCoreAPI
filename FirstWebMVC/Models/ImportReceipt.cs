using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class ImportReceipt
    {
        public int Id { get; set; }

        [Display(Name = "Ngày nhập")]
        public DateTime ImportDate { get; set; } = DateTime.Now;

        // Nhà cung cấp
        [Display(Name = "Nhà cung cấp")]
        public int SupplierId { get; set; }

        public Supplier? Supplier { get; set; }

        // Danh sách chi tiết
        public ICollection<ImportReceiptDetail>? ImportReceiptDetails { get; set; }
    }
}