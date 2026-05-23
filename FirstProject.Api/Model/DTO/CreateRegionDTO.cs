using System.ComponentModel.DataAnnotations;

namespace FirstProject.Api.Model.DTO
{
    public class CreateRegionDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be a max 100 characters length")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a min 3 characters length")]
        [MaxLength(3, ErrorMessage = "Code has to be a max 3 characters length")]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
