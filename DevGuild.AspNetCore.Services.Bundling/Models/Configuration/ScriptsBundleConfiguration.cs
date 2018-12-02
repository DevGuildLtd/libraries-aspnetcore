using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Bundling.Models.Configuration
{
    internal class ScriptsBundleConfiguration
    {
        public String Output { get; set; }

        public String[] Input { get; set; }
    }
}
