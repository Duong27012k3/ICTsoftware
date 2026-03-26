using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class Field
    {
        public int FieldId { get; set; }
        public string? NameField { get; set; } = string.Empty;

        [Required(ErrorMessage = "UID không được để trống")]
        [Display(Name = "UID (Slug)")]
        public string Uid { get; set; } = string.Empty;

        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "active";

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<FieldTrans> FieldTrans { get; set; } = new List<FieldTrans>();
        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
