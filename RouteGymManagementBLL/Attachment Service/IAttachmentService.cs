using Microsoft.AspNetCore.Http;

namespace RouteGymManagementBLL.Attachment_Service
{
    public interface IAttachmentService
    {
        string? Upload(string folderName, IFormFile file);
        bool Delete(string fileName, string folderName);

    }
}
