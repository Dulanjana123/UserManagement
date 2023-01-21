using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UserManagemnt.Repositories
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
