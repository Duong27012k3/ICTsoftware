using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        [Required]
        [Display(Name = "Lĩnh vực")]
        public int FieldId { get; set; }

        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [Display(Name = "Link catalogue")]
        public string? CatalogueUrl { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "active";

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Field? Field { get; set; } = null!;
        public ICollection<ServiceTrans> ServiceTrans { get; set; } = new List<ServiceTrans>();
        public ICollection<Block> Blocks { get; set; } = new List<Block>();
    }
}
