using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Represents permissions hub configuration.
    /// </summary>
    public sealed class PermissionsHubConfiguration
    {
        private readonly List<PermissionsHubConfigurationEntry> entries = new List<PermissionsHubConfigurationEntry>();

        /// <summary>
        /// Adds entry to the configuration.
        /// </summary>
        /// <param name="path">The path to the permissions manager.</param>
        /// <param name="permissionsNamespace">The permissions namespace.</param>
        /// <param name="constructor">The permissions manager constructor.</param>
        public void AddEntry(String path, PermissionsNamespace permissionsNamespace, PermissionsManagerConstructor constructor)
        {
            var entry = new PermissionsHubConfigurationEntry(path, constructor, permissionsNamespace);
            this.entries.Add(entry);
        }

        /// <summary>
        /// Gets the configuration entry by path to the permissions manager.
        /// </summary>
        /// <param name="path">The path to the permissions manager.</param>
        /// <returns>A configuration entry.</returns>
        public PermissionsHubConfigurationEntry GetEntry(String path)
        {
            return this.entries.SingleOrDefault(x => x.Path == path);
        }

        /// <summary>
        /// Gets the parent configuration entry.
        /// </summary>
        /// <param name="entry">The original entry entry.</param>
        /// <returns>The parent configuration entry or <c>null</c> if no entries were found.</returns>
        public PermissionsHubConfigurationEntry GetEntryParent(PermissionsHubConfigurationEntry entry)
        {
            return this.entries
                .Select(x => new
                {
                    Candidate = x,
                    StartsWith = entry.Path.StartsWith(PermissionsHubConfiguration.NormalizePath(x.Path)),
                    Length = x.Path.Length
                })
                .Where(x => x.StartsWith && x.Candidate.Path != entry.Path)
                .OrderByDescending(x => x.Length)
                .FirstOrDefault()?.Candidate;
        }

        private static String NormalizePath(String path)
        {
            if (path == null)
            {
                return null;
            }

            if (path.EndsWith("/"))
            {
                return path;
            }

            return path + "/";
        }
    }
}
