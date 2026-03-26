using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class Feature
    {
        public int FeatureId { get; set; }

        [Required]
        [Display(Name = "Dự án")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Mã ngôn ngữ không được để trống")]
        [Display(Name = "Ngôn ngữ")]
        public string LangCode { get; set; } = "vi";

        [Display(Name = "Nội dung")]
        public string? Content { get; set; }

        [Display(Name = "Loại (feature/benefit/spec)")]
        public string FeatureType { get; set; } = "feature";

        [Display(Name = "Nhãn (cho spec)")]
        public string? Label { get; set; }

        [Display(Name = "Icon")]
        public string? Icon { get; set; }

        // Navigation
        public Project Project { get; set; } = null!;
    }
}
