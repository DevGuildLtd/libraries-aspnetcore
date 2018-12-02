using System;
using System.Collections.Generic;
using System.Text;
using Amazon;
using Amazon.Runtime;

namespace DevGuild.AspNetCore.Services.Storage.AmazonS3
{
    /// <summary>
    /// Represents Amazon S3 storage container constructor.
    /// </summary>
    /// <seealso cref="StorageContainerConstructor" />
    public class AmazonS3StorageContainerConstructor : StorageContainerConstructor
    {
        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        public AWSCredentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public RegionEndpoint Region { get; set; }

        /// <summary>
        /// Gets or sets the name of the bucket.
        /// </summary>
        /// <value>
        /// The name of the bucket.
        /// </value>
        public String BucketName { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public String Key { get; set; }

        /// <inheritdoc />
        public override IStorageContainer Create()
        {
            return new AmazonS3StorageContainer(this.Credentials, this.Region, this.BucketName, this.Key);
        }
    }
}
