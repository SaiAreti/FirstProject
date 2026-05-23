using Microsoft.AspNetCore.Http.Metadata;
using System.ComponentModel.DataAnnotations;

namespace FirstProject.Api.Model.DTO
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? Description { get; set; }
    }
}
