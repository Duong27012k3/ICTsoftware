using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class Block
    {
        [Key]
        public int BlockId { get; set; }

        [Required(ErrorMessage = "Loại owner không được để trống")]
        [Display(Name = "Loại")]
        public string OwnerType { get; set; } = string.Empty; // "service" | "project"

        [Required]
        [Display(Name = "ID Owner")]
        public int OwnerId { get; set; } // ServiceId hoặc ProjectId

        [Required(ErrorMessage = "Loại block không được để trống")]
        [Display(Name = "Loại block")]
        public string BlockType { get; set; } = string.Empty; // "banner" | "text" | "image"

        [Display(Name = "Thứ tự")]
        public int BlockOrder { get; set; } = 0;

        [Display(Name = "Hình ảnh")]
        public string? ImageUrl { get; set; }

        // Navigation
        public ICollection<BlockTrans> BlockTrans { get; set; } = new List<BlockTrans>();

        // Polymorphic navigation (tùy owner_type)
        public Service? Service { get; set; }
        public Project? Project { get; set; }

    }
}
