using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

namespace DevGuild.AspNetCore.Services.Storage.AmazonS3
{
    /// <summary>
    /// Represents Amazon S3 storage container.
    /// </summary>
    /// <seealso cref="StorageContainer" />
    public sealed class AmazonS3StorageContainer : StorageContainer
    {
        private readonly String bucketName;
        private readonly String key;
        private readonly AmazonS3Client amazonS3Client;
        private readonly String baseUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="AmazonS3StorageContainer"/> class.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="region">The region.</param>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="key">The key.</param>
        public AmazonS3StorageContainer(AWSCredentials credentials, RegionEndpoint region, String bucketName, String key)
        {
            this.bucketName = bucketName;
            this.key = key.EndsWith("/") ? key : String.Concat(key, "/");

            this.amazonS3Client = new AmazonS3Client(credentials, region);
            this.baseUrl = $"https://{this.bucketName}.s3.amazonaws.com/";
        }

        /// <inheritdoc />
        public override async Task StoreFileAsync(String fileName, Stream fileStream)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException($"{nameof(fileName)} is null", nameof(fileName));
            }

            if (fileStream == null)
            {
                throw new ArgumentNullException($"{nameof(fileStream)} is null", nameof(fileStream));
            }

            if (fileStream.Position > 0)
            {
                fileStream.Seek(0, SeekOrigin.Begin);
            }

            fileName = this.ValidateAndNormalizeFileName(fileName, false);
            var fullKey = String.Concat(this.key, fileName);

            if (await this.CheckS3ObjectExists(fullKey))
            {
                throw new FileAlreadyExistsException();
            }

            await this.PutS3Object(fullKey, fileStream);
        }

        /// <inheritdoc />
        public override String GetFileUrl(String fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException($"{nameof(fileName)} is null", nameof(fileName));
            }

            fileName = this.ValidateAndNormalizeFileName(fileName, false);
            return String.Concat(this.baseUrl, this.key, fileName);
        }

        /// <inheritdoc />
        public override Task<String> GetFileUrlAsync(String fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException($"{nameof(fileName)} is null", nameof(fileName));
            }

            fileName = this.ValidateAndNormalizeFileName(fileName, false);
            return Task.FromResult(String.Concat(this.baseUrl, this.key, fileName));
        }

        /// <inheritdoc />
        public override async Task<Stream> GetFileContentAsync(String fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException($"{nameof(fileName)} is null", nameof(fileName));
            }

            fileName = this.ValidateAndNormalizeFileName(fileName, false);
            var fullKey = String.Concat(this.key, fileName);

            var request = new GetObjectRequest
            {
                BucketName = this.bucketName,
                Key = fullKey
            };

            try
            {
                using (var response = await this.amazonS3Client.GetObjectAsync(request))
                {
                    var memoryStream = new MemoryStream();
                    await response.ResponseStream.CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return memoryStream;
                }
            }
            catch (AmazonS3Exception exception)
            {
                if (exception.ErrorCode == "NoSuchKey")
                {
                    throw new FileNotFoundException();
                }

                throw;
            }
        }

        /// <inheritdoc />
        public override async Task DeleteFileAsync(String fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException($"{nameof(fileName)} is null", nameof(fileName));
            }

            fileName = this.ValidateAndNormalizeFileName(fileName, false);
            var fullKey = String.Concat(this.key, fileName);

            var request = new DeleteObjectRequest
            {
                BucketName = this.bucketName,
                Key = fullKey
            };

            await this.amazonS3Client.DeleteObjectAsync(request);
        }

        /// <inheritdoc />
        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                this.amazonS3Client?.Dispose();
            }
        }

        private async Task<Boolean> CheckS3ObjectExists(String fullKey)
        {
            try
            {
                await this.amazonS3Client.GetObjectMetadataAsync(this.bucketName, fullKey);
                return true;
            }
            catch (AmazonS3Exception exception)
            {
                if (exception.ErrorCode == "NotFound")
                {
                    return false;
                }

                throw;
            }
        }

        private async Task PutS3Object(String fullKey, Stream fileStream)
        {
            await this.amazonS3Client.PutObjectAsync(new PutObjectRequest
            {
                BucketName = this.bucketName,
                Key = fullKey,
                CannedACL = S3CannedACL.PublicRead,
                InputStream = fileStream
            });
        }
    }
}
