using FlightDocsSystem.DataAccess.Repository.IRepository;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FlightDocsSystem.DataAccess.Repository
{
    public class FirebaseStorageRepository : IFirebaseStorageRepository
    {
        private readonly IConfiguration _configuration;

        public FirebaseStorageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> DeleteFile(string fileName)
        {
            try
            {
                var projectId = _configuration["Firebase:ProjectId"];
                var credentialFilePath = "..\\FlightDocsSystem\\service-account.json";
                var credential = GoogleCredential.FromFile(credentialFilePath);
                var storageClient = await StorageClient.CreateAsync(credential);

                var bucketName = _configuration["Firebase:BucketName"];

                await storageClient.DeleteObjectAsync(bucketName, fileName);

                return true;
            }
            catch (Exception ex)
            {
                // Handle error
                Console.WriteLine($"Error deleting file: {ex.Message}");
                return false;
            }
        }


        public async Task<string> UploadFile(IFormFile file, string? documentVersion)
        {
            var projectId = _configuration["Firebase:ProjectId"];
            var bucketName = _configuration["Firebase:BucketName"];
            var credentialFilePath = "..\\FlightDocsSystem\\service-account.json";
            Console.WriteLine(credentialFilePath);
            Console.WriteLine(bucketName);
            //D:\repos\FlightDocsSystem\FlightDocsSystem\service-account.json
            // Thay đổi đường dẫn này để trỏ đến tệp JSON của tài khoản dịch vụ

            // Khởi tạo Firebase Admin SDK với thông tin xác thực
            var credential = GoogleCredential.FromFile(credentialFilePath);
            var storageClient = await StorageClient.CreateAsync(credential);

            // Tạo tên tệp tin duy nhất
            var fileName = Path.GetFileName(file.FileName);
            string objectName = "";
            if (documentVersion == null)
            {
                objectName = $"{Path.GetFileNameWithoutExtension(fileName)}{Path.GetExtension(fileName)}";
            }
            objectName = $"{Path.GetFileNameWithoutExtension(fileName)}_{documentVersion}{Path.GetExtension(fileName)}";
            // Tải lên tệp tin lên Firebase Cloud Storage
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                await storageClient.UploadObjectAsync(bucketName, objectName, contentType: file.ContentType, memoryStream);
            }

            // Trả về URL của tệp tin đã tải lên
            return $"https://storage.googleapis.com/{bucketName}/{objectName}?readOnly=true";
        }
    }
}