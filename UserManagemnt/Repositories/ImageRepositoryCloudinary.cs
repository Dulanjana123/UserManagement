using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace UserManagemnt.Repositories
{
    public class ImageRepositoryCloudinary : IImageRepository
    {
        //can use private field inside UploadAsync method
        private readonly Account account;

        /// <summary>
        /// Inject IConfiguration 
        /// to read appsettings.json file cloudinary section
        /// </summary>
        /// <param name="configuration"></param>
        public ImageRepositoryCloudinary(IConfiguration configuration)
        {
            //Initiate cloudinary account
            //store it in a account private field
            account = new Account(configuration.GetSection("Cloudinary")["CloudName"],
                 configuration.GetSection("Cloudinary")["ApiKey"],
                 configuration.GetSection("Cloudinary")["ApiSecret"]);
        }

        /// <summary>
        /// Create new cloudinary account
        /// and upload file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> UploadAsync(IFormFile file)
        {
            //create new cloudinary object
            var client = new Cloudinary(account);

            //use cloudinary object to call UploadAsync method of the cloudinary library
            var uploadFileResult = await client.UploadAsync(
                new CloudinaryDotNet.Actions.ImageUploadParams()
                {
                    //file to upload and filename
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    //file display name
                    DisplayName = file.FileName
                });
            //check upload file result not null and statuscode 200
            if (uploadFileResult != null && uploadFileResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //return url of the file uploaded path
                return uploadFileResult.SecureUri.ToString();
            }
            return null;
        }
    }
}
