using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class BlockTrans
    {
        public int BlockTransId { get; set; }
        [Required]
        public int BlockId { get; set; }
        [Required(ErrorMessage = "Mã ngôn ngữ không được để trống")]
        [Display(Name = "Ngôn ngữ")]
        public string LangCode { get; set; } = "vi";
        [Display(Name = "Tiêu đề")]
        public string? Title { get; set; }
        [Display(Name = "Nội dung")]
        public string? Content { get; set; }
        // Navigation
        public Block Block { get; set; } = null!;
    }
}
