using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private readonly List<string> _allowedExtensions = new() { ".png", ".jpg", ".jpeg" };
        private const int _allowedMAxSize = 2_097_152;
        public async Task <string?> UploadAsync(IFormFile file, string FolderName)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_allowedExtensions.Contains(extension))
                return null;
            if (file.Length > _allowedMAxSize)
                return null;

            //var folderPath = $"{Directory.GetCurrentDirectory}\\wwwroot\\Files\\{FolderName}";

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            var fileName = $"{Guid.NewGuid()}{extension}";//Must be unique

            var filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            //using var fileStream=File.Create(filePath);

             await file.CopyToAsync(fileStream);
            return fileName;
        }
        public  bool Delete(string filePath)
        {
          if (File.Exists(filePath)) {
                File.Delete(filePath);
                return true; 
            }
          return false;
        }

    }
}
