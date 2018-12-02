using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Bundling
{
    public class BundlingOptions
    {
        public Boolean Enabled { get; set; }

        public String Path { get; set; }

        public String WebRootRelativePath { get; set; }
    }
}
