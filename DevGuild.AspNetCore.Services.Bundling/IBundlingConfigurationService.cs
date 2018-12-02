using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Bundling.Models;

namespace DevGuild.AspNetCore.Services.Bundling
{
    public interface IBundlingConfigurationService
    {
        Boolean Enabled { get; }

        IReadOnlyDictionary<String, ScriptsBundle> ScriptsBundles { get; }

        IReadOnlyDictionary<String, StylesBundle> StylesBundles { get; }

        Task InitializeAsync();
    }
}
