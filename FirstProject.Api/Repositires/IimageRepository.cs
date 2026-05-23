using FirstProject.Api.Model.Domain;

namespace FirstProject.Api.Repositires
{
    public interface IimageRepository
    {
        Task<Image> UploadImageAsync(Image image);
    }
}
