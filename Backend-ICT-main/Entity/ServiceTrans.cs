using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class ServiceTrans
    {

        public int ServiceTransId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Mã ngôn ngữ không được để trống")]
        [Display(Name = "Ngôn ngữ")]
        public string LangCode { get; set; } = "vi";

        [Required(ErrorMessage = "Tên không được để trống")]
        [Display(Name = "Tên dịch vụ")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Mô tả ngắn")]
        public string? ShortDescription { get; set; }

        // Navigation
        public Service Service { get; set; } = null!;
    }
}
