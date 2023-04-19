using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public class CertificateDataProtectionEncryptionProvider : IDataProtectionEncryptionProvider
    {
        public String Name => "Certificate";

        public IDataProtectionBuilder ConfigureEncryption(IDataProtectionBuilder builder, IConfigurationSection configuration)
        {
            var thumbprint = configuration.GetValue<String>("Thumprint", "");
            if (!String.IsNullOrEmpty(thumbprint))
            {
                return builder.ProtectKeysWithCertificate(thumbprint);
            }

            var path = configuration.GetValue<String>("Path", "");
            if (!String.IsNullOrEmpty(path))
            {
                var password = configuration.GetValue<String>("Password", "");
                if (!String.IsNullOrEmpty(password))
                {
                    return builder.ProtectKeysWithCertificate(new X509Certificate2(path, password));
                }
                else
                {
                    return builder.ProtectKeysWithCertificate(new X509Certificate2(path));
                }
            }

            var base64 = configuration.GetValue<String>("Base64", "");
            if (!String.IsNullOrEmpty(base64))
            {
                var certBytes = Convert.FromBase64String(base64);
                var password = configuration.GetValue<String>("Password", "");

                if (!String.IsNullOrEmpty(password))
                {
                    return builder.ProtectKeysWithCertificate(new X509Certificate2(certBytes, password));
                }
                else
                {
                    return builder.ProtectKeysWithCertificate(new X509Certificate2(certBytes));
                }
            }
            
            throw new InvalidOperationException("Certificate Encryption provider is not configured properly");
        }
    }
}
