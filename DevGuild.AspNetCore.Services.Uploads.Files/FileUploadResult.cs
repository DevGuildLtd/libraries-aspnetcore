using System;
using DevGuild.AspNetCore.Services.Uploads.Files.Models;

namespace DevGuild.AspNetCore.Services.Uploads.Files
{
    public class FileUploadResult
    {
        private FileUploadResult(Boolean succeeded, UploadedFile file, String errorCode)
        {
            this.Succeeded = succeeded;
            this.File = file;
            this.ErrorCode = errorCode;
        }

        public Boolean Succeeded { get; }

        public UploadedFile File { get; }

        public String ErrorCode { get; }

        public static FileUploadResult Succeed(UploadedFile file)
        {
            return new FileUploadResult(true, file, null);
        }

        public static FileUploadResult Fail(String errorCode)
        {
            return new FileUploadResult(false, null, errorCode);
        }
    }
}
