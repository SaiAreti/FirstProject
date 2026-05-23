using FirstProject.Api.Model.Domain;
using FirstProject.Api.Model.DTO;
using FirstProject.Api.Repositires;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace FirstProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IimageRepository iimageRepository;

        public ImageUploadController(IimageRepository iimageRepository)
        {
            this.iimageRepository = iimageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload(ImageUploadRequestDTO imageUploadRequestDTO) 
        {
             ValidateFileUpload(imageUploadRequestDTO);
             if(ModelState.IsValid) 
             {
               
                var imageUploadDomainModel = new Image
                {
                    File = imageUploadRequestDTO.File,
                    FileExtension = Path.GetExtension(imageUploadRequestDTO.File.FileName),
                    FileName = imageUploadRequestDTO.FileName,
                    FileSizeInBytes = imageUploadRequestDTO.File.Length,
                    Description = imageUploadRequestDTO.Description
                };
                //Use Repository Upload Image 
                await iimageRepository.UploadImageAsync(imageUploadDomainModel);
                return Ok(imageUploadDomainModel);
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        public void ValidateFileUpload(ImageUploadRequestDTO imageUploadRequestDTO) 
        {
            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDTO.File.FileName))) 
            {
                ModelState.AddModelError("File", "Invalid file type. Only .jpg, .jpeg, .png, and .gif are allowed.");
            }
            if(imageUploadRequestDTO.File.Length > 5 * 1024 * 1024) 
            {
                ModelState.AddModelError("File", "File size exceeds the 5MB limit.");
            }
        }
    }
}
