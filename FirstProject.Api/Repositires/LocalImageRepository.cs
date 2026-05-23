using FirstProject.Api.Data;
using FirstProject.Api.Model.Domain;

namespace FirstProject.Api.Repositires
{
    public class LocalImageRepository : IimageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly WalkDbContext walkDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,WalkDbContext walkDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.walkDbContext = walkDbContext;
        }

       

        public async Task<Image> UploadImageAsync(Image image)
        {
            var localpath = Path.Combine(webHostEnvironment.ContentRootPath,"Images",$"{image.FileName}{image.FileExtension}");
            using var stream = new FileStream(localpath, FileMode.Create);
            await image.File.CopyToAsync(stream);

          

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            await walkDbContext.Images.AddAsync(image);
            await walkDbContext.SaveChangesAsync();
            return image;
        }
    }
}
