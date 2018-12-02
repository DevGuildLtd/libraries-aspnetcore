using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using DevGuild.AspNetCore.Services.Uploads.Files.Configuration;
using Newtonsoft.Json;

namespace DevGuild.AspNetCore.Services.Uploads.Files.Models
{
    public class UploadedFile
    {
        private List<UploadedFileCustomData> customData;

        public UploadedFile()
        {
        }

        public UploadedFile(String originalName, String extension, Int64 size, String hash, FileUploadConfiguration configuration, IEnumerable<UploadedFileCustomData> customData)
        {
            this.OriginalName = originalName;
            this.Extension = extension;
            this.Size = size;
            this.Hash = hash;
            this.Configuration = configuration.Id;
            this.ConfigurationVersion = configuration.Version;
            this.Container = configuration.Container;
            this.ContainerPrefix = configuration.ContainerPrefix;
            this.Created = DateTime.UtcNow;
            this.ReferenceCount = 0;
            this.ReferenceCountLastUpdated = this.Created;
            this.SetCustomData(customData);
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(256)]
        public String OriginalName { get; set; }

        [Required]
        [MaxLength(256)]
        public String Extension { get; set; }

        public Int64 Size { get; set; }

        [Required]
        [MaxLength(88)]
        public String Hash { get; set; }

        [Required]
        [MaxLength(100)]
        public String Configuration { get; set; }

        public Int32 ConfigurationVersion { get; set; }

        [Required]
        [MaxLength(100)]
        public String Container { get; set; }

        [Required]
        [MaxLength(100)]
        public String ContainerPrefix { get; set; }

        public DateTime Created { get; set; }

        public Int32 ReferenceCount { get; set; }

        public DateTime ReferenceCountLastUpdated { get; set; }

        [Column("CustomData")]
        public String CustomDataContent { get; set; }

        [NotMapped]
        public IReadOnlyCollection<UploadedFileCustomData> CustomData
        {
            get
            {
                if (this.customData == null)
                {
                    this.customData = !String.IsNullOrEmpty(this.CustomDataContent)
                        ? JsonConvert.DeserializeObject<List<UploadedFileCustomData>>(this.CustomDataContent)
                        : new List<UploadedFileCustomData>();
                }

                return this.customData.AsReadOnly();
            }
        }

        public String GetContainerPath()
        {
            var pathBuilder = new StringBuilder();
            if (!String.IsNullOrEmpty(this.ContainerPrefix))
            {
                pathBuilder.Append($"{this.ContainerPrefix}/");
            }

            pathBuilder.Append($"{this.Id}/{this.OriginalName}.{this.Extension}");
            return pathBuilder.ToString();
        }

        private void SetCustomData(IEnumerable<UploadedFileCustomData> customData)
        {
            this.customData = (customData ?? new List<UploadedFileCustomData>()).ToList();
            this.CustomDataContent = JsonConvert.SerializeObject(this.customData);
        }
    }
}
