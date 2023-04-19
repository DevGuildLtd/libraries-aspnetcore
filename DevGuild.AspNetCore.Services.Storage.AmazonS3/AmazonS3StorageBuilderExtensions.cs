using System;
using System.Collections.Generic;
using System.Text;
using Amazon;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Services.Storage.AmazonS3
{
    public static class AmazonS3StorageBuilderExtensions
    {
        public static StorageServiceBuilder AddAmazonS3Provider(this StorageServiceBuilder builder)
        {
            return builder.AddContainerProvider("AmazonS3", configuration => new AmazonS3StorageContainerConstructor
            {
                BucketName = configuration.GetValue<String>("BucketName"),
                Region = RegionEndpoint.GetBySystemName(configuration.GetValue<String>("Region")),
                Key = configuration.GetValue<String>("Key"),
                Credentials = new BasicAWSCredentials(configuration.GetValue<String>("AccessKey"), configuration.GetValue<String>("SecretKey")),
                PublicRead = configuration.GetValue<Boolean?>("PublicRead") ?? true,
            });
        }
    }
}
