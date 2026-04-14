using System.Collections.Generic;

namespace FirstWebMVC.Models
{
    public class Faculty
    {
        public int FacultyID { get; set; }

        public string FacultyName { get; set; } = ""; // ✅ thêm = ""

        public ICollection<Student> Students { get; set; } = new List<Student>(); // ✅ thêm khởi tạo
    }
}