using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Bundling.Models.Configuration
{
    internal class BundlesConfiguration
    {
        public StylesBundleConfiguration[] Styles { get; set; }

        public ScriptsBundleConfiguration[] Scripts { get; set; }
    }
}
