using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Vai trò")]
        public string Role { get; set; } = "user"; // "admin" | "user"

        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "active"; // "active" | "inactive"
    }
}
