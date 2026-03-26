using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

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
        public ICollection<ProjectTrans> ProjectTrans { get; set; } = new List<ProjectTrans>();

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Feature> Features { get; set; } = new List<Feature>();

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Block> Blocks { get; set; } = new List<Block>();

        // Mapped properties cho frontend
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public IEnumerable<string> features => Features.Where(f => f.FeatureType == "feature").Select(f => f.Content ?? "");

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public IEnumerable<string> benefits => Features.Where(f => f.FeatureType == "benefit").Select(f => f.Content ?? "");

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public IEnumerable<object> technicalSpecs => Features.Where(f => f.FeatureType == "spec").Select(f => new { label = f.Label, value = f.Content });

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public IEnumerable<object> detailedSections => Blocks.OrderBy(b => b.BlockOrder).Select(b => new {
            title = b.BlockTrans.FirstOrDefault()?.Title,
            content = b.BlockTrans.FirstOrDefault()?.Content,
            imageUrl = b.ImageUrl
        });
    }
}
