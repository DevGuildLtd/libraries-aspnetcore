using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Files.Models
{
    public sealed class UploadedFileCustomData : IEquatable<UploadedFileCustomData>
    {
        public UploadedFileCustomData(String key, String value)
        {
            this.Key = key;
            this.Value = value;
        }

        public String Key { get; }

        public String Value { get; }

        public Boolean Equals(UploadedFileCustomData other)
        {
            if (Object.ReferenceEquals(null, other))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return String.Equals(this.Key, other.Key) && String.Equals(this.Value, other.Value);
        }

        public override Boolean Equals(Object obj)
        {
            if (Object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is UploadedFileCustomData other)
            {
                return String.Equals(this.Key, other.Key) && String.Equals(this.Value, other.Value);
            }
            else
            {
                return false;
            }
        }

        public override Int32 GetHashCode()
        {
            unchecked
            {
                return ((this.Key?.GetHashCode() ?? 0) * 397) ^ (this.Value?.GetHashCode() ?? 0);
            }
        }

        public override String ToString()
        {
            return $"{this.Key} = {this.Value}";
        }
    }
}
