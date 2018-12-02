using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace DevGuild.AspNetCore.Services.Bundling.Models
{
    /// <summary>
    /// Represents a logical and physical path to a file.
    /// </summary>
    public sealed class BundlePath
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BundlePath"/> class.
        /// </summary>
        /// <param name="path">The logical path to a file.</param>
        /// <param name="file">The actual file.</param>
        public BundlePath(String path, IFileInfo file)
        {
            this.Path = path;
            this.File = file;
        }

        /// <summary>
        /// Gets the logical path to a file.
        /// </summary>
        /// <value>
        /// The logical path to a file.
        /// </value>
        public String Path { get; }

        /// <summary>
        /// Gets the actual file.
        /// </summary>
        /// <value>
        /// The actual file.
        /// </value>
        public IFileInfo File { get; }

        public String GetHashedPath()
        {
            return $"{this.Path}?v={this.File.LastModified.UtcTicks}";
        }
    }
}
