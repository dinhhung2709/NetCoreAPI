using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models
{
    public class Student
    {
        // 👉 ID tự tăng (Identity)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [StringLength(20)]
        public string StudentCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        // 👉 Cho phép null để tránh lỗi Excel import
        public int? Age { get; set; }

        public string? Email { get; set; }

        // 👉 FK nullable (quan trọng khi import Excel)
        public int? FacultyID { get; set; }

        // 👉 navigation property
        public Faculty? Faculty { get; set; }
    }
}