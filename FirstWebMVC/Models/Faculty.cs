using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class Faculty
    {
        public int FacultyID { get; set; }

        [Required(ErrorMessage = "Tên khoa không được để trống")]
        [StringLength(100)]
        public string FacultyName { get; set; } = string.Empty;

        // navigation property
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}