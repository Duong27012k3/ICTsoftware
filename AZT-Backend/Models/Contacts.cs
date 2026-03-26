using System.ComponentModel.DataAnnotations;

namespace AZT_Backend.Models
{
    public class Ct
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên công ty/tổ chức")]
        public required string CompanyName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public required string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public required string PhoneNumber { get; set; } = string.Empty;
        public required string Message { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }

    }
}
