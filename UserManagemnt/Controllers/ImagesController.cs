using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using UserManagemnt.Repositories;

namespace UserManagemnt.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageRepository _imageRepository;

        /// <summary>
        /// Inject ImageRepository
        /// </summary>
        /// <param name="imageRepository"></param>
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        /// <summary>
        /// Provide image file to image upload method in image repository
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Image url</returns>
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageUrl = await _imageRepository.UploadAsync(file);

            if (imageUrl == null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }

            //return Json format imageurl
            return Json(new { link = imageUrl });
        }
    }
}
