using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace RouteGymManagementBLL.Attachment_Service
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IWebHostEnvironment webHost;
        private readonly string[] allowedExtensions = { ".jpg", ".png", ".jpeg" };
        private readonly long maxFileSize = 5 * 1024 * 1024; // 5 MB

        public AttachmentService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                if (folderName is null || file is null || file.Length == 0) return null;

                if (file.Length > maxFileSize) return null;

                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension)) return null;

                var folderPath = Path.Combine(webHost.WebRootPath, "images", folderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = Guid.NewGuid().ToString() + fileExtension;
                // wwwroot/images/member(ex)/132sa13s1a31s1assas13131.png
                var filePath = Path.Combine(folderPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
                return fileName;  // 132sa13s1a31s1assas13131.png
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Upload File To Folder = {folderName} : {ex}");
                return null;
            }

        }

        public bool Delete(string fileName, string folderName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folderName)) return false;

                var fullPath = Path.Combine(webHost.WebRootPath, "images", folderName, fileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                else
                {
                    return false;
                }
                ;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Delete File To With Name = {fileName} : {ex}");
                return false;
            }
        }
    }
}
