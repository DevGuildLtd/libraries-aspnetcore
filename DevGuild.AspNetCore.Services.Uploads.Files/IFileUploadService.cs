using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Uploads.Files.Models;

namespace DevGuild.AspNetCore.Services.Uploads.Files
{
    public interface IFileUploadService
    {
        Task<UploadedFile> GetUploadedFileAsync(Guid id);

        Task<FileUploadResult> CreateUploadedFileAsync(String configuration, String fileName, Stream fileStream);
    }
}
