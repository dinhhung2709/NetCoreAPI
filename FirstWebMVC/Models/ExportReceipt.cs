using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class ExportReceipt
    {
        public int Id { get; set; }

        [Display(Name = "Ngày xuất")]
        public DateTime ExportDate { get; set; }

        // Danh sách chi tiết phiếu xuất
        public ICollection<ExportReceiptDetail>? ExportReceiptDetails { get; set; }
    }
}