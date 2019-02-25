using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Logging
{
    public interface IRequestInformationProvider
    {
        RequestInformation GetRequestInformation();

        void OverrideRequestInformation(RequestInformation information);
    }
}
