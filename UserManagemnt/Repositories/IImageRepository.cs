using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UserManagemnt.Repositories
{
    public interface IImageRepository
    {
        /// <summary>
        /// Declaration of the image uploadAsync method
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<string> UploadAsync(IFormFile file);
    }
}
