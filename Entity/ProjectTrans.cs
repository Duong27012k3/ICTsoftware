using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class ProjectTrans
    {
        public int ProjectTransId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Mã ngôn ngữ không được để trống")]
        [Display(Name = "Ngôn ngữ")]
        public string LangCode { get; set; } = "vi";

        [Required(ErrorMessage = "Tên không được để trống")]
        [Display(Name = "Tên dự án")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Mô tả ngắn")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Mô tả chi tiết")]
        public string? Description { get; set; }

        // Navigation
        public Project Project { get; set; } = null!;
    }
}
