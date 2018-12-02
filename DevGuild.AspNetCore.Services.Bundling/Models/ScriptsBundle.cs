using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuild.AspNetCore.Services.Bundling.Models
{
    /// <summary>
    /// Represents a scripts bundle.
    /// </summary>
    public class ScriptsBundle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptsBundle"/> class.
        /// </summary>
        /// <param name="output">The output file path.</param>
        /// <param name="input">The collection of input files' paths.</param>
        public ScriptsBundle(BundlePath output, IEnumerable<BundlePath> input)
        {
            this.Output = output;
            this.Input = input.ToList();
        }

        /// <summary>
        /// Gets the output file path of the bundle.
        /// </summary>
        /// <value>
        /// The output file path of the bundle.
        /// </value>
        public BundlePath Output { get; }

        /// <summary>
        /// Gets the collection of input files' paths.
        /// </summary>
        /// <value>
        /// The collection of input files' paths.
        /// </value>
        public IReadOnlyList<BundlePath> Input { get; }
    }
}
