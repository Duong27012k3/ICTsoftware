using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống")]
        [Display(Name = "Nội dung")]
        public string Message { get; set; } = string.Empty;

        [Display(Name = "Ngày gửi")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
