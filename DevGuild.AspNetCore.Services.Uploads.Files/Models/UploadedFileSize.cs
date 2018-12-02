using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Files.Models
{
    public static class UploadedFileSize
    {
        public static Int64 Bytes(Int64 value)
        {
            return value;
        }

        public static Int64 KiloBytes(Int64 value)
        {
            return value * 1024;
        }

        public static Int64 MegaBytes(Int64 value)
        {
            return value * 1024 * 1024;
        }

        public static Int64 GigaBytes(Int64 value)
        {
            return value * 1024 * 1024 * 1024;
        }

        public static Int64 TeraBytes(Int64 value)
        {
            return value * 1024 * 1024 * 1024 * 1024;
        }
    }
}
