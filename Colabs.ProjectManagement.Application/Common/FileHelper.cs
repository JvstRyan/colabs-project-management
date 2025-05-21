using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;


namespace Colabs.ProjectManagement.Application.Common
{
    public static class FileHelper
    {
        public static bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            // Check file extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            
            if (!allowedExtensions.Contains(extension))
                return false;

            // Check MIME type
            string[] allowedMimeTypes = { "image/jpeg", "image/png", "image/gif", "image/webp" };
            
            if (!allowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
                return false;

            // Check file size (e.g., max 5MB)
            const int maxFileSize = 5 * 1024 * 1024; // 5MB
            if (file.Length > maxFileSize)
                return false;

            return true;
        }

        public static string GenerateUniqueFileName(string baseFileName)
        {
            var extension = Path.GetExtension(baseFileName);
            return $"{Path.GetFileNameWithoutExtension(baseFileName)}-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}{extension}";
        }

    }
}
