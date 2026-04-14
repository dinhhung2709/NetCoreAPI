using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [StringLength(20, ErrorMessage = "Mã sinh viên tối đa 20 ký tự")]
        public string StudentCode { get; set; } = "";

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên tối đa 100 ký tự")]
        public string FullName { get; set; } = "";

        [Range(1, 100, ErrorMessage = "Tuổi phải từ 1 đến 100")]
        public int? Age { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? Email { get; set; }

        //  thêm phần khóa ngoại Faculty  (Buổi 8)
        [Required(ErrorMessage = "Vui lòng chọn khoa")]
        public int FacultyID { get; set; }

        public Faculty? Faculty { get; set; }
    }
}