using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class FieldTrans
    {
        public int FieldTransId { get; set; }

        [Required]
        public int FieldId { get; set; }

        [Required(ErrorMessage = "Mã ngôn ngữ không được để trống")]
        [Display(Name = "Ngôn ngữ")]
        public string LangCode { get; set; } = "vi";

        [Required(ErrorMessage = "Tên không được để trống")]
        [Display(Name = "Tên lĩnh vực")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        // Navigation
        public Field Field { get; set; } = null!;
    }
}
